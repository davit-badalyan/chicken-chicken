using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    private float screenWidth;
    private float screenHeight;
    
    public Vector3 startPosition = new Vector3(0, 0.5f, -3);
    public Vector3 rotationAngle = new Vector3(0, 0, 0);
    
    public GameObject body;
    public GameObject leftLeg;
    public GameObject rightLeg;

    public float forwardMovementSpeed = 20.0f;
    public float sideMovementSpeed = 120.0f;
    public float rotationSpeed = 120.0f;
    public float fallingSpeed = 90.0f;
    public float rotationBound = 45.0f;
    
    void Awake()
    {
        screenWidth = Screen.width / 2.0f;
        screenHeight = Screen.height / 2.0f;
    }
    
    void Start()
    {
        transform.position = startPosition;
        rightLeg.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.touchCount == 3)
        {
            Restart();
        }


        if (!isFallen() && isOnGround())
        {
            FallPlayer();
            MoveForward();
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveSide(Vector3.right);
                RotatePlayer(Vector3.back);
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                MoveSide(Vector3.left);
                RotatePlayer(Vector3.forward);
            }

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    if (touchPosition.x >= screenWidth)
                    {
                        MoveSide(Vector3.right);
                        RotatePlayer(Vector3.back);
                    } 
                    else if (touchPosition.x < screenWidth)
                    {
                        MoveSide(Vector3.left);
                        RotatePlayer(Vector3.forward);                    
                    }
                }
            }
        }
        
    }

    void Restart()
    {
        rotationAngle = Vector3.zero;
            
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * forwardMovementSpeed));
    }
    
    void MoveSide(Vector3 direction)
    {
        transform.Translate(direction * (Time.deltaTime * sideMovementSpeed), Space.World);
    }

    void RotatePlayer(Vector3 direction)
    {
        ResetRotation();
        Rotate(direction, rotationSpeed);

        leftLeg.SetActive(!leftLeg.activeSelf);
        rightLeg.SetActive(!rightLeg.activeSelf);
    }

    void FallPlayer()
    {
        if (rotationAngle.z < 0) {
            Rotate(Vector3.back, fallingSpeed);
        }
        else
        {
            Rotate(Vector3.forward, fallingSpeed);
        }
    }

    void Rotate(Vector3 direction, float speed)
    {
        rotationAngle += direction * (Time.deltaTime * speed);
        RotateToAngle(rotationAngle, rotationSpeed);
    }

    void ResetRotation()
    {
        rotationAngle = Vector3.zero;
        RotateToAngle(rotationAngle, 20.0f);
    }

    void RotateToAngle(Vector3 angle,float speed)
    {
        Quaternion toAngle = Quaternion.Euler(angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toAngle, Time.deltaTime * speed);
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
