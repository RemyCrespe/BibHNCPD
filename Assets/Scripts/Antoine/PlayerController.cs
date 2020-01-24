/********************
 * LEBLOND Antoine
 * 20/01/2020
 * LEBLOND Antoine
 * Mouvements du joueur en fonction de la caméra, fonction de récupération des ressources
 * Vitesse de déplacement, vitesse de minage
 * ******************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    public float P_movementSpeed;
    Rigidbody _playerRigidbody;
    CapsuleCollider _playerCollider;

    public float P_distanceToOre;
    public float P_pickupSpeed;

    float _moveHorizontal;
    float _moveVertical;

    public float P_rotationSpeed;
    public float P_speed;

    public float P_gravity;
    private Vector3 _moveDir = Vector3.zero;

    public bool P_isPicking = false;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (P_isPicking) //If the player is mining, freeze is movement
        {
            _moveHorizontal = 0;
            _moveVertical = 0;
        }
        else
        {
            _moveHorizontal = Input.GetAxisRaw("Horizontal");
            _moveVertical = Input.GetAxisRaw("Vertical");
        }

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = _moveVertical * camForward_Dir + _moveHorizontal * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float _turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, _turnAmount * P_rotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            _moveDir = transform.forward * move.magnitude;

            _moveDir *= P_speed;
        }

        _moveDir.y -= P_gravity * Time.deltaTime;

        _characterController.Move(_moveDir * Time.deltaTime);
    }

    public bool IsCloseToEntity(RaycastHit target) //This function check if the Player is near the ressource when he passes the mouse over it
    {
        Vector3 _closestPoint1 = target.collider.ClosestPointOnBounds(transform.position);
        Vector3 _closestPoint2 = _playerCollider.ClosestPointOnBounds(target.transform.position);
        
        float _targetDistance = Vector3.Distance(_closestPoint1, _closestPoint2);

        if (_targetDistance <= P_distanceToOre)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PickupRessource(RaycastHit ressource)
    {
        StartCoroutine(PickupCooldown(ressource));
        P_isPicking = true;
    }

    public IEnumerator PickupCooldown(RaycastHit ressource) //This fonction after an amount of time add a random number of a ressource type in inventory, then destroy the ressource
    {
        yield return new WaitForSeconds(P_pickupSpeed);
        switch (ressource.collider.name)
        {
            case "PinkIron":
                GetComponent<Inventory>().ModifyRessourceAmount(1);
                break;
            case "Iron":
                GetComponent<Inventory>().ModifyRessourceAmount(2);
                break;
        }
        P_isPicking = false;
        Destroy(ressource.transform.gameObject);
    }

    public void DropRessourceToTarget(RaycastHit target) //Put all the ressource of the player in the inventory of the target
    {
        Inventory inventory = target.transform.gameObject.GetComponent<Inventory>();
        inventory._nbPinkOre += GetComponent<Inventory>()._nbPinkOre;
        inventory._nbIron += GetComponent<Inventory>()._nbIron;
        GetComponent<Inventory>()._nbPinkOre = 0;
        GetComponent<Inventory>()._nbIron = 0;
        print(inventory._nbPinkOre);
        print(inventory._nbIron);
    }
}
