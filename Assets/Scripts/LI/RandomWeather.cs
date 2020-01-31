using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*******************************
** RICOU Julie
** Vendredi 24 janvier
** Gere le changement aleatoire de meteo
*******************************/


public class RandomWeather : MonoBehaviour
{

    int[] _weather; //0 Rain, 1 SandStorm, 2 Clouds, 3 Clear

    int _currentWeather;

    [SerializeField]
    GameObject[] _weatherObjects; //0 Rain, 1 SandStorm, 2 Clouds

    [SerializeField]
    int _minTempsChangeWeather;

    [SerializeField]
    int _maxTempsChangeWeather;

    void Start()
    {
        _currentWeather = 0;
        ChangeWeather();
    }

    void ChangeWeather()
    {
        _weatherObjects[_currentWeather].SetActive(false);
        var main = _weatherObjects[_currentWeather].GetComponent<ParticleSystem>().main;
        main.loop = true;
        if (_currentWeather == 0)
        {
            main = _weatherObjects[_currentWeather].GetComponentInChildren<ParticleSystem>().main;
            main.loop = true;
        }
        int _lastWeather = _currentWeather;
        while (_lastWeather == _currentWeather) _currentWeather = Random.Range(0, 4);
        _weatherObjects[_currentWeather].SetActive(true);
        Invoke("EndWeather", Random.Range(_minTempsChangeWeather, _maxTempsChangeWeather));
    }

    void EndWeather()
    {
        var main = _weatherObjects[_currentWeather].GetComponent<ParticleSystem>().main;
        main.loop = false;
        if (_currentWeather == 0)
        {
            main = _weatherObjects[_currentWeather].GetComponentInChildren<ParticleSystem>().main;
            main.loop = false;
        }
        Invoke("ChangeWeather", 32);
    }
}
