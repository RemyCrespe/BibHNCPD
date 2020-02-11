/**************************************************************************
 ** Nathan Lefebvre
 **
 ** 06 - 02 - 2020
 **
 ** Script qui gère le trigger de l'obj ventilation
 *************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_ventilation : MonoBehaviour
{
    public bool _EnterTrigg { get; set; }

    private void Start()
    {
        _EnterTrigg = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _EnterTrigg = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _EnterTrigg = false;
        }
    }
}
