using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public Player player;
    public int fallDirection = 0;
    public float zRotation = 0;
    public float targetAngle = 0;
    public float fallingSpeed = 90.0f;
    public float rotationBound = 45.0f;

    void Update()
    {
        if (GameManager.Instance.GameStarted)
        {
            if (!IsFallen() && !GameManager.Instance.PlayerFailed)
            {
                Fall();
            }
            else
            {
                GameManager.Instance.PauseGame();
            }
        }

    }
    
    private void Fall()
    {
        targetAngle += fallDirection * fallingSpeed * Time.deltaTime;
                
        zRotation = Mathf.LerpAngle(zRotation, targetAngle, Time.deltaTime * 50);
        transform.eulerAngles = new Vector3(0, 0, zRotation);
    }

    private bool IsFallen()
    {
        return zRotation >= rotationBound || zRotation <= -rotationBound;
    }

    public void ResetRotation(int direction)
    {
        targetAngle = 0;
        zRotation = 0;
        fallDirection = direction;
    }

    public void Reset()
    {
        zRotation = 0;
        targetAngle = 0;
        fallDirection = -1;
    }
}
