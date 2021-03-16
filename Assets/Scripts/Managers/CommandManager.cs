using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private List<ICommand> commandBuffer = new List<ICommand>();
    
    private static CommandManager instance;
    public static CommandManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("CommandManager is NULL");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
