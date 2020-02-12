/*******************************
** RICOU Julie
** Jeudi 30 janvier
** Gere l’eclairage des phares la nuit
*******************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLight : MonoBehaviour
{
    DayNight _dN;
    bool _isDay = false;
    public Material _lightMat;

    [SerializeField]
    float _maxEdge;

    [SerializeField]
    float _maxIntensity;


    void Start()
    {
        _dN = DayNight.P_instance;
    }

    void Update()
    {
        if (_dN.p_isDay != _isDay)
        {
            changeRobotLight();
        }
    }

    void changeRobotLight()
    {
        if (!_isDay)
        {
            if (_lightMat.GetFloat("Vector1_32CA2FEE") > 0)
            {
               _lightMat.SetFloat("Vector1_32CA2FEE", _lightMat.GetFloat("Vector1_32CA2FEE") - 0.0005f);
            }
            else
            {
                _isDay = _dN.p_isDay;
            }
        }
        else
        {
            if (_lightMat.GetFloat("Vector1_32CA2FEE") < _maxEdge)
            {
                _lightMat.SetFloat("Vector1_32CA2FEE", _lightMat.GetFloat("Vector1_32CA2FEE") + 0.0005f);
            }
            else
            {
                _isDay = _dN.p_isDay;
            }
        }

    }
}
