/**************************************************************************
 ** Nathan Lefebvre
 **
 ** 23 - 01 - 2020
 **
 ** Manager qui permet de faire spawn le joueur dans la room choisi
 *************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _GOplayer;
    [SerializeField] private Transform _Tplayer;
    [SerializeField] private Transform _spw1;
    [SerializeField] private Transform _spw2;
    [SerializeField] private Transform _spw3;
    private int _nbRoom;

    private void Awake()
    {
        _nbRoom = GameManager.Instance._GMnbRoom;
    }

    private void Update()
    {
        RoomChosen(_nbRoom);
    }

    private void RoomChosen(int nbRoom)
    {
       
        switch (nbRoom)
        {
            case 1:
                _Tplayer.position = _spw1.position;
                _GOplayer.SetActive(true);     
                break;
            case 2:
                _Tplayer.position = _spw2.position;
                _GOplayer.SetActive(true);
                break;
            case 3:
                _Tplayer.position = _spw3.position;
                _GOplayer.SetActive(true);
                break;
        }
    }
}
