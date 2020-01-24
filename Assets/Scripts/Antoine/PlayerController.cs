using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /********************
     * LEBLOND Antoine
     * 20/01/2020
     * LEBLOND Antoine
     * Mouvements du joueur en fonction de la caméra, fonction de récupération des ressources
     * Vitesse de déplacement, vitesse de minage
     * ******************/

    private CharacterController _characterController;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _distanceToOre;

    [SerializeField]
    private float P_rotationSpeed;

    [SerializeField]
    private float P_speed;

    [SerializeField]
    private float P_gravity;

    Rigidbody _playerRigidbody;
    CapsuleCollider _playerCollider;

    public float P_pickupSpeed;

    private float _moveHorizontal;
    private float _moveVertical;

    private Vector3 _moveDir = Vector3.zero;

    public bool P_isPicking = false;

    Inventory _playerInventory;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _playerInventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (P_isPicking)
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

    public bool IsCloseToEntity(RaycastHit target)
    {
        Vector3 _closestPoint1 = target.collider.ClosestPointOnBounds(transform.position);
        Vector3 _closestPoint2 = _characterController.ClosestPointOnBounds(target.transform.position);
        
        float _targetDistance = Vector3.Distance(_closestPoint1, _closestPoint2);

        if (_targetDistance <= _distanceToOre)
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
        if (!P_isPicking)
        {
            StartCoroutine(PickupCooldown(ressource));
            P_isPicking = true;
        }
    }

    private IEnumerator PickupCooldown(RaycastHit ressource)
    {
        yield return new WaitForSeconds(P_pickupSpeed);
        switch (ressource.collider.tag)
        {
            case "Pink Quartz":
                _playerInventory.PickupRessource(1);
                break;
            case "Iron":
                _playerInventory.PickupRessource(2);
                break;
        }
        P_isPicking = false;
        Destroy(ressource.transform.gameObject);
    }

    public void AddRessourceToTarget(RaycastHit target)
    {
        Inventory _targetInventory = target.transform.gameObject.GetComponent<Inventory>();
        _targetInventory.AddRessource(1, _playerInventory.GetNbRessource(1));
        _targetInventory.AddRessource(2, _playerInventory.GetNbRessource(2));
        _playerInventory.SetRessource(1,0);
        _playerInventory.SetRessource(2,0);
        print(_targetInventory.GetNbRessource(1));
        print(_targetInventory.GetNbRessource(1));
    }
}
