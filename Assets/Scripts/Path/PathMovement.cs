using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Behaviours/PathMovement")]

public class PathMovement : MovementBehaviourSO
{
    [SerializeField] private float moveSpeed;
    
    public float DistanceTravelled { get; private set; } = 0;
    public Transform ControlledTransform { get; private set; }
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    
    private Node targetNode;
    private Vector2 targetPos;
    
    public Node TargetNode 
    { 
        get => targetNode;

        private set
        {
            targetNode = value;
            if (targetNode != null) 
                targetPos = targetNode.GetRandomPoint();
        } 
    }
    
    public override Vector2 TargetPoint { get => targetPos; }
    
    public override void OnStart(BehaviourController controller)
    {
        ControlledTransform = controller.transform;
    }

    public override void MovementOnUpdate()
    {
        if (!IsActive || TargetNode == null) return;

        Vector2 moveDirection = (TargetPoint - (Vector2) ControlledTransform.position).normalized;
        Vector2 distanceToMove = moveDirection * MoveSpeed * Time.deltaTime;
        ControlledTransform.position = (Vector2) ControlledTransform.position + distanceToMove;
        DistanceTravelled += distanceToMove.magnitude;
        if (Vector2.Distance(ControlledTransform.position, TargetPoint) < MoveSpeed * Time.deltaTime)
            TargetNodeReached();
    }

    public override void SetSpeed(float moveSpeed)
    { 
        MoveSpeed = moveSpeed;
    }
    
    private void TargetNodeReached()
    {
        TargetNode.PathReachedBy(this);
        TargetNode = TargetNode.GetNextNode();
    }
    
    public void SetNode(Node node)
    {
        TargetNode = node;
    }
}
