using System;
using HTMLEngine;
using HTMLEngine.NGUI;
using HTMLEngine.Unity3D;
using UnityEngine;

public class NGUIDemo : MonoBehaviour
{
	private const string demo0 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<p id='introduction' align=center>This is based on Profixy's HTMLEngine-Mini.<br>URL:&nbsp;<u>http://html-engine-mini.googlecode.com/</u><br></p>\n<br><p><i>Here is no any &lt;html&gt;, &lt;body&gt; etc tags. Only small subset of tags supported yet:</i></p>\n<br>\n<p><code>- &lt;<font color=yellow>p</font>&nbsp;[id='...'] [align=(left | right | center | justify)]<br>[valign=(top | middle | bottom)]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>spin</font>&nbsp;[id='...'] [width=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>img</font>&nbsp;src='...' [id='...'] [fps=...] [width=...] [height=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>font</font>&nbsp;[face=...] [size=...] [color=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>effect</font>&nbsp;name=... [amount=...] [color=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>br</font>&gt; &lt;<font color=yellow>b</font>&gt; &lt;<font color=yellow>i</font>&gt; &lt;<font color=yellow>u</font>&gt; &lt;<font color=yellow>s</font>&gt; &lt;<font color=yellow>code</font>&gt;</code></p>\n<p><code>- &lt;<font color=yellow>a</font>&nbsp;href='...'&gt;</code></p>\n<br>\n<p><i>Also this demo contains some internal resources (fonts and images) than can be used.</i></p>\n<br>\n<p><b>Available fonts:&nbsp;</b>'default16', 'default16b', 'default16bi', 'default16i', 'title24'</p>\n<p><b>Available atleses:&nbsp;</b>'smiles', 'logos', 'faces'</p>\n";

	private const string demo1 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<p align=left>Without effect:</p>\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\n<p align=left>Shadow effect:</p>\n<effect name=shadow color=black>\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\n</effect>\n<p align=left>Outline effect:</p>\n<effect name=outline color=black>\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\n</effect>\n";

	private const string demo2 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<font size=24>\n<br><spin id='outlined' align=center><effect name=outline color=#FFFFFF80><font color=black>Outlined text</font></effect></spin>\n<p align=center><effect name=outline color=yellow><font color=black>Outlined yellow text</font></effect></p>\n<p align=center><effect name=outline color=#FFFFFF80 amount=2>Some stuppid effect i got</effect></p>\n<p align=center><effect name=shadow>Default shadowed text</effect></p>\n<p align=center><effect name=shadow color=black>Strong-shadowed text</effect></p>\n<p align=center><effect name=shadow color=#FFFFFF80 amount=2>Some shadowed text</effect></p>\n</font>\n    ";

	private const string demo3 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=justify>Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text.</p>\n<br><p align=center><font color=gray>Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text.</font></p>\n<br><p align=right>Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text.</p>\n<br><p align=left><font color=gray>Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text.</font></p>\n        ";

	private const string demo4 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=center valign=top>Picture <img src='smiles/sad'> with &lt;p valign=top&gt;</p>\n<br><p align=center valign=middle>Picture <img src='smiles/smile'> with &lt;p valign=middle&gt; much better than others in this case <img src='smiles/cool'></p>\n<br><p align=center valign=bottom>Picture <img src='smiles/sad'> with &lt;p valign=bottom&gt;</p>\n<br><p align=center valign=bottom>Picture <img src='faces/power_' fps=10 id='anim'> with &lt;img fps=10&gt;</p>\n<br><p align=justify valign=bottom><img src='logos/unity'> is a feature rich, fully integrated development engine for the creation of interactive 3D content. It provides complete, out-of-the-box functionality to assemble high-quality, high-performing content and publish to multiple platforms.</p>\n<br><p align=center><img src='logos/unity2'></p>\n        ";

	private const string demo5 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=center>Now we try to make something dynamic inside text markup...</p>\n<br><p align=center valign=middle>Due to performance we can not parse text every frame <img src='smiles/sad'>, but we can reserve some place to draw things inside compiled html! <img src='smiles/cool'></p>\n<br><p align=center>It's possible with img tag. Look at source.</p>\n<br><p align=center><img src='#time'></p>\n<br><p align=center>With same technique we can render animated pictures and even results from render targets.</p>\n";

	private const string demo6 = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=center>Links support</p>\n<br><p align=left valign=middle>1)&nbsp;<a href='plaintextlink' id='simple'>Simple plain text link.</a></p>\n<br><p align=left valign=middle>2)&nbsp;<a href='textandimage'>Simple text and <img src='smiles/smile'> image link.</a></p>\n<br><p align=left valign=middle>3)&nbsp;<a href='biglink1'>Multiline link <img src='smiles/smile'>.</a>&nbsp;<a href='biglink2'>Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>.</a></p>\n<br><br><p align=center>Try to click around and see to left-bottom corner for results</p>\n<br><br><p align=center>At last we have some basic stuff to interact with player</p>\n";

	public UILabel lastLinkText;

	public UIScrollBar scrollBar;

	private NGUIHTML html;

	private bool updateTime;

	public void Awake()
	{
		Debug.Log("Initializing Demo");
		HtEngine.RegisterLogger(new Unity3DLogger());
		HtEngine.RegisterDevice(new NGUIDevice());
		HtEngine.LinkHoverColor = HtColor.Parse("#FF4444");
		HtEngine.LinkPressedFactor = 0.5f;
		HtEngine.LinkFunctionName = "onLinkClicked";
		html = GetComponent<NGUIHTML>();
		html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<p id='introduction' align=center>This is based on Profixy's HTMLEngine-Mini.<br>URL:&nbsp;<u>http://html-engine-mini.googlecode.com/</u><br></p>\n<br><p><i>Here is no any &lt;html&gt;, &lt;body&gt; etc tags. Only small subset of tags supported yet:</i></p>\n<br>\n<p><code>- &lt;<font color=yellow>p</font>&nbsp;[id='...'] [align=(left | right | center | justify)]<br>[valign=(top | middle | bottom)]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>spin</font>&nbsp;[id='...'] [width=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>img</font>&nbsp;src='...' [id='...'] [fps=...] [width=...] [height=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>font</font>&nbsp;[face=...] [size=...] [color=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>effect</font>&nbsp;name=... [amount=...] [color=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>br</font>&gt; &lt;<font color=yellow>b</font>&gt; &lt;<font color=yellow>i</font>&gt; &lt;<font color=yellow>u</font>&gt; &lt;<font color=yellow>s</font>&gt; &lt;<font color=yellow>code</font>&gt;</code></p>\n<p><code>- &lt;<font color=yellow>a</font>&nbsp;href='...'&gt;</code></p>\n<br>\n<p><i>Also this demo contains some internal resources (fonts and images) than can be used.</i></p>\n<br>\n<p><b>Available fonts:&nbsp;</b>'default16', 'default16b', 'default16bi', 'default16i', 'title24'</p>\n<p><b>Available atleses:&nbsp;</b>'smiles', 'logos', 'faces'</p>\n";
	}

	public void FixedUpdate()
	{
		if (!updateTime)
		{
			return;
		}
		foreach (Transform item in base.transform)
		{
			Transform transform2 = item;
			if (transform2.name == "time")
			{
				UILabel component = transform2.GetComponent<UILabel>();
				if (component != null)
				{
					DateTime now = DateTime.Now;
					component.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", now.Hour, now.Minute, now.Second, now.Millisecond);
				}
			}
		}
	}

	internal void onBtnDemoClicked(GameObject senderGo)
	{
		updateTime = false;
		switch (senderGo.name)
		{
		case "BtnDemo1":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<p id='introduction' align=center>This is based on Profixy's HTMLEngine-Mini.<br>URL:&nbsp;<u>http://html-engine-mini.googlecode.com/</u><br></p>\n<br><p><i>Here is no any &lt;html&gt;, &lt;body&gt; etc tags. Only small subset of tags supported yet:</i></p>\n<br>\n<p><code>- &lt;<font color=yellow>p</font>&nbsp;[id='...'] [align=(left | right | center | justify)]<br>[valign=(top | middle | bottom)]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>spin</font>&nbsp;[id='...'] [width=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>img</font>&nbsp;src='...' [id='...'] [fps=...] [width=...] [height=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>font</font>&nbsp;[face=...] [size=...] [color=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>effect</font>&nbsp;name=... [amount=...] [color=...]&gt;</code></p>\n<p><code>- &lt;<font color=yellow>br</font>&gt; &lt;<font color=yellow>b</font>&gt; &lt;<font color=yellow>i</font>&gt; &lt;<font color=yellow>u</font>&gt; &lt;<font color=yellow>s</font>&gt; &lt;<font color=yellow>code</font>&gt;</code></p>\n<p><code>- &lt;<font color=yellow>a</font>&nbsp;href='...'&gt;</code></p>\n<br>\n<p><i>Also this demo contains some internal resources (fonts and images) than can be used.</i></p>\n<br>\n<p><b>Available fonts:&nbsp;</b>'default16', 'default16b', 'default16bi', 'default16i', 'title24'</p>\n<p><b>Available atleses:&nbsp;</b>'smiles', 'logos', 'faces'</p>\n";
			break;
		case "BtnDemo2":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<p align=left>Without effect:</p>\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\n<p align=left>Shadow effect:</p>\n<effect name=shadow color=black>\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\n</effect>\n<p align=left>Outline effect:</p>\n<effect name=outline color=black>\n<p align=center>Normal text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></p>\n<p align=center><b>Bold text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></b></p>\n<p align=center><i>Italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></p>\n<p align=center><b><i>Bold and italic text&nbsp;<u>underlined</u>&nbsp;<s>striked</s></i></b></p>\n</effect>\n";
			break;
		case "BtnDemo3":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<font size=24>\n<br><spin id='outlined' align=center><effect name=outline color=#FFFFFF80><font color=black>Outlined text</font></effect></spin>\n<p align=center><effect name=outline color=yellow><font color=black>Outlined yellow text</font></effect></p>\n<p align=center><effect name=outline color=#FFFFFF80 amount=2>Some stuppid effect i got</effect></p>\n<p align=center><effect name=shadow>Default shadowed text</effect></p>\n<p align=center><effect name=shadow color=black>Strong-shadowed text</effect></p>\n<p align=center><effect name=shadow color=#FFFFFF80 amount=2>Some shadowed text</effect></p>\n</font>\n    ";
			break;
		case "BtnDemo4":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=justify>Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text. Justify aligned text.</p>\n<br><p align=center><font color=gray>Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text. Centered text.</font></p>\n<br><p align=right>Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text. Right aligned text.</p>\n<br><p align=left><font color=gray>Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text. Left aligned text.</font></p>\n        ";
			break;
		case "BtnDemo5":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=center valign=top>Picture <img src='smiles/sad'> with &lt;p valign=top&gt;</p>\n<br><p align=center valign=middle>Picture <img src='smiles/smile'> with &lt;p valign=middle&gt; much better than others in this case <img src='smiles/cool'></p>\n<br><p align=center valign=bottom>Picture <img src='smiles/sad'> with &lt;p valign=bottom&gt;</p>\n<br><p align=center valign=bottom>Picture <img src='faces/power_' fps=10 id='anim'> with &lt;img fps=10&gt;</p>\n<br><p align=justify valign=bottom><img src='logos/unity'> is a feature rich, fully integrated development engine for the creation of interactive 3D content. It provides complete, out-of-the-box functionality to assemble high-quality, high-performing content and publish to multiple platforms.</p>\n<br><p align=center><img src='logos/unity2'></p>\n        ";
			break;
		case "BtnDemo6":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=center>Now we try to make something dynamic inside text markup...</p>\n<br><p align=center valign=middle>Due to performance we can not parse text every frame <img src='smiles/sad'>, but we can reserve some place to draw things inside compiled html! <img src='smiles/cool'></p>\n<br><p align=center>It's possible with img tag. Look at source.</p>\n<br><p align=center><img src='#time'></p>\n<br><p align=center>With same technique we can render animated pictures and even results from render targets.</p>\n";
			updateTime = true;
			break;
		case "BtnDemo7":
			html.html = "<p align=center><font face=title size=24><font color=yellow>HTMLEngine</font>&nbsp;for&nbsp;<font color=lime>Unity3D.GUI</font>&nbsp;and&nbsp;<font color=lime>NGUI</font></font></p>\n<br>\n<br><p align=center>Links support</p>\n<br><p align=left valign=middle>1)&nbsp;<a href='plaintextlink' id='simple'>Simple plain text link.</a></p>\n<br><p align=left valign=middle>2)&nbsp;<a href='textandimage'>Simple text and <img src='smiles/smile'> image link.</a></p>\n<br><p align=left valign=middle>3)&nbsp;<a href='biglink1'>Multiline link <img src='smiles/smile'>.</a>&nbsp;<a href='biglink2'>Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>. Multiline link <img src='smiles/smile'>.</a></p>\n<br><br><p align=center>Try to click around and see to left-bottom corner for results</p>\n<br><br><p align=center>At last we have some basic stuff to interact with player</p>\n";
			break;
		}
	}

	internal void onLinkClicked(GameObject senderGo)
	{
		NGUILinkText component = senderGo.GetComponent<NGUILinkText>();
		if (component != null)
		{
			Debug.Log(component.linkText);
			if (lastLinkText != null)
			{
				lastLinkText.text = "Last Link Text: [FFFF00]" + component.linkText + "[-]";
			}
		}
	}
}
