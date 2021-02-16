using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    private Rigidbody _playerRb;
    private float _horizontalInput;
    private float _forwardInput;
    
    public float speed = 10.0f;

    void Start()
    {
        // _playerRb = GetComponent<Rigidbody>();
        // playerRb.centerOfMass = centerOfMass.transform.position;
    }

    void Update()
    {
        // This is where we get player input
        _horizontalInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");

        // Move the Player forward
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));

        // Move the Player left and right depend on user input
        if (_horizontalInput != 0)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * speed * _horizontalInput));
        }
    }

    bool isOnGround()
    {
        return true;
    }

}
