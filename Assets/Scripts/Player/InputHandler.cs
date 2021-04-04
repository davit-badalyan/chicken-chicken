using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;
    
    public Player player;

    private void Awake()
    {
        screenWidth = Screen.width / 2.0f;
        screenHeight = Screen.height / 2.0f;
    }

    void Update()
    {
        if (GameManager.Instance.GameStarted && !GameManager.Instance.PlayerFailed)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                player.ChangeActiveLeg(-1);
                player.movementHandler.MoveSide(1);
                player.rotationHandler.ResetRotation(-1);
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                player.ChangeActiveLeg(1);
                player.movementHandler.MoveSide(-1);
                player.rotationHandler.ResetRotation(1);
            }
        }

        if (GameManager.Instance.GameStarted && !GameManager.Instance.PlayerFailed)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = touch.position;
        
                if (touch.phase == TouchPhase.Began)
                {
                    if (touchPosition.x >= screenWidth)
                    {
                        player.ChangeActiveLeg(-1);
                        player.movementHandler.MoveSide(1);
                        player.rotationHandler.ResetRotation(-1);
                    } 
                    else if (touchPosition.x < screenWidth)
                    {
                        player.ChangeActiveLeg(1);
                        player.movementHandler.MoveSide(-1);
                        player.rotationHandler.ResetRotation(1);                   
                    }
                }
            }
        }
    }
}
