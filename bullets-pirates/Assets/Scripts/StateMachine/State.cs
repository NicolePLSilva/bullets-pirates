using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{   
    public abstract void Enter();

    public abstract void InProgress(float deltaTime);

    public abstract void Exit();

}
