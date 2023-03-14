using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour
{
    public BehaviourController Controller { get; }
    
    public bool IsActive { get; }
    
    public void SetController(BehaviourController controller);
}
