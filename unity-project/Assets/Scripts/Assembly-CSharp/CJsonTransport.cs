using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LitJson;
using UnityEngine;

public class CJsonTransport : IDisposable
{
	public delegate void MessageHandler(string rmiName, string data);

	public delegate void StateHandler();

	public enum EState
	{
		Disconnected = 0,
		Connecting = 1,
		Connected = 2
	}

	private class PendingMessage
	{
		public string Name;

		public string Data;
	}

	public string ServerIP = "32.236.192.141";

	public int ServerPort = 2031;

	public StateHandler OnConnected;

	public StateHandler OnDisconnected;

	private MessageHandler _onMessage;

	private TcpClient _client;

	private NetworkStream _stream;

	private Thread _thread;

	private volatile bool _running;

	private volatile EState _state = EState.Disconnected;

	private readonly object _sync = new object();

	private readonly Queue<PendingMessage> _inbox = new Queue<PendingMessage>();

	private readonly object _sendLock = new object();

	private readonly StringBuilder _chunks = new StringBuilder();

	private bool _fireConnected;

	private bool _fireDisconnected;

	private static string s_defaultIP = "32.236.192.141";

	private static int s_defaultPort = 2031;

	private static CJsonTransport s_global;

	public EState State
	{
		get
		{
			return _state;
		}
	}

	public static string DefaultIP
	{
		get
		{
			return s_defaultIP;
		}
	}

	public static int DefaultPort
	{
		get
		{
			return s_defaultPort;
		}
	}

	public CJsonTransport(MessageHandler onMessage)
	{
		_onMessage = onMessage;
	}

	public static void LoadConfig()
	{
		try
		{
			string text = null;
			string[] array = new string[2];
			try
			{
				array[0] = Path.Combine(Application.persistentDataPath, "server_config.txt");
			}
			catch
			{
				array[0] = null;
			}
			try
			{
				array[1] = Path.Combine(Path.GetDirectoryName(Application.dataPath), "server_config.txt");
			}
			catch
			{
				array[1] = null;
			}
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				if (text2 != null && File.Exists(text2))
				{
					text = text2;
					break;
				}
			}
			if (text == null)
			{
				return;
			}
			string[] array3 = File.ReadAllLines(text);
			string[] array4 = array3;
			foreach (string text3 in array4)
			{
				string text4 = text3.Trim();
				if (text4.Length == 0 || text4.StartsWith("#") || text4.StartsWith("//"))
				{
					continue;
				}
				int num = text4.IndexOf('=');
				if (num > 0)
				{
					string text5 = text4.Substring(0, num).Trim().ToLower();
					string s = text4.Substring(num + 1).Trim();
					int result;
					if (text5 == "server_ip" || text5 == "entry_ip")
					{
						s_defaultIP = s;
					}
					else if ((text5 == "server_port" || text5 == "entry_port") && int.TryParse(s, out result))
					{
						s_defaultPort = result;
					}
				}
			}
			EGDebug.LogWarning("[CJsonTransport] Config: " + s_defaultIP + ":" + s_defaultPort);
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[CJsonTransport] LoadConfig error: " + ((ex != null) ? ex.ToString() : null));
		}
	}

	public void SetAsGlobal()
	{
		s_global = this;
	}

	public void UnsetGlobal()
	{
		if (s_global == this)
		{
			s_global = null;
		}
	}

	public void Connect()
	{
		Disconnect();
		_fireConnected = false;
		_fireDisconnected = false;
		_state = EState.Connecting;
		lock (_sync)
		{
			_inbox.Clear();
		}
		_chunks.Length = 0;
		_running = true;
		ServerIP = s_defaultIP;
		ServerPort = s_defaultPort;
		string ip = ServerIP;
		int port = ServerPort;
		EGDebug.LogWarning("[CJsonTransport] Connecting to " + ip + ":" + port);
		_thread = new Thread(() =>
		{
			RunReader(ip, port);
		});
		_thread.IsBackground = true;
		_thread.Start();
	}

	public void Disconnect()
	{
		_running = false;
		try
		{
			if (_stream != null)
			{
				_stream.Close();
			}
		}
		catch
		{
		}
		try
		{
			if (_client != null)
			{
				_client.Close();
			}
		}
		catch
		{
		}
		_stream = null;
		_client = null;
		Thread thread = _thread;
		_thread = null;
		if (thread != null && thread.IsAlive && Thread.CurrentThread != thread)
		{
			try
			{
				thread.Join(500);
			}
			catch
			{
			}
		}
		if (_state == EState.Connected)
		{
			_fireDisconnected = true;
		}
		_state = EState.Disconnected;
	}

	public static void Send(string rmiName, string data)
	{
		if (s_global != null)
		{
			s_global.SendRaw(rmiName, data);
		}
	}

	public void SendRaw(string rmiName, string data)
	{
		string s;
		try
		{
			if (string.IsNullOrEmpty(data) || data.Trim() == "null")
			{
				s = "{\"m\":\"" + rmiName + "\",\"d\":null}\n";
			}
			else
			{
				JsonData value = JsonMapper.ToObject(data);
				JsonData jsonData = new JsonData();
				jsonData["m"] = rmiName;
				jsonData["d"] = value;
				s = JsonMapper.ToJson(jsonData, false) + "\n";
			}
		}
		catch
		{
			try
			{
				JsonData value2 = new JsonData(data);
				JsonData jsonData2 = new JsonData();
				jsonData2["m"] = rmiName;
				jsonData2["d"] = value2;
				s = JsonMapper.ToJson(jsonData2, false) + "\n";
			}
			catch
			{
				s = "{\"m\":\"" + rmiName + "\",\"d\":null}\n";
			}
		}
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		try
		{
			bool flag;
			lock (_sendLock)
			{
				flag = _stream != null && _state == EState.Connected;
				if (flag)
				{
					_stream.Write(bytes, 0, bytes.Length);
					_stream.Flush();
				}
			}
			if (!flag)
			{
				EGDebug.LogError("[CJsonTransport] Not connected, drop " + rmiName);
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[CJsonTransport] Send error: " + ex.Message);
		}
	}

	private void RunReader(string ip, int port)
	{
		try
		{
			TcpClient tcpClient = new TcpClient();
			tcpClient.NoDelay = true;
			tcpClient.Connect(ip, port);
			_client = tcpClient;
			_stream = tcpClient.GetStream();
			_state = EState.Connected;
			_fireConnected = true;
			byte[] array = new byte[65536];
			MemoryStream memoryStream = new MemoryStream();
			while (_running)
			{
				int num = _stream.Read(array, 0, array.Length);
				if (num <= 0)
				{
					break;
				}
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					if (array[i] == 10)
					{
						memoryStream.Write(array, num2, i - num2);
						ParseLine(Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
						memoryStream.SetLength(0L);
						num2 = i + 1;
					}
				}
				if (num2 < num)
				{
					memoryStream.Write(array, num2, num - num2);
				}
				if (memoryStream.Length > 16777216)
				{
					throw new IOException("Transport line limit exceeded");
				}
			}
		}
		catch (Exception ex)
		{
			if (_running)
			{
				EGDebug.LogError("[CJsonTransport] Reader error: " + ex.Message);
			}
		}
		finally
		{
			if (_running)
			{
				_fireDisconnected = true;
			}
			_state = EState.Disconnected;
			try
			{
				if (_stream != null)
				{
					_stream.Close();
				}
				if (_client != null)
				{
					_client.Close();
				}
			}
			catch
			{
			}
		}
	}

	private void ParseLine(string line)
	{
		line = line.Trim();
		if (line.Length == 0)
		{
			return;
		}
		try
		{
			JsonData jsonData = JsonMapper.ToObject(line);
			if (jsonData["m"].ToString() == "TransportCompressed")
			{
				byte[] buffer = Convert.FromBase64String(jsonData["d"].ToString());
				using (MemoryStream compressedStream = new MemoryStream(buffer))
				{
					using (GZipStream gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							byte[] array = new byte[16384];
							int count;
							while ((count = gZipStream.Read(array, 0, array.Length)) > 0)
							{
								memoryStream.Write(array, 0, count);
								if (memoryStream.Length > 16777216)
								{
									throw new IOException("Transport decompression limit exceeded");
								}
							}
							ParseLine(Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
							return;
						}
					}
				}
			}
			if (jsonData["m"].ToString() == "TransportChunk")
			{
				_chunks.Append(jsonData["d"]["text"].ToString());
				if (_chunks.Length > 16777216)
				{
					throw new IOException("Transport chunk limit exceeded");
				}
				if ((bool)jsonData["d"]["last"])
				{
					string line2 = _chunks.ToString();
					_chunks.Length = 0;
					ParseLine(line2);
				}
				return;
			}
			PendingMessage pendingMessage = new PendingMessage();
			pendingMessage.Name = (((IDictionary)jsonData).Contains((object)"m") ? jsonData["m"].ToString() : string.Empty);
			pendingMessage.Data = ((((IDictionary)jsonData).Contains((object)"d") && jsonData["d"] != null) ? JsonMapper.ToJson(jsonData["d"], false) : "null");
			lock (_sync)
			{
				_inbox.Enqueue(pendingMessage);
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[CJsonTransport] Parse error: " + ex.Message);
		}
	}

	public void Pump()
	{
		if (_fireConnected)
		{
			_fireConnected = false;
			if (OnConnected != null)
			{
				OnConnected();
			}
		}
		if (_fireDisconnected)
		{
			_fireDisconnected = false;
			if (OnDisconnected != null)
			{
				OnDisconnected();
			}
		}
		bool flag = true;
		DateTime dateTime = DateTime.UtcNow.AddMilliseconds(4.0);
		while (flag && DateTime.UtcNow < dateTime)
		{
			flag = false;
			PendingMessage pendingMessage = null;
			lock (_sync)
			{
				if (_inbox.Count > 0)
				{
					pendingMessage = _inbox.Dequeue();
					flag = true;
				}
			}
			if (pendingMessage != null && _onMessage != null)
			{
				_onMessage(pendingMessage.Name, pendingMessage.Data);
			}
		}
	}

	public void Dispose()
	{
		UnsetGlobal();
		Disconnect();
	}
}
