using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelState
{
    protected LevelManager levelManager;

    public virtual void SetManager(LevelManager manager)
    {
        this.levelManager = manager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

