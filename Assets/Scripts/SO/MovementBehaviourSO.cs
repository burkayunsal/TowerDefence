using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBehaviourSO : BehaviourSO, IMovement
{
    public abstract Vector2 TargetPoint { get; }

    public abstract void MovementOnUpdate();

    public abstract void SetSpeed(float moveSpeed);
}
