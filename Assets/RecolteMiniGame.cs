using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolteMiniGame : MonoBehaviour
{
    [SerializeField] private int _idMiniGame;

    public int GetId() => _idMiniGame;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            var script = GetComponent<PlayerController>();

            script.AddRecolt(this);

            gameObject.SetActive(false);
        }
    }
}
