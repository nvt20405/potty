using System;
using System.IO;
using System.Reflection;

namespace Nettention.Proud
{
	internal class PlatformGetter
	{
		private static RuntimePlatform platformType = RuntimePlatform.None;

		internal static RuntimePlatform StaticPlatformType
		{
			get
			{
				if (platformType == RuntimePlatform.None)
				{
					try
					{
						try
						{
							AssemblyName assemblyRef = new AssemblyName("UnityEngine");
							Assembly assembly = Assembly.Load(assemblyRef);
							Type type = assembly.GetType("UnityEngine.Application");
							PropertyInfo property = type.GetProperty("platform");
							object value = property.GetValue(property, null);
							platformType = (RuntimePlatform)value;
						}
						catch (FileNotFoundException)
						{
							platformType = RuntimePlatform.WindowsPlayer;
							switch (Environment.OSVersion.Platform)
							{
							case PlatformID.Unix:
								platformType = RuntimePlatform.IPhonePlayer;
								break;
							case PlatformID.MacOSX:
								platformType = RuntimePlatform.OSXPlayer;
								break;
							default:
								platformType = RuntimePlatform.WindowsPlayer;
								break;
							}
						}
						catch (TargetInvocationException)
						{
							platformType = RuntimePlatform.WindowsPlayer;
						}
						catch (MethodAccessException)
						{
							platformType = RuntimePlatform.WindowsWebPlayer;
						}
					}
					catch (Exception)
					{
						platformType = RuntimePlatform.IPhonePlayer;
					}
				}
				return platformType;
			}
		}
	}
}
