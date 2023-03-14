using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Behaviours/Movement")]
public class Movement : MovementBehaviourSO
{
    [SerializeField] private float speed;

    public UnityEvent<BehaviourController> OnTargetReached { get; } = new UnityEvent<BehaviourController>();

    public BehaviourController MoveTarget { get; set; }

    public override void SetSpeed(float moveSpeed)
    {
        speed = moveSpeed;
    }

    public override void OnStart(BehaviourController controller) { }

    public override Vector2 TargetPoint
    {
        get
        {
            if (MoveTarget == null)
                return Controller.transform.position;
            return MoveTarget.transform.position;
        }
    }

    public override void MovementOnUpdate()
    {
        if (!IsActive || !MoveTarget) return;

        Vector2 movementDirection = (TargetPoint - (Vector2)Controller.transform.position).normalized;
        Controller.transform.position = (Vector2)Controller.transform.position + (movementDirection * speed * Time.deltaTime);
        
        if (Vector2.Distance(Controller.transform.position, MoveTarget.transform.position) < (speed * Time.deltaTime)) 
            TargetReached();
    }

    void TargetReached()
    {
        OnTargetReached.Invoke(MoveTarget);
        OnTargetReached.RemoveAllListeners();
    }
    
   
}
