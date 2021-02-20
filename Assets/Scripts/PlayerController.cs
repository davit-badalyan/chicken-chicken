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
    public Vector3 startPosition = new Vector3(0, 0.5f, -3);
    
    public float movementSpeed = 20.0f;
    public float rotationSpeed = 60.0f;

    void Start()
    {
        // _playerRb = GetComponent<Rigidbody>();
        // playerRb.centerOfMass = centerOfMass.transform.position;
        rightLeg.SetActive(false);
    }

    void Update()
    {
        // This is where we get player input
        _horizontalInput = Input.GetAxis("Horizontal");
        // _forwardInput = Input.GetAxis("Vertical");
        
        CheckForRestart();

        MovePlayer();
        RotatePlayer();
        FallPlayer();

    }

    void CheckForRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            transform.rotation = Quaternion.identity;
        }
    }

    void MovePlayer()
    {
        if (!isFallen())
        {
            // Move Player forward
            transform.Translate(Vector3.forward * (Time.deltaTime * movementSpeed));
        
            // Move Player left and right depend on user input
            transform.Translate(Vector3.right * (Time.deltaTime * movementSpeed * 5 * _horizontalInput), Space.World);
        }
    }

    void RotatePlayer()
    {
        if (!isFallen())
        {
            // Rotate Player left and right depend on user input
            if (_horizontalInput > 0)
            {
                transform.Rotate(Vector3.back * (Time.deltaTime * rotationSpeed));
                
                rightLeg.SetActive(true);
                leftLeg.SetActive(false); 
            } else if (_horizontalInput < 0)
            {
                transform.Rotate(Vector3.forward * (Time.deltaTime * rotationSpeed));

                leftLeg.SetActive(true);
                rightLeg.SetActive(false);
            }
        }
    }

    void FallPlayer()
    {
        if (!isFallen())
        {
            // Rotate Player based on which leg is active 
            if (rightLeg.activeSelf && _horizontalInput == 0)
            {
                transform.Rotate(Vector3.back * (Time.deltaTime * rotationSpeed));
            }
            else if (leftLeg.activeSelf && _horizontalInput == 0)
            {
                transform.Rotate(Vector3.forward * (Time.deltaTime * rotationSpeed));
            }
        }
    }

    bool isOnGround()
    {
        return true;
    }
    
    bool isFallen()
    {
        if (transform.rotation.z * 100 >= 45.00f || transform.rotation.z * 100 <= -45.00f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
