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
    [TextArea(7, 3)]
    [SerializeField]
    private string P_pnjDialogueText;
    
    [SerializeField]
    private Text P_dialogueText;

    [SerializeField]
    private GameObject P_dialogueView;
    
    [SerializeField]
    private AudioSource _audioSource;

    public void Dialogue()
    {
        P_dialogueText.text = P_pnjDialogueText;
        if (!P_dialogueView.activeSelf)
        {
            P_dialogueView.SetActive(true);
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
            P_dialogueView.SetActive(false);
        }
    }
}
