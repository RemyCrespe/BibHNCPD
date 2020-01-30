/********************
     * LEBLOND Antoine
     * 24/01/2020
     * LEBLOND Antoine
     * PNJ avec système de dialogue
     * Le texte du dialogue
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
