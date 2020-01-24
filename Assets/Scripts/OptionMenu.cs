/*************************
MALLEM Laura
20/01/2020
Script de fonctions du menu option
	-liste les résolutions possible de la machine
	-resize la fenetre selon la résolution choisi par l'utilisateur
	-fonction volume (float)
	-fonction plein écran (boolean)
*****************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

	public AudioMixer P_audioMixer;

	public Dropdown P_resolutionDropdown;

	private Resolution[] _resolutions;

	
	void Start ()
	{
		_resolutions = Screen.resolutions;

		P_resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;

		for (int i=0; i < _resolutions.Length; i++)
		{
			string option = _resolutions[i].width + "x" + _resolutions[i].height;
			options.Add(option);
	
			if (_resolutions[i].width == Screen.currentResolution.width &&
			    _resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			};
		};


		P_resolutionDropdown.AddOptions(options);
		P_resolutionDropdown.value = currentResolutionIndex;
		P_resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = _resolutions[resolutionIndex];

		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

   	public void SetVolume(float volume)
	{
		P_audioMixer.SetFloat("volume", volume);
	}

	public void SetFullScreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

}
