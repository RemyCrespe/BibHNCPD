using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*******************************
** RICOU Julie
** Jeudi 23 janvier
** Gere la disparition du blueprint apres le mini jeu
** 
** Parametres :
** p_dissolve : permet de savoir si le blueprint doit être dissout ou non
** _dissolveState : permet de savoir ou on en est dans la dissolution
*******************************/



public class MiniGameDissolve : MonoBehaviour
{
    public bool p_dissolve;
    float _dissolveState = 0;
    void Update()
    {
        if (p_dissolve && _dissolveState <= 2.1f) Dissolve();
    }

    void Dissolve()
    {
        _dissolveState += 0.02f;
        GetComponent<Renderer>().material.SetFloat("_dissolve", _dissolveState);
    }
}
