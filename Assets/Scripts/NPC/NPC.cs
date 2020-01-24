/********************
 * LEBLOND Antoine
 * 22/01/2020
 * LEBLOND Antoine
 * Possibilité d'avoir des PNJ statiques ou qui suivent le joueur, système de dialogue
 * Vitesse de déplacement, si le pnj doit suivre le joueur ou non, le texte du dialogue, la distance au joueur
 * ******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{    
    [SerializeField]
    private string P_pnjDialogueText;

    [SerializeField]
    private Text P_dialogueText;

    [SerializeField]
    private GameObject P_dialogueView;

    public void Dialogue()
    {
        P_dialogueText.text = P_pnjDialogueText;
        if (!P_dialogueView.activeSelf)
        {
            P_dialogueView.SetActive(true);
        }
        else
        {
            P_dialogueView.SetActive(false);
        }
    }
}
