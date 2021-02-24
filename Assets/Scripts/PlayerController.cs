using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public Vector3 startPosition = new Vector3(0, 0.5f, -3);
    public Vector3 rotationAngle = new Vector3(0, 0, 0);
    
    public GameObject body;
    public GameObject leftLeg;
    public GameObject rightLeg;

    public float forwardMovementSpeed = 10.0f;
    public float sideMovementSpeed = 20.0f;
    public float rotationSpeed = 90.0f;
    public float rotationBound = 45.0f;


    void Start()
    {
        transform.position = startPosition;
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
            rotationAngle = Vector3.zero;
            
            transform.position = startPosition;
            transform.rotation = Quaternion.identity;
        }
    }

    void MovePlayer()
    {
        if (!isFallen() && isOnGround())
        {
            // Move Player forward
            transform.Translate(Vector3.forward * (Time.deltaTime * forwardMovementSpeed));
        
            // Move Player right and left based on input
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * (Time.deltaTime * sideMovementSpeed), Space.World);
            } 
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * (Time.deltaTime * sideMovementSpeed), Space.World);
            }
        }
    }

    void RotatePlayer()
    {
        if (!isFallen() && isOnGround())
        {
            ResetRotation();
            
            // Rotate Player left and right based on input
            if (Input.GetKey(KeyCode.D))
            {
                rotationAngle += Vector3.back * (Time.deltaTime * rotationSpeed);
                transform.eulerAngles = rotationAngle;
                
                rightLeg.SetActive(true);
                leftLeg.SetActive(false); 
            } 
            else if (Input.GetKey(KeyCode.A))
            {
                rotationAngle += Vector3.forward * (Time.deltaTime * rotationSpeed);
                transform.eulerAngles = rotationAngle;

                leftLeg.SetActive(true);
                rightLeg.SetActive(false);
            }
        }
    }

    void FallPlayer()
    {
        if (!isFallen() && isOnGround())
        {
            // Rotate Player based on which leg is active 
            if (rightLeg.activeSelf)
            {
                rotationAngle += Vector3.back * (Time.deltaTime * rotationSpeed);
                transform.eulerAngles = rotationAngle;
            }
            else if (leftLeg.activeSelf)
            {
                rotationAngle += Vector3.forward * (Time.deltaTime * rotationSpeed);
                transform.eulerAngles = rotationAngle;
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
    
    bool isFallen()
    {
        if (rotationAngle.z >= rotationBound || rotationAngle.z <= -rotationBound)    
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    bool isOnGround()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;

        if (Physics.Raycast(body.transform.position, body.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(body.transform.position, body.transform.TransformDirection(Vector3.forward) * 1000, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(body.transform.position, body.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            return false;
        }
    }
}
