using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLight : MonoBehaviour
{
    DayNight _dN;
    bool _isDay = false;
    Light _light;
    public Material _lightMat;

    [SerializeField]
    float _maxEdge;

    [SerializeField]
    float _maxIntensity;


    void Start()
    {
        _dN = DayNight.P_instance;
        _light = GetComponent<Light>();
    }

    void Update()
    {
        if (_dN.p_isDay != _isDay) changeRobotLight();
    }

    void changeRobotLight()
    {
        if (!_isDay)
        {
            if (_light.intensity > 0)
            {
                if (_lightMat.GetFloat("Vector1_32CA2FEE") > 0) _lightMat.SetFloat("Vector1_32CA2FEE", _lightMat.GetFloat("Vector1_32CA2FEE") - 0.0005f);
                _light.intensity -= 0.2f;
            }
            else
            {
                _isDay = _dN.p_isDay;
            }
        }
        else
        {
            if (_light.intensity < _maxIntensity)
            {
                if (_lightMat.GetFloat("Vector1_32CA2FEE") < _maxEdge) _lightMat.SetFloat("Vector1_32CA2FEE", _lightMat.GetFloat("Vector1_32CA2FEE") + 0.0005f);
                _light.intensity += 0.2f;
            }
            else
            {
                _isDay = _dN.p_isDay;
            }
        }

    }
}
