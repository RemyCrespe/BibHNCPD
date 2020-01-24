using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*******************************
** RICOU Julie
** Vendredi 24 janvier
** Gere le deplacement des elements de meteo en fonction du joueur
** 
** Parametres :
** _player : le joueur
*******************************/


public class FollowPlayer : MonoBehaviour
{ 
    [SerializeField]
    GameObject _player;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float _smoothFactor = 0.5f;

    void LateUpdate()
    {

        transform.position = Vector3.Slerp(transform.position, _player.transform.position, _smoothFactor);
    }
}
