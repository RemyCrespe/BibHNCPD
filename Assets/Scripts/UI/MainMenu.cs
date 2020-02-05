/**********************************
MALLEM Laura
22/01/2020
Script de fonctions du menu principale
	-PlayGame -> lance la scène "SampleScene" (à modifier)
	-QuitGame -> ferme la fenêtre
***********************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("Unity17");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
