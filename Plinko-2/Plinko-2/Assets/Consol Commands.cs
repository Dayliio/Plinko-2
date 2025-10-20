using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using TMPro;



public class ConsolCommands : MonoBehaviour
{
    public Ball_Count balls_Count;
    static double Ball = Ball_Count.balls;

    // This method will be called by the console command.  It MUST be static.
    public static void SetBallsTo999()
    {
        
        Ball_Count instance = FindFirstObjectByType<Ball_Count>();

        if (instance != null)
        {
            Ball = 999; // Set the Balls variable on the instance.
            Debug.Log("Balls set to 999 via console command.");
        }
        else
        {
            Debug.LogError("BallManager instance not found in the scene.");
        }
    }

    void Start()
    {
        // Register the console command.
        AddConsoleCommand("set_balls_999", "Sets the number of balls to 999", SetBallsTo999);
    }

    // Method to add a console command.  This is a general utility.
    private void AddConsoleCommand(string commandName, string commandDescription, Action commandAction)
    {
        // Use reflection to get the necessary methods from the Debug class.
        var debugClass = typeof(Debug);
        var developerConsoleField = debugClass.GetField("developerConsole", BindingFlags.Static | BindingFlags.NonPublic);
        var consoleType = developerConsoleField.FieldType;
        var getInstance = consoleType.GetMethod("get_instance", BindingFlags.Static | BindingFlags.Public);
        var registerCommand = consoleType.GetMethod("RegisterCommand", BindingFlags.Instance | BindingFlags.Public);

        if (developerConsoleField == null || consoleType == null || getInstance == null || registerCommand == null)
        {
            Debug.LogError("Failed to register console command.  Debug class structure may have changed.");
            return; // Important: Exit if we can't get the needed methods.
        }

        // Get the console instance.
        object consoleInstance = getInstance.Invoke(null, null);

        // Create a delegate for the command action.
        var commandDelegate = commandAction;

        // Register the command.
        registerCommand.Invoke(consoleInstance, new object[] { commandName, commandDescription, commandDelegate });
        Debug.Log($"Console command '{commandName}' registered.");
    }
    
}

