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
    public InputScript inputScript;
    public MovementScript movementScript;
    public RotationScript rotationScript;
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
        rotationScript.zRotation = 0;
        rotationScript.targetAngle = 0;
        rotationScript.fallDirection = -1;
        movementScript.xPosition = 0;
        movementScript.targetPositionX = 0;
        
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        
        rightLeg.SetActive(true);
        leftLeg.SetActive(false);
    }
}
