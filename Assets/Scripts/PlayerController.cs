using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    private float _horizontalInput;
    private float _forwardInput;
    
    public GameObject leftLeg;
    public GameObject rightLeg;
    public float speed = 10.0f;

    void Start()
    {
        // _playerRb = GetComponent<Rigidbody>();
        // playerRb.centerOfMass = centerOfMass.transform.position;
        leftLeg.SetActive(false);
    }

    void Update()
    {
        // This is where we get player input
        // _horizontalInput = Input.GetAxis("Horizontal");
        // _forwardInput = Input.GetAxis("Vertical");

        // Move the Player forward
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (leftLeg.activeSelf)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                // transform.Rotate(Vector3.forward * (Time.deltaTime * 30));

                leftLeg.SetActive(false);
                rightLeg.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (rightLeg.activeSelf)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                // transform.Rotate(Vector3.back * (Time.deltaTime * 30));
                
                rightLeg.SetActive(false);
                leftLeg.SetActive(true); 
            }
        }



        // Move the Player left and right depend on user input
        // if (_horizontalInput != 0)
        // {
        //     transform.Translate(Vector3.right * (Time.deltaTime * speed * _horizontalInput));
        // }
    }

    bool isOnGround()
    {
        return true;
    }

}
