using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Player player;
    public Joystick joystick;
    public int joystickDirection;
    public int currentDirection;

    void Update()
    {
        if (GameManager.Instance.GameStarted && !GameManager.Instance.PlayerFailed)
        {
            CheckForKeyboardInput();
            CheckForTouchInput();
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
        currentDirection = joystickDirection;
        joystickDirection = joystick.Horizontal;
            
        if (currentDirection != joystickDirection && joystickDirection > 0)
        {
            MoveRight();
        }
        else if (currentDirection != joystickDirection && joystickDirection < 0)
        {
            MoveLeft();
        }
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
    
    public void MoveRight()
    {
        ResetRotation(-1);
        ChangeActiveLeg(-1);
        player.movementHandler.MoveSide(1);
    }
     
    public void MoveLeft()
    {
        ResetRotation(1);
        ChangeActiveLeg(1);
        player.movementHandler.MoveSide(-1);
    }

    public void Reset()
    {
        joystickDirection = 0;
        joystick.HideJoystick();
    }
}
