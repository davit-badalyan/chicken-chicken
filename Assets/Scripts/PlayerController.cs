using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public Vector3 startPosition = new Vector3(0, 0.5f, -3);
    public GameObject leftLeg;
    public GameObject rightLeg;

    public float forwardMovementSpeed = 20.0f;
    public float sideMovementSpeed = 30.0f;
    public float rotationSpeed = 60.0f;

    void Start()
    {
        rightLeg.SetActive(false);
    }

    void Update()
    {
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
            transform.Translate(Vector3.forward * (Time.deltaTime * forwardMovementSpeed));
        
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * (Time.deltaTime * sideMovementSpeed), Space.World);
            } else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * (Time.deltaTime * sideMovementSpeed), Space.World);
            }
        }
    }

    void RotatePlayer()
    {
        if (!isFallen())
        {
            ResetRotation();
            // Rotate Player left and right depend on user input
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.back * (Time.deltaTime * rotationSpeed));
                
                rightLeg.SetActive(true);
                leftLeg.SetActive(false); 
            } else if (Input.GetKey(KeyCode.A))
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
            if (rightLeg.activeSelf)
            {
                transform.Rotate(Vector3.back * (Time.deltaTime * rotationSpeed));
            }
            else if (leftLeg.activeSelf)
            {
                transform.Rotate(Vector3.forward * (Time.deltaTime * rotationSpeed));
            }
        }
    }

    void ResetRotation()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            float speed = 20.0f;
            Quaternion zeroRotation = Quaternion.identity;

            transform.rotation = Quaternion.Slerp(transform.rotation, zeroRotation, Time.deltaTime * speed);
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
