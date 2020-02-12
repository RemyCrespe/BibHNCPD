/*******************************
** RICOU Julie
** Jeudi 23 janvier
** Gere le cycle jour / nuit
** 
** Parametres :
** _dayDuration : duree du cycle
** _lights : lumieres (soleil et lune)
** _skyboxe : ciel (skybox)
** _fogColorChangeSpeed : vitesse de changement de couleur du brouillard
** _fogMaxDist : distance du brouillard (intensite) pour differents temps
** _fogColor : couleur du brouillard pour differents temps
*******************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class DayNight : Singleton<DayNight>
{
    [SerializeField]
    float _dayDuration;
    
    public bool p_isDay;

    [SerializeField]
    GameObject[] _lights;

    [SerializeField]
    Material _skyboxe;

    float _timer = 0; //permet de savoir ou on est dans le cycle

    Vector3 _skyboxColor;

    [SerializeField] float _fogColorChangeSpeed;

    [SerializeField] float _fogMaxDistDay;
    [SerializeField] float _fogMaxDistNight;
    [SerializeField] float _fogMaxDistMorning;
    [SerializeField] float _fogMaxDistEvening;
    [SerializeField] Color _fogColorDay;
    [SerializeField] Color _fogColorNight;
    [SerializeField] Color _fogColorMorning;
    [SerializeField] Color _fogColorEvening;

    Vector3 _colorTemporaire; //stock la couleur du brouillard lors de degrade

    void Start()
    {
        p_isDay = true;
        _skyboxColor = new Vector3(0, 0, 0);
        _colorTemporaire = new Vector3(RenderSettings.fogColor.r, RenderSettings.fogColor.g, RenderSettings.fogColor.b);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        transform.Rotate(new Vector3(180/(_dayDuration/Time.deltaTime),0,0));

        if(_timer >= _dayDuration)
        {
            _timer = 0;
            p_isDay = !p_isDay;
            ChangeSky();
        }

        if(p_isDay)
        {
            if (_timer >= _dayDuration - (6*(_dayDuration/60)) && _timer < _dayDuration - (4 * (_dayDuration / 60)))
            {

                if (RenderSettings.fogEndDistance >= _fogMaxDistEvening) RenderSettings.fogEndDistance -= 10f;
                _colorTemporaire = Vector3.MoveTowards(_colorTemporaire, new Vector3(_fogColorEvening.r, _fogColorEvening.g, _fogColorEvening.b), _fogColorChangeSpeed);
                RenderSettings.fogColor = new Color(_colorTemporaire.x, _colorTemporaire.y, _colorTemporaire.z);

                if (_skyboxe.GetColor("_BaseColor") != new Color(0.2f, 0, 0.35f))
                {
                    _skyboxColor = Vector3.MoveTowards(_skyboxColor, new Vector3(0.2f, 0, 0.35f), 0.002f);
                    _skyboxe.SetColor("_BaseColor", new Color(_skyboxColor.x, _skyboxColor.y, _skyboxColor.z));
                }
            }
            else if (_timer >= _dayDuration - (4 * (_dayDuration / 60)))
            {

                if(RenderSettings.fogEndDistance >= _fogMaxDistEvening) RenderSettings.fogEndDistance -= 10f;
                _colorTemporaire = Vector3.MoveTowards(_colorTemporaire, new Vector3(_fogColorEvening.r, _fogColorEvening.g, _fogColorEvening.b), _fogColorChangeSpeed);
                RenderSettings.fogColor = new Color(_colorTemporaire.x, _colorTemporaire.y, _colorTemporaire.z);

                if (_skyboxe.GetColor("_BaseColor") != new Color(0, 0, 0))
                {
                    _skyboxColor = Vector3.MoveTowards(_skyboxColor, new Vector3(0, 0, 0), 0.004f);
                    _skyboxe.SetColor("_BaseColor", new Color(_skyboxColor.x, _skyboxColor.y, _skyboxColor.z));
                }
            }
            else if (_timer <= _dayDuration - (55 * (_dayDuration / 60)))
            {

                if (RenderSettings.fogEndDistance <= _fogMaxDistDay) RenderSettings.fogEndDistance += 10f;
                _colorTemporaire = Vector3.MoveTowards(_colorTemporaire, new Vector3(_fogColorDay.r, _fogColorDay.g, _fogColorDay.b), _fogColorChangeSpeed);
                RenderSettings.fogColor = new Color(_colorTemporaire.x, _colorTemporaire.y, _colorTemporaire.z);

                if (_skyboxe.GetColor("_BaseColor") != new Color(0.2f, 0, 0.35f))
                {
                    _skyboxColor = Vector3.MoveTowards(_skyboxColor, new Vector3(0.2f, 0, 0.35f), 0.002f);
                    _skyboxe.SetColor("_BaseColor", new Color(_skyboxColor.x, _skyboxColor.y, _skyboxColor.z));
                }
            }
            else if (_timer > _dayDuration - (55 * (_dayDuration / 60)) && _timer <= _dayDuration - (52 * (_dayDuration / 60)))
            {

                if (RenderSettings.fogEndDistance <= _fogMaxDistDay) RenderSettings.fogEndDistance += 10f;
                _colorTemporaire = Vector3.MoveTowards(_colorTemporaire, new Vector3(_fogColorDay.r, _fogColorDay.g, _fogColorDay.b), _fogColorChangeSpeed);
                RenderSettings.fogColor = new Color(_colorTemporaire.x, _colorTemporaire.y, _colorTemporaire.z);

                if (_skyboxe.GetColor("_BaseColor") != new Color(0, 0.7f, 1))
                {
                    _skyboxColor = Vector3.MoveTowards(_skyboxColor, new Vector3(0, 0.7f, 1), 0.005f);
                    _skyboxe.SetColor("_BaseColor", new Color(_skyboxColor.x, _skyboxColor.y, _skyboxColor.z));
                }
            }

        }

        else
        {
            if (_timer >= _dayDuration - (6 * (_dayDuration / 60)))
            {

                if (RenderSettings.fogEndDistance >= _fogMaxDistMorning) RenderSettings.fogEndDistance -= 10f;
                _colorTemporaire = Vector3.MoveTowards(_colorTemporaire, new Vector3(_fogColorMorning.r, _fogColorMorning.g, _fogColorMorning.b), _fogColorChangeSpeed);
                RenderSettings.fogColor = new Color(_colorTemporaire.x, _colorTemporaire.y, _colorTemporaire.z);

                if (_skyboxe.GetColor("_BaseColor") != new Color(0, 0, 0))
                {
                    _skyboxColor = Vector3.MoveTowards(_skyboxColor, new Vector3(0, 0, 0), 0.008f);
                    _skyboxe.SetColor("_BaseColor", new Color(_skyboxColor.x, _skyboxColor.y, _skyboxColor.z));
                }
            }
            else if (_timer <= _dayDuration - (54 * (_dayDuration / 60)))
            {

                if (RenderSettings.fogEndDistance >= _fogMaxDistNight) RenderSettings.fogEndDistance -= 10f;
                _colorTemporaire = Vector3.MoveTowards(_colorTemporaire, new Vector3(_fogColorNight.r, _fogColorNight.g, _fogColorNight.b), _fogColorChangeSpeed);
                RenderSettings.fogColor = new Color(_colorTemporaire.x, _colorTemporaire.y, _colorTemporaire.z);

                if (_skyboxe.GetColor("_BaseColor") != new Color(0.5f, 0.5f, 0.5f))
                {
                    _skyboxColor = Vector3.MoveTowards(_skyboxColor, new Vector3(0.5f, 0.5f, 0.5f), 0.008f);
                    _skyboxe.SetColor("_BaseColor", new Color(_skyboxColor.x, _skyboxColor.y, _skyboxColor.z));
                }
            }
        }
    }

    void ChangeSky()
    {
        if (p_isDay)
        {
            _dayDuration *= 2;
            RenderSettings.sun = _lights[0].GetComponent<Light>();
        }
        else
        {
            _dayDuration /= 2; //divise par 2 la duree du cycle pour que la nuit soir plus courte que le jour
            RenderSettings.sun = _lights[1].GetComponent<Light>();
        }
    }
}
