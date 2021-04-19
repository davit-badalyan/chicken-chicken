using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;
    
    public Player player;
    public Joystick joystick;
    public float joystickDirection;

    private void Awake()
    {
        screenWidth = Screen.width / 2.0f;
        screenHeight = Screen.height / 2.0f;
    }

    void Update()
    {
        if (GameManager.Instance.GameStarted && !GameManager.Instance.PlayerFailed)
        {
            CheckForKeyboardInput();
            CheckForTouchInput();
            CheckForJoystickInput();
        }
        else
        {
            HideJoystick();
        }
    }

    private void CheckForKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
    }
    
    private void CheckForTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
        
            if (touch.phase == TouchPhase.Began)
            {
                if (touchPosition.x >= screenWidth)
                {
                    MoveRight();
                } 
                else if (touchPosition.x < screenWidth)
                {
                    MoveLeft();          
                }
            }
        }
    }

    private void CheckForJoystickInput()
    {
        joystickDirection = joystick.Horizontal;
            
        if (joystickDirection > 0)
        {
            MoveRight();
        }
        else if (joystickDirection < 0)
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        ResetRotation(-1);
        ChangeActiveLeg(-1);
        player.movementHandler.MoveSide(1);
    }

    private void MoveLeft()
    {
        ResetRotation(1);
        ChangeActiveLeg(1);
        player.movementHandler.MoveSide(-1);
    }
    
    private void ChangeActiveLeg(int direction)
    {
        player.ChangeActiveLeg(direction);
    }

    private void ResetRotation(int direction)
    {
        player.rotationHandler.ResetRotation(direction);
    }

    private void HideJoystick()
    {
        joystick.HideJoystick();
    }
}
