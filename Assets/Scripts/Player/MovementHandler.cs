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
                xPosition = 0;
            } 
        }
    }
    
    private void MoveForward()
    {
        xPosition = Mathf.Lerp(xPosition, targetPositionX, Time.deltaTime * 25);
        player.transform.position = new Vector3(xPosition, player.transform.position.y, player.transform.position.z + Time.deltaTime * forwardMovementSpeed);
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
    
    
    public void MoveSide(int direction)
    {
        targetPositionX += direction * sideMovementDistance;
    }
    
}
