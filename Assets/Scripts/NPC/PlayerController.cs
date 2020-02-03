/********************
     * LEBLOND Antoine
     * 20/01/2020
     * LEBLOND Antoine
     * Mouvements du joueur en fonction de la caméra, fonction de récupération des ressources, gestion des animations du joueur
     * Vitesse de déplacement, vitesse de minage
     * ******************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _movementSpeed = 3.0f;

    [SerializeField]
    private float _distanceToOre = 2.0f;

    [SerializeField]
    private float _rotationSpeed = 240.0f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _gravity = 20.0f;

    Rigidbody _playerRigidbody;
    CapsuleCollider _playerCollider;
    Animator _playerAnimator;

    public float P_pickupSpeed = 3.0f;

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
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (P_isPicking)
        {
            _moveHorizontal = 0;
            _moveVertical = 0;
            _playerAnimator.SetBool("IsPicking", true);
        }
        else
        {
            _moveHorizontal = Input.GetAxisRaw("Horizontal");
            _moveVertical = Input.GetAxisRaw("Vertical");
            _playerAnimator.SetBool("IsPicking", false);
        }

        if (_moveHorizontal != 0 || _moveVertical != 0)
        {
            _playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            _playerAnimator.SetBool("IsMoving", false);
        }

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = _moveVertical * camForward_Dir + _moveHorizontal * Camera.main.transform.right;

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float _turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, _turnAmount * _rotationSpeed * Time.deltaTime, 0);

        if (_characterController.isGrounded)
        {
            _moveDir = transform.forward * move.magnitude;
            _audioSource.Play();
            _moveDir *= _speed;
        }
        else
        {
            _audioSource.Stop();
        }

        _moveDir.y -= _gravity * Time.deltaTime;

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
                _playerInventory.AddRessourceQuantity(0, Random.Range(1, 5));
                break;
            case "Iron":
                _playerInventory.AddRessourceQuantity(1, Random.Range(1, 5));
                break;
        }
        P_isPicking = false;
        Destroy(ressource.transform.gameObject);
    }

    public void AddRessourceToTarget(RaycastHit target)
    {
        Inventory _targetInventory = target.transform.gameObject.GetComponent<Inventory>();
        for (int i = 0; i < _targetInventory.GetRessourceTabSize(); i++)
        {
            _targetInventory.AddRessourceQuantity(i, _playerInventory.GetRessourceQuantity(i));
            _playerInventory.SetRessourceQuantity(i, 0);
            print(_targetInventory.GetRessourceQuantity(i));
        }
    }
}
