
/******************************************************************************************************
 * Redin Laurine
 * 21/01/2020
 * 
 * Script d'ouverture/fermeture de l'inventaire
 * Fais passer l'inventaire de sa forme réduite à sa forme ouverte
 *****************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Panel;
   
    public void OpenPanel()
    {
        bool isActive = Panel.activeSelf;
    }

    //Tentative de cacher le bouton d'inventaire fermé

    //public void CloseButton()
    //{
    //    EventSystem.current.currentSelectedGameObject.SetActive(false);
    //}
}
