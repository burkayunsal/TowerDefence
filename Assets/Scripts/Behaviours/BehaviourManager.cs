using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : Singleton<BehaviourManager>
{
    private List<BehaviourController> lsBehaviourControllers { get; set; } = new List<BehaviourController>();
    private List<HealthBehaviourSO> lsHealthBehaviours { get; set; } = new List<HealthBehaviourSO>();
    private List<AttackBehaviourSO> lsAttackBehaviours { get; set; } = new List<AttackBehaviourSO>();
    private List<MovementBehaviourSO> lsMovementBehaviours { get; set; } = new List<MovementBehaviourSO>();

    private List<RotationBehaviourSO> lsRotationBehaviours { get; set; } = new List<RotationBehaviourSO>();
    
    public void AddBehaviourController(BehaviourController controller)
    {
        lsBehaviourControllers.Add(controller);
        if (controller.HealthBehaviour != null) lsHealthBehaviours.Add(controller.HealthBehaviour);
        if (controller.AttackBehaviour != null) lsAttackBehaviours.Add(controller.AttackBehaviour);
        if (controller.MovementBehaviour != null) lsMovementBehaviours.Add(controller.MovementBehaviour);
        if (controller.RotationBehaviour != null) lsRotationBehaviours.Add(controller.RotationBehaviour);
    }
    
    public void RemoveBehaviourController(BehaviourController controller)
    {
        lsBehaviourControllers.Remove(controller);
        if (controller.HealthBehaviour != null) lsHealthBehaviours.Remove(controller.HealthBehaviour);
        if (controller.AttackBehaviour != null) lsAttackBehaviours.Remove(controller.AttackBehaviour);
        if (controller.MovementBehaviour != null) lsMovementBehaviours.Remove(controller.MovementBehaviour);
        if (controller.RotationBehaviour != null) lsRotationBehaviours.Remove(controller.RotationBehaviour);
    }
    
    
    public HealthBehaviourSO GetNearestHealthBehaviour(Vector2 requestedPosition, float radius)
    {
        HealthBehaviourSO result = null;
        float closestDistance = Mathf.Infinity;
        foreach (HealthBehaviourSO hb in lsHealthBehaviours)
        {
            if (hb.IsDead) continue;
            if (!hb.IsActive) continue;
   
            float distance = Vector2.Distance(hb.Controller.transform.localPosition, requestedPosition);

            if (distance > closestDistance) continue;
            if (distance > radius ) continue;
            closestDistance = distance;
            result = hb;
        }
        return result;
    }
}
