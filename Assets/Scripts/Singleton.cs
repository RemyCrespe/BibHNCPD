using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    public static T P_instance { get; private set; }

    [SerializeField]
    private bool _dontDestroyOnLoad = false;

    protected virtual void Awake()
    { // Start on Awake load
        if (P_instance == null) // Check if Instance is null
        {
            P_instance = (T)this; // put this on the Instance
        }
        else
        {
            Debug.LogError("Instance d'un autre : " + typeof(T));
        }

        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }
    }

    protected virtual void OnDestroy()
    { // For destroy the object
        if (P_instance == this) // Checked if the instance is this
            P_instance = null; // Put null on instance
    }
}
