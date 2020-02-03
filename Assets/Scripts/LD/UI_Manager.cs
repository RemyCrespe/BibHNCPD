using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _txt;
    [SerializeField] private string _nameSceneDJ;

    private void Start()
    {
        _txt.enabled = false;
    }

    private void Update()
    { 
        if (GameManager.P_instance._enterTrigger && !GameManager.P_instance.IsbtUse_down())
        {
            _txt.enabled = true;
        }
        else
        {
            _txt.enabled = false;
        }
    }

    // Affiche le message si est à coté de la porte du DJ
    private void OnGUI()
    {

        //affiche les boutons de sélection des rooms
        if (GameManager.P_instance._enterTrigger && GameManager.P_instance.IsbtUse_down())
        {
            if (GUI.Button(new Rect(200, 200, 60, 50), "Room 1"))
            {
                GameManager.P_instance._GMnbRoom = 1;
                GameManager.P_instance.LoadScene(_nameSceneDJ);
            }
            if (GUI.Button(new Rect(270, 200, 60, 50), "Room 2"))
            {
                GameManager.P_instance._GMnbRoom = 2;
                GameManager.P_instance.LoadScene(_nameSceneDJ);
            }
            if (GUI.Button(new Rect(340, 200, 60, 50), "Room 3"))
            {
                GameManager.P_instance._GMnbRoom = 3;
                GameManager.P_instance.LoadScene(_nameSceneDJ);
            }
        }
    }
}
