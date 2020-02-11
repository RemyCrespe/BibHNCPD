/**************************************************************************
 ** Nathan Lefebvre
 **
 ** 06 - 02 - 2020
 **
 ** Script qui gère la vanne pour stop la ventilation
 *************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actioner : MonoBehaviour
{
    [SerializeField] private GameObject _obj_vent;
    private bool _EnterTrigg; 

    private void Update()
    {
        if(_EnterTrigg && GameManager.Instance.IsbtUse_down())
        {
            _obj_vent.GetComponent<obj_interaction>().enabled = false;
            Debug.Log("vent disable");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            _EnterTrigg = true;
        }
    }

}
