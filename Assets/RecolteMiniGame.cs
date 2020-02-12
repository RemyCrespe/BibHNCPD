using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolteMiniGame : MonoBehaviour
{
    [SerializeField] private int _idMiniGame;

    public int GetId() => _idMiniGame;

    private void OnTriggerEnter(Collider other)
    {
        print("enter trigger");

        if (other.CompareTag("Player"))
        {
            var script = other.GetComponent<PlayerController>();
            
            print("it's a player + " + script);

            script.AddRecolt(this);

            gameObject.SetActive(false);
        }
    }
}
