using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Player is NULL");
            }

            return instance;
        }
    }

    // Variables
    private float screenWidth;
    private float screenHeight;
    
    public Vector3 startPosition = new Vector3(0, 0.5f, -3);
    
    public GameObject body;
    public GameObject leftLeg;
    public GameObject rightLeg;

    public float forwardMovementSpeed = 20.0f;
    public float sideMovementSpeed = 120.0f;
    public float rotationSpeed = 120.0f;
    public float fallingSpeed = 90.0f;
    public float rotationBound = 45.0f;
    public float zRotation = 0;
    public float targetAngle = 0;
    public int fallDirection = 0;
    private float r;

    private void Awake()
    {
        instance = this;
        screenWidth = Screen.width / 2.0f;
        screenHeight = Screen.height / 2.0f;
    }
    
    void Start()
    {
        Resume();
    }

    void Update()
    {
        if (GameManager.Instance.GameStarted)
        {
            if (!IsFallen() && IsOnGround())
            {
                MoveForward();
                
                if (Input.GetKeyDown(KeyCode.D))
                {
                    MoveSide(Vector3.right);
                    ResetRotation(-1);
                }
                else if(Input.GetKeyDown(KeyCode.A))
                {
                    MoveSide(Vector3.left);
                    ResetRotation(1);
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
                            ResetRotation(-1);
                        } 
                        else if (touchPosition.x < screenWidth)
                        {
                            MoveSide(Vector3.left);
                            ResetRotation(1);                    
                        }
                    }
                }

                targetAngle += fallDirection * fallingSpeed * Time.deltaTime;
                
                zRotation = Mathf.SmoothDamp(zRotation, targetAngle, ref r, Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, zRotation);

            }
            else
            {
                GameManager.Instance.PauseGame();
            }
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * forwardMovementSpeed));
    }
    
    private void MoveSide(Vector3 direction)
    {
        transform.Translate(direction * (Time.deltaTime * sideMovementSpeed), Space.World);
    }

    private void ResetRotation(int direction)
    {
        targetAngle = 0;
        fallDirection = direction;

        leftLeg.SetActive(direction > 0);
        rightLeg.SetActive(direction < 0);
    }

    private bool IsFallen()
    {
        return zRotation >= rotationBound || zRotation <= -rotationBound;
    }
    
    private bool IsOnGround()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        bool result = Physics.Raycast(body.transform.position, body.transform.TransformDirection(Vector3.down), out hit,
            Mathf.Infinity, layerMask);

        Debug.DrawRay(body.transform.position, body.transform.TransformDirection(Vector3.forward) * 1000, result ? Color.green : Color.red);
        return result;
    }

    public void Resume()
    {
        zRotation = 0;
        targetAngle = 0;
        fallDirection = -1;
        
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        
        rightLeg.SetActive(true);
        leftLeg.SetActive(false);
    }
}
