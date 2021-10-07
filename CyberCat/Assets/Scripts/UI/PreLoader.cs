using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
	private CanvasGroup fadeGroup;
	private float loadTime;
	private float minumumLogoTime = 3.0f;  // Sahnenin minumum zaman aralığı .

	int a;
	private void Start()
	{

		//PlayerPrefs.DeleteAll();

		//Sahnedeki canvas grubu buluyoruz
		fadeGroup = FindObjectOfType<CanvasGroup>();
		// Arka plan ekranını getirme aralığı 
		fadeGroup.alpha = 1;

		// Pre load the game 

		// if loadtime is super , give it a small buffer time so we can apreciate the logo
		if (Time.time < minumumLogoTime)
			loadTime = minumumLogoTime;
		else
			loadTime = Time.time;

	}
	private void Update()
	{
		// Fade-in
		if (Time.time < minumumLogoTime)
		{
			fadeGroup.alpha = 1 - Time.time;
		}

		// Fade-out
		if (Time.time > minumumLogoTime && loadTime != 0)
		{
			fadeGroup.alpha = Time.time - minumumLogoTime;
			if (fadeGroup.alpha >= 1)
			{

				int b = PlayerPrefs.GetInt("level");

				if (b == 0)
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("DialogScene");
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
					Debug.Log(PlayerPrefs.GetInt("level"));
				}

			}
		}
	}

}