using System;
using HTMLEngine;
using HTMLEngine.Unity3D;
using UnityEngine;

public class Unity3DDemo : MonoBehaviour
{
	private const string demo0 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<p id='introduction' align=center>This is based on Profixy's HTMLEngine-Mini.<br>URL:&nbsp;<u>http://html-engine-mini.googlecode.com/</u><br></p>\r\n<br><p><i>Here is no any &lt;html&gt;, &lt;body&gt; etc tags. Only small subset of tags supported yet:</i></p>\r\n<br>\r\n<p><code>- &lt;<font color=yellow>p</font>&nbsp;[id='...'] [align=(left | right | center | justify)]<br>[valign=(top | middle | bottom)]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>spin</font>&nbsp;[id='...'] [width=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>img</font>&nbsp;src='...' [id='...'] [fps=...] [width=...] [height=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>font</font>&nbsp;[face=...] [size=...] [color=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>effect</font>&nbsp;name=... [amount=...] [color=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>br</font>&gt; &lt;<font color=yellow>b</font>&gt; &lt;<font color=yellow>i</font>&gt; &lt;<font color=yellow>u</font>&gt; &lt;<font color=yellow>s</font>&gt; &lt;<font color=yellow>code</font>&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>a</font>&nbsp;href='...'&gt;</code></p>\r\n<br>\r\n<p><i>Also this demo contains some internal resources (fonts and images) than can be used.</i></p>\r\n<br>\r\n<p><b>Available fonts:&nbsp;</b>'default16', 'default16b', 'default16bi', 'default16i', 'title24'</p>\r\n<p><b>Available atleses:&nbsp;</b>'smiles', 'logos', 'faces'</p>\r\n";

	private const string demo1 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<p align=left>Without effect:</p>\r\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\r\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\r\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\r\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\r\n<p align=left>Shadow effect:</p>\r\n<effect name=shadow color=black>\r\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\r\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\r\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\r\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\r\n</effect>\r\n<p align=left>Outline effect:</p>\r\n<effect name=outline color=black>\r\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\r\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\r\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\r\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\r\n</effect>\r\n";

	private const string demo2 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<font size=24>\r\n<br><spin id='outlined' align=center><effect name=outline color=#FFFFFF80><font color=black>Outlined text</font></effect></spin>\r\n<p align=center><effect name=outline color=yellow><font color=black>Outlined yellow text</font></effect></p>\r\n<p align=center><effect name=outline color=#FFFFFF80 amount=2>Some stuppid effect i got</effect></p>\r\n<p align=center><effect name=shadow>Default shadowed text</effect></p>\r\n<p align=center><effect name=shadow color=black>Strong-shadowed text</effect></p>\r\n<p align=center><effect name=shadow color=#FFFFFF80 amount=2>Some shadowed text</effect></p>\r\n</font>\r\n    ";

	private const string demo3 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=justify>Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text.</p>\r\n<br><p align=center><font color=gray>Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text.</font></p>\r\n<br><p align=right>Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text.</p>\r\n<br><p align=left><font color=gray>Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text.</font></p>\r\n        ";

	private const string demo4 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=center valign=top>Picture <img src='smiles/sad'> with &lt;p valign=top&gt;</p>\r\n<br><p align=center valign=middle>Picture <img src='smiles/smile'> with &lt;p valign=middle&gt; much better than others in this case <img src='smiles/cool'></p>\r\n<br><p align=center valign=bottom>Picture <img src='smiles/sad'> with &lt;p valign=bottom&gt;</p>\r\n<br><p align=justify valign=bottom><img src='logos/unity'> is a feature rich, fully integrated development engine for the creation of interactive 3D content. It provides complete, out-of-the-box functionality to assemble high-quality, high-performing content and publish to multiple platforms.</p>\r\n<br><p align=center><img src='logos/unity2'></p>\r\n        ";

	private const string demo5 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=center>Now we try to make something dynamic inside text markup...</p>\r\n<br><p align=center valign=middle>Due to performance we can not parse text every frame <img src='smiles/sad'>, but we can reserve some place to draw things inside compiled html! <img src='smiles/cool'></p>\r\n<br><p align=center>It's possible with img tag. Look at source.</p>\r\n<br><p align=center><img src='#time'></p>\r\n<br><p align=center>With same technique we can render animated pictures and even results from render targets.</p>\r\n";

	private const string demo6 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=center>Links support</p>\r\n<br><p align=left valign=middle>1)&nbsp;<a href='plaintextlink' id='simple'>Simple plain text link.</a></p>\r\n<br><p align=left valign=middle>2)&nbsp;<a href='textandimage'>Simple text and <img src='smiles/smile'> image link.</a></p>\r\n<br><p align=left valign=middle>3)&nbsp;<a href='biglink1'>Multiline link <img src='smiles/smile'>.</a>&nbsp;<a href='biglink2'>Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>.</a></p>\r\n<br><br><p align=center>Try to click around and see to left-bottom corner for results</p>\r\n<br><br><p align=center>At last we have some basic stuff to interact with player</p>\r\n";

	private const string demo7 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br><br><p align=center>Some stuff using Unity3DGUI.Label here</p>\r\n<br><br><p align=center>Below is totally dynamic html text, and statistics for compile and drawing</p>\r\n";

	private const int x = 5;

	private const int y = 50;

	private bool changed = true;

	private string _html = string.Empty;

	private int bar;

	private readonly string[] barTexts = new string[8] { "Start", "Text styles", "Text effects", "Para aligns", "Images", "Other", "Links", "HtmlGUI" };

	private readonly string[] barHtmls = new string[8] { "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<p id='introduction' align=center>This is based on Profixy's HTMLEngine-Mini.<br>URL:&nbsp;<u>http://html-engine-mini.googlecode.com/</u><br></p>\r\n<br><p><i>Here is no any &lt;html&gt;, &lt;body&gt; etc tags. Only small subset of tags supported yet:</i></p>\r\n<br>\r\n<p><code>- &lt;<font color=yellow>p</font>&nbsp;[id='...'] [align=(left | right | center | justify)]<br>[valign=(top | middle | bottom)]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>spin</font>&nbsp;[id='...'] [width=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>img</font>&nbsp;src='...' [id='...'] [fps=...] [width=...] [height=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>font</font>&nbsp;[face=...] [size=...] [color=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>effect</font>&nbsp;name=... [amount=...] [color=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>br</font>&gt; &lt;<font color=yellow>b</font>&gt; &lt;<font color=yellow>i</font>&gt; &lt;<font color=yellow>u</font>&gt; &lt;<font color=yellow>s</font>&gt; &lt;<font color=yellow>code</font>&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>a</font>&nbsp;href='...'&gt;</code></p>\r\n<br>\r\n<p><i>Also this demo contains some internal resources (fonts and images) than can be used.</i></p>\r\n<br>\r\n<p><b>Available fonts:&nbsp;</b>'default16', 'default16b', 'default16bi', 'default16i', 'title24'</p>\r\n<p><b>Available atleses:&nbsp;</b>'smiles', 'logos', 'faces'</p>\r\n", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<p align=left>Without effect:</p>\r\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\r\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\r\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\r\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\r\n<p align=left>Shadow effect:</p>\r\n<effect name=shadow color=black>\r\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\r\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\r\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\r\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\r\n</effect>\r\n<p align=left>Outline effect:</p>\r\n<effect name=outline color=black>\r\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\r\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\r\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\r\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\r\n</effect>\r\n", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<font size=24>\r\n<br><spin id='outlined' align=center><effect name=outline color=#FFFFFF80><font color=black>Outlined text</font></effect></spin>\r\n<p align=center><effect name=outline color=yellow><font color=black>Outlined yellow text</font></effect></p>\r\n<p align=center><effect name=outline color=#FFFFFF80 amount=2>Some stuppid effect i got</effect></p>\r\n<p align=center><effect name=shadow>Default shadowed text</effect></p>\r\n<p align=center><effect name=shadow color=black>Strong-shadowed text</effect></p>\r\n<p align=center><effect name=shadow color=#FFFFFF80 amount=2>Some shadowed text</effect></p>\r\n</font>\r\n    ", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=justify>Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text.</p>\r\n<br><p align=center><font color=gray>Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text.</font></p>\r\n<br><p align=right>Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text.</p>\r\n<br><p align=left><font color=gray>Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text.</font></p>\r\n        ", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=center valign=top>Picture <img src='smiles/sad'> with &lt;p valign=top&gt;</p>\r\n<br><p align=center valign=middle>Picture <img src='smiles/smile'> with &lt;p valign=middle&gt; much better than others in this case <img src='smiles/cool'></p>\r\n<br><p align=center valign=bottom>Picture <img src='smiles/sad'> with &lt;p valign=bottom&gt;</p>\r\n<br><p align=justify valign=bottom><img src='logos/unity'> is a feature rich, fully integrated development engine for the creation of interactive 3D content. It provides complete, out-of-the-box functionality to assemble high-quality, high-performing content and publish to multiple platforms.</p>\r\n<br><p align=center><img src='logos/unity2'></p>\r\n        ", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=center>Now we try to make something dynamic inside text markup...</p>\r\n<br><p align=center valign=middle>Due to performance we can not parse text every frame <img src='smiles/sad'>, but we can reserve some place to draw things inside compiled html! <img src='smiles/cool'></p>\r\n<br><p align=center>It's possible with img tag. Look at source.</p>\r\n<br><p align=center><img src='#time'></p>\r\n<br><p align=center>With same technique we can render animated pictures and even results from render targets.</p>\r\n", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<br><p align=center>Links support</p>\r\n<br><p align=left valign=middle>1)&nbsp;<a href='plaintextlink' id='simple'>Simple plain text link.</a></p>\r\n<br><p align=left valign=middle>2)&nbsp;<a href='textandimage'>Simple text and <img src='smiles/smile'> image link.</a></p>\r\n<br><p align=left valign=middle>3)&nbsp;<a href='biglink1'>Multiline link <img src='smiles/smile'>.</a>&nbsp;<a href='biglink2'>Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>.</a></p>\r\n<br><br><p align=center>Try to click around and see to left-bottom corner for results</p>\r\n<br><br><p align=center>At last we have some basic stuff to interact with player</p>\r\n", "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br><br><p align=center>Some stuff using Unity3DGUI.Label here</p>\r\n<br><br><p align=center>Below is totally dynamic html text, and statistics for compile and drawing</p>\r\n" };

	private HtCompiler compiler;

	private readonly int width = Screen.width / 2 - 5;

	private int height;

	private string currentLink;

	private string html
	{
		get
		{
			return _html;
		}
		set
		{
			_html = value;
			changed = true;
		}
	}

	public void Awake()
	{
		Debug.Log("Initializing Demo");
		HtEngine.RegisterLogger(new Unity3DLogger());
		HtEngine.RegisterDevice(new Unity3DDevice());
		compiler = HtEngine.GetCompiler();
		html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\r\n<br>\r\n<p id='introduction' align=center>This is based on Profixy's HTMLEngine-Mini.<br>URL:&nbsp;<u>http://html-engine-mini.googlecode.com/</u><br></p>\r\n<br><p><i>Here is no any &lt;html&gt;, &lt;body&gt; etc tags. Only small subset of tags supported yet:</i></p>\r\n<br>\r\n<p><code>- &lt;<font color=yellow>p</font>&nbsp;[id='...'] [align=(left | right | center | justify)]<br>[valign=(top | middle | bottom)]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>spin</font>&nbsp;[id='...'] [width=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>img</font>&nbsp;src='...' [id='...'] [fps=...] [width=...] [height=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>font</font>&nbsp;[face=...] [size=...] [color=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>effect</font>&nbsp;name=... [amount=...] [color=...]&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>br</font>&gt; &lt;<font color=yellow>b</font>&gt; &lt;<font color=yellow>i</font>&gt; &lt;<font color=yellow>u</font>&gt; &lt;<font color=yellow>s</font>&gt; &lt;<font color=yellow>code</font>&gt;</code></p>\r\n<p><code>- &lt;<font color=yellow>a</font>&nbsp;href='...'&gt;</code></p>\r\n<br>\r\n<p><i>Also this demo contains some internal resources (fonts and images) than can be used.</i></p>\r\n<br>\r\n<p><b>Available fonts:&nbsp;</b>'default16', 'default16b', 'default16bi', 'default16i', 'title24'</p>\r\n<p><b>Available atleses:&nbsp;</b>'smiles', 'logos', 'faces'</p>\r\n";
	}

	public void Update()
	{
		if (changed)
		{
			compiler.Compile(_html, width);
			height = compiler.CompiledHeight;
			changed = false;
		}
	}

	public void OnGUI()
	{
		float num = 0f;
		if (Event.current.type == EventType.Repaint)
		{
			num = Time.realtimeSinceStartup;
		}
		bar = GUI.Toolbar(new Rect(5f, 5f, Screen.width - 10, 40f), bar, barTexts);
		if (GUI.changed)
		{
			html = barHtmls[bar];
		}
		string text = GUI.TextArea(new Rect(5 + width + 5, 50f, width - 5, Screen.height - 50 - 5), html);
		if (GUI.changed && text != html)
		{
			html = text;
		}
		if (Event.current.type == EventType.Repaint)
		{
			Rect rect = default(Rect);
			rect = new Rect(5f, 50f, width, height);
			GUI.BeginGroup(rect);
			compiler.Draw(Time.deltaTime);
			GUI.EndGroup();
			if (bar == 7)
			{
				Unity3DGUI.Label(new Rect(10f, 300f, width, 400f), string.Format("<p align=center>Dynamic html text! Current time:<br>\r\n<font color=lime><code>{0}</code></font><br><br>\r\nDo not use that often, coz compile time will eat some FPS<br>\r\nCompiling time of this text:<br>\r\n<font color=yellow><code>{1:F4} ms</code></font><br>\r\nAnd drawing time of this text:<br><font color=yellow><code>{2:F4} ms</code></font></p>", DateTime.Now, Unity3DGUI.lastCompilerTookSeconds * 1000f, Unity3DGUI.lastDrawTookSeconds * 1000f));
			}
		}
		if (Event.current.type == EventType.MouseUp)
		{
			currentLink = compiler.GetLink((int)Event.current.mousePosition.x - 5, (int)Event.current.mousePosition.y - 50);
			if (currentLink != null)
			{
				Debug.Log("Link clicked: " + currentLink);
			}
			else
			{
				currentLink = "no links here";
				Debug.Log("No links");
			}
		}
		if (Event.current.type == EventType.Repaint)
		{
			num = Time.realtimeSinceStartup - num;
			GUI.Label(new Rect(10f, Screen.height - 30, Screen.width, 30f), string.Format("OnGUI time (Repaint): {0:F4}ms. Current link: {1}", num * 1000f, currentLink));
		}
	}

	public void OnDestroy()
	{
		if (compiler != null)
		{
			compiler.Dispose();
			compiler = null;
		}
	}
}
