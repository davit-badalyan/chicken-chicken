using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private float p;
    private Coroutine currentSideMovement = null;

    public Player player;
    public float xPosition = 0;
    public float targetPositionX = 0;
    public float forwardMovementSpeed = 20.0f;
    public float sideMovementDistance = 2.0f;
    public float sideMovementDistanceByJoystick = 10.0f;
    public float sideMovementTime = 2.0f;
    
    private void Update()
    {
        if (GameManager.Instance.GameStarted)
        {
            if (IsOnGround() && !GameManager.Instance.PlayerFailed)
            {
                MoveForward();
            }
            else
            {
                GameManager.Instance.PauseGame();
            } 
        }
    }
    
    private void MoveForward()
    {
        player.transform.Translate(Vector3.forward * (Time.deltaTime * forwardMovementSpeed));
    }

    private bool IsOnGround()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        Transform playerTransform = player.body.transform;
        bool result = Physics.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector3.down),
            out hit,
            Mathf.Infinity, layerMask);

        return result;
    }
    
    private void StartSideMovement(int direction)
    {
        currentSideMovement = StartCoroutine (SmoothLerp (sideMovementTime, direction));
    }

    public void StopSideMovement()
    {
        if (currentSideMovement != null)
        {
            StopCoroutine (currentSideMovement);
        }
    }
    
    private IEnumerator SmoothLerp(float time, int direction)
    {
        xPosition += direction * sideMovementDistance;
        float elapsedTime = 0;
        
        while (elapsedTime < time)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 targetPos = new Vector3(xPosition, playerPos.y, playerPos.z);
            
            playerPos = Vector3.Lerp(playerPos, targetPos, elapsedTime / time);
            player.transform.position = playerPos;
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
    }
    
    public void MoveSide(int direction)
    {
        StopSideMovement();
        StartSideMovement(direction);
    }

    public void MoveSideByJoystick(int direction)
    {
        targetPositionX += direction * sideMovementDistanceByJoystick * Time.deltaTime;
        
        xPosition = Mathf.SmoothDamp(xPosition, targetPositionX, ref p, Time.deltaTime);
        player.transform.position = new Vector3(xPosition, player.transform.position.y, player.transform.position.z);
    }
}
