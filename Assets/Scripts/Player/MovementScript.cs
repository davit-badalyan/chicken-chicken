using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private float p;

    public Player player;
    public float xPosition = 0;
    public float targetPositionX = 0;
    public float forwardMovementSpeed = 20.0f;
    public float sideMovementSpeed = 120.0f;
    
    // Update is called once per frame
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
        bool result = Physics.Raycast(player.body.transform.position, player.body.transform.TransformDirection(Vector3.down), out hit,
            Mathf.Infinity, layerMask);

        Debug.DrawRay(player.body.transform.position, player.body.transform.TransformDirection(Vector3.forward) * 1000, result ? Color.green : Color.red);
        return result;
    }
    
    public void MoveSide(int direction)
    {
        targetPositionX += direction * sideMovementSpeed * Time.deltaTime;
        
        xPosition = Mathf.SmoothDamp(xPosition, targetPositionX, ref p, Time.deltaTime);
        player.transform.position = new Vector3(xPosition, player.transform.position.y, player.transform.position.z);
    }
}
