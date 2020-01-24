/*******************************************
** Aucouturier Romuald
** 13-01-2020
** 
** Singleton
** Sert pour l'instance de classe
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T P_instance { get; private set; }

    [SerializeField]
    private bool _dontDestroyOnLoad = false;

    public void Awake()
    {
        if (P_instance != null)
        {
            print("Vous essayer d'instancier une deuxieme instance d'un même type");
            return;
        }

        P_instance = (T)this;

        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }
    }
}
