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

    public GameObject body;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public InputHandler inputHandler;
    public MovementHandler movementHandler;
    public RotationHandler rotationHandler;
    public Vector3 startPosition = new Vector3(0, 0.5f, -3);

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        Resume();
    }
    
    public void Resume()
    {
        rotationHandler.zRotation = 0;
        rotationHandler.targetAngle = 0;
        rotationHandler.fallDirection = -1;
        movementHandler.xPosition = 0;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        
        rightLeg.SetActive(true);
        leftLeg.SetActive(false);
    }
    
    public void ChangeActiveLeg(int direction)
    {
        leftLeg.SetActive(direction > 0);
        rightLeg.SetActive(direction < 0);
    }
}
