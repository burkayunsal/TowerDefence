using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Behaviours/Rotation")]

public class Rotation : RotationBehaviourSO
{
    [SerializeField] float rotationSpeed;
    
    private IMovement movementBehaviour;
    private IAttack attackBehaviour;
    private Transform controlledTransform;

    
    private delegate void OnUpdate();
    private OnUpdate _onUpdate;
    
    public override void OnStart(BehaviourController controller)
    {
        if (controller.MovementBehaviour)
        {
            movementBehaviour = controller.MovementBehaviour;
            _onUpdate = TargetRotation;
        }

        if (controller.AttackBehaviour)
        {
            attackBehaviour = controller.AttackBehaviour;
            _onUpdate = TowerRotation;
        }
        
        controlledTransform = controller.transform;
    }

    void TargetRotation()
    {
        Vector3 vectorToTarget = movementBehaviour.TargetPoint - (Vector2) controlledTransform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle , Vector3.forward);
        controlledTransform.rotation = Quaternion.Slerp(controlledTransform.rotation, q, Time.deltaTime * rotationSpeed);
    }
    
    void TowerRotation()
    {
        if (attackBehaviour.lsTargets.Count == 0 || attackBehaviour.lsTargets[0] == null) return;
        
        Vector3 vectorToTarget = attackBehaviour.lsTargets[0].Controller.transform.position - controlledTransform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        controlledTransform.rotation = Quaternion.Slerp(controlledTransform.rotation, q, Time.deltaTime * rotationSpeed);
    }
    
    public override void RotationOnUpdate()
    {
       _onUpdate?.Invoke();
    }
}
