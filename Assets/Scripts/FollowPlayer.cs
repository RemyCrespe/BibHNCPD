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
    
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z);
    }
}
