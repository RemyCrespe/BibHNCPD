/**************************************************************************
 ** Nathan Lefebvre
 **
 ** 04 - 02 - 2020
 **
 ** Script qui gère les actions des interractions sur l'objet (le déplace, le tourne)
 *************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_interaction : MonoBehaviour
{
    [SerializeField] private Perso_Interact _Player;
    [SerializeField] private obj_ventilation _Vent;
    [SerializeField] private Transform _TPlayer;
    [SerializeField] private CharacterController charController;
    [SerializeField] private float force;
    private Transform _Tobj;
    private Rigidbody _rb_obj;
    private float Abs_diffx, Abs_diffz, diffx, diffz;

    private void Start()
    {
        _rb_obj = GetComponent<Rigidbody>();
        _Tobj = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_Vent._EnterTrigg)
        {
            Interaction_vent();
        }
        else
        {
            Interaction_translate();
        }
    }

    void Interaction_translate()
    {
        Abs_diffx = Mathf.Abs(_Tobj.position.x - _TPlayer.position.x); // valeur absolue de diffx
        Abs_diffz = Mathf.Abs(_Tobj.position.z - _TPlayer.position.z); // valeur absolue de diffz
        diffx = _Tobj.position.x - _TPlayer.position.x; //valeur réel de diffx
        diffz = _Tobj.position.z - _TPlayer.position.z; //valeur réel de diffz

        if (_Player._EnterTrigg && GameManager.Instance.IsbtUse_down())
        {
            if (Abs_diffx > Abs_diffz)
            {
                if (diffx < 0)
                {
                    _rb_obj.AddForce(-force, 0, 0, ForceMode.Impulse);
                }
                else
                {
                    _rb_obj.AddForce(force, 0, 0, ForceMode.Impulse);
                }
            }
            else
            {
                if (diffz < 0)
                {
                    _rb_obj.AddForce(0, 0, -force, ForceMode.Impulse);
                }
                else
                {
                    _rb_obj.AddForce(0, 0, force, ForceMode.Impulse);
                }
            }
   
        }
    }

    void Interaction_vent()
    {
        Vector3 vent = _TPlayer.transform.forward * -force;
        charController.SimpleMove(vent);
    }
}
