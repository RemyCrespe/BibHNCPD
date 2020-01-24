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
    public GameObject P_player;
    
    public float P_allowedDistance;
    public float P_followSpeed;

    public float P_targetDistance;

    public string P_pnjDialogueText;
    public Text P_dialogueText;
    public GameObject P_dialogueView;

    public bool P_canFollowPlayer = false;

    void Update()
    {
        if (P_player)
        {
            if (P_canFollowPlayer) //If the NPC is set to follow the player
            {
                P_targetDistance = Vector3.Distance(P_player.transform.position, transform.position); //Calculate the distance between the NPC and the player
                Vector3 _lookPosition = new Vector3(P_player.transform.position.x, transform.position.y, P_player.transform.position.z); //The NPC look at x & z position of the player
                transform.LookAt(_lookPosition);

                if (P_targetDistance > P_allowedDistance) //If the NPC is to far, the follows the player
                {
                    transform.position = Vector3.MoveTowards(transform.position, P_player.transform.position, Time.deltaTime * P_followSpeed);
                }
            }
        }
    }

    public void Dialogue()
    {
        P_dialogueText.text = P_pnjDialogueText; //The text is equals to the NPC text
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
