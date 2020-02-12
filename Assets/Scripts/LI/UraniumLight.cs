/*******************************
** RICOU Julie
** Jeudi 23 janvier
** Gere la radiation de l uranium
** 
** Parametres :
** _minIntensity : intensite minimum de la radiation
** _maxIntensity : intensite maximum de la radiation
** _duree : duree de la radiation
*******************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UraniumLight : MonoBehaviour
{
    [SerializeField] float _minIntensity;
    [SerializeField] float _maxIntensity;
    [SerializeField] float _duree;

    bool _moveToMax = false;


    void Update()
    {
        if (_moveToMax)
        {
            GetComponent<Renderer>().material.SetFloat("Vector1_18779E34", GetComponent<Renderer>().material.GetFloat("Vector1_18779E34") +1);
            GetComponent<Renderer>().material.SetFloat("Vector1_18779E34", Mathf.Lerp(0,1,GetComponent<Renderer>().material.GetFloat("Vector1_18779E34") - _duree / (_maxIntensity - _minIntensity)));
            GetComponent<Renderer>().material.SetFloat("Vector1_18779E34", GetComponent<Renderer>().material.GetFloat("Vector1_18779E34") - 1);
            GetComponent<Light>().intensity += _duree / (_maxIntensity - _minIntensity);
            if (GetComponent<Light>().intensity >= _maxIntensity) _moveToMax = false;
        }
        else
        {
            GetComponent<Renderer>().material.SetFloat("Vector1_18779E34", GetComponent<Renderer>().material.GetFloat("Vector1_18779E34") +1);
            GetComponent<Renderer>().material.SetFloat("Vector1_18779E34", Mathf.Lerp(0,1, GetComponent<Renderer>().material.GetFloat("Vector1_18779E34") + _duree / (_maxIntensity - _minIntensity)));
            GetComponent<Renderer>().material.SetFloat("Vector1_18779E34", GetComponent<Renderer>().material.GetFloat("Vector1_18779E34") - 1);
            GetComponent<Light>().intensity -= _duree / (_maxIntensity - _minIntensity);
            if (GetComponent<Light>().intensity <= _minIntensity) _moveToMax = true;
        }
    }
}
