using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : PoolObject
{
    #region Parameters & Serializables
    
    [SerializeField] private MovementBehaviourSO movementBehaviour;
    [SerializeField] private RotationBehaviourSO rotationBehaviour;
    [SerializeField] private HealthBehaviourSO healthBehaviour;
    [SerializeField] private AttackBehaviourSO attackBehaviour;
    public MovementBehaviourSO MovementBehaviour => movementBehaviour;
    public RotationBehaviourSO RotationBehaviour => rotationBehaviour;
    public HealthBehaviourSO HealthBehaviour => healthBehaviour;
    public AttackBehaviourSO AttackBehaviour => attackBehaviour;

    public List<IBehaviour> lsBehaviours = new List<IBehaviour>();
    
   [SerializeField] private AnimationController anim;
    
   public AnimationController AnimationController => anim;
    
    #endregion
    
    #region PoolProperties

    public override void OnDeactivate()
    {
        BehaviourManager.I.RemoveBehaviourController(this);
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        CreateBehaviour();
        BehaviourManager.I.AddBehaviourController(this);
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }

    #endregion

    void CreateBehaviour()
    {
        lsBehaviours.Clear();

        if (movementBehaviour) movementBehaviour = Instantiate(movementBehaviour);
        if (rotationBehaviour) rotationBehaviour = Instantiate(rotationBehaviour);
        if (healthBehaviour) healthBehaviour = Instantiate(healthBehaviour);
        if (attackBehaviour) attackBehaviour = Instantiate(attackBehaviour);
        
        if (movementBehaviour)
        {
            lsBehaviours.Add(movementBehaviour);
            movementBehaviour.SetController(this);
        }
        
        if (healthBehaviour)
        {
            lsBehaviours.Add(healthBehaviour);
            healthBehaviour.SetController(this);
        }

        if (attackBehaviour)
        {
            lsBehaviours.Add(attackBehaviour);
            attackBehaviour.SetController(this);
        }

        if (rotationBehaviour)
        {
            lsBehaviours.Add(rotationBehaviour);
            rotationBehaviour.SetController(this);
        }
    }

    private void Update()
    {
        if (movementBehaviour)
            movementBehaviour.MovementOnUpdate();

        if (rotationBehaviour)
            rotationBehaviour.RotationOnUpdate();
        
        if(attackBehaviour)
            attackBehaviour.AttackOnUpdate();
    }
    
    public bool TryGetBehaviour<T>(out T result) where T : IBehaviour
    {
        foreach (IBehaviour behaviour in lsBehaviours)
        {
            if (behaviour is T) 
            { 
                result = (T)behaviour;
                return true;
            }
        }
        result = default(T);
        return false;
    }
}
