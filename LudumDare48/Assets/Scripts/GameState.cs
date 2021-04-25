using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public const int NUMBER_OF_SCENES = 7;

    private static int currentScene = 0;

    // 0 = Bad option, 1 = Okay option, 2 = Good option, 3 = No Option Chosen
    private static int[] choices = new int[] {3, 3, 3, 3, 3, 3, 3};

    public static int CurrentScene
    {
        get
        {
            return currentScene;
        }
        set
        {
            currentScene = value;
        }
    }

    public static int[] Choices
    {
        get
        {
            return choices;
        }
        set
        {
            choices = value;
        }
    }

    public static int GetChoice(int sceneNumber)
    {
        return choices[sceneNumber];
    }

    public static void UpdateChoice(int sceneNumber, int choice)
    {
        choices[sceneNumber] = choice;
    }

    public static bool CanOpenDoor()
    {
        return choices[currentScene] != 3;
    }

}
