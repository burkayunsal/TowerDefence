using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Node : MonoBehaviour
{
    public bool isLastNode;

    [SerializeField] private Node nextNode;
    [SerializeField] private float nodeRadius = 30;
    
    public UnityEvent<MovementBehaviourSO> OnPathReached { get; set; } = new UnityEvent<MovementBehaviourSO>();

   public Node GetNextNode()
   {
       if (nextNode == null) return null;
       return nextNode;
   }

   public Vector2 GetRandomPoint()
   {
       return (Vector2) transform.position + Random.insideUnitCircle * Random.Range(0, nodeRadius);
   }

   public void PathReachedBy(MovementBehaviourSO movementBehaviour)
   {
       OnPathReached.Invoke(movementBehaviour);
       if (isLastNode && GameManager.isRunning)
       {
           GameManager.OnLevelFailed();
       }
   }
   
}
