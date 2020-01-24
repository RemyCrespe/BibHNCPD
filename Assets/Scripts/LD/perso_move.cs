using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perso_move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private string HorizontalInputName, VerticalInputName;

    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horMove = Input.GetAxis(HorizontalInputName) * moveSpeed;
        float verMove = Input.GetAxis(VerticalInputName) * moveSpeed;

        Vector3 forwardMovement = transform.forward * verMove;
        Vector3 rightMovement = transform.right * horMove;

        charController.SimpleMove(forwardMovement + rightMovement);
    }
}
