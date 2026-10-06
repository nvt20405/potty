using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
public class DownloadObbExample : MonoBehaviour
{
	private string expPath;

	private string logtxt;

	private bool alreadyLogged;

	private string nextScene = "GameClient";

	private bool downloadStarted;

	public GameObject downloadGroup;

	private void log(string t)
	{
		logtxt = logtxt + t + "\n";
		MonoBehaviour.print("MYLOG " + t);
	}

	private void Start()
	{
		SceneManager.LoadScene(nextScene);
	}

	public void OnDownloadClick()
	{
		GooglePlayDownloader.FetchOBB();
		StartCoroutine(loadLevel());
	}

	protected IEnumerator loadLevel()
	{
		string mainPath;
		do
		{
			yield return new WaitForSeconds(0.5f);
			mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
			log("waiting mainPath " + mainPath);
		}
		while (mainPath == null);
		if (!downloadStarted)
		{
			downloadStarted = true;
			string uri = "file://" + mainPath;
			log("downloading " + uri);
			WWW www = WWW.LoadFromCacheOrDownload(uri, 0);
			yield return www;
			if (www.error != null)
			{
				log("wwww error " + www.error);
			}
			else
			{
				SceneManager.LoadScene(nextScene);
			}
		}
		SceneManager.LoadScene(nextScene);
	}
}
