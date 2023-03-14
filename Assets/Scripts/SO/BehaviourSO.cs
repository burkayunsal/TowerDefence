using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourSO : ScriptableObject , IBehaviour
{
    public virtual BehaviourController Controller { get; protected set; }

    public virtual bool IsActive { get; protected set; } = true;
    
    public virtual void SetController(BehaviourController controller)
    {
        Controller = controller;
        OnStart(controller);
    }
    
    public abstract void OnStart(BehaviourController controller);
}
