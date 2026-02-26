using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachine
{
    public LevelState currentState;

    public void ChangeState(LevelState newState, LevelManager manager = null)
    {
        currentState?.Exit();
        currentState = newState;

        if (manager != null)
        {
            newState.SetManager(manager); // referansý veriyoruz
        }

        currentState.Enter();
    }
}

