/**************************************************************************
 ** Nathan Lefebvre
 **
 ** 01 - 02 - 2020
 **
 ** Scrip qui renvoie si oui ou non le joueur entre en contacte avec un objet
 ** d'interaction
 *************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perso_Interact : MonoBehaviour
{
    public bool _EnterTrigg { get; set; }

    private void Start()
    {
        _EnterTrigg = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "obj_interact")
        {
            _EnterTrigg = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "obj_interact")
        {
            _EnterTrigg = false;
        }
    }
}
