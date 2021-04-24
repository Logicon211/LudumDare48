using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public const int NUMBER_OF_SCENES = 7;

    private static int currentScene = 0;

    // 0 = No Choice Chosen, 1 = Bad option, 2 = Okay option, 3 = Good option
    private static int[] choices = new int[NUMBER_OF_SCENES];

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

}
