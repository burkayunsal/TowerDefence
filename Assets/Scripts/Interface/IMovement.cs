using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    public Vector2 TargetPoint { get; }
    public void MovementOnUpdate();
    public void SetSpeed(float moveSpeed);
}
