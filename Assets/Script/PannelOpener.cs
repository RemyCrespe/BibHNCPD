
/******************************************************************************************************
 * Redin Laurine
 * 20/01/2020
 * 21/01/2020
 * 
 * Script d'ouverture/fermeture du menu pause
 * ouvre le Canva pause apres appuis sur le bouton, et le referme avec le bouton de reprise du jeux
 *****************************************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelOpener : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Panel.gameObject.SetActive(true);
        }
    }
}
