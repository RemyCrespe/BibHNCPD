/********************
 * LEBLOND Antoine
 * 21/01/2020
 * LEBLOND Antoine
 * Déplacer la caméra avec la souris si on appuie sur clique droit
 * Vitesse de rotation de la camera, position du joueur
 * ******************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float _smoothFactor = 0.5f;

    [SerializeField]
    private float _rotationsSpeed = 5.0f;

    private Vector3 _cameraOffset;

    private bool _lookAtPlayer = false;

    private bool _rotateAroundPlayer = true;

    void Start()
    {
        _cameraOffset = transform.position - _playerTransform.position;
    }

    void LateUpdate()
    {
        if (_rotateAroundPlayer && Input.GetMouseButton(1))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _rotationsSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;
        }

        Vector3 newPos = _playerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, _smoothFactor);

        if (_lookAtPlayer || _rotateAroundPlayer)
        {
            transform.LookAt(_playerTransform);
        }
    }
}
