using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HealthBehaviourSO : BehaviourSO, IHealth
{
    public UnityEvent<HealthBehaviourSO> OnDeath { get; protected set; } = new UnityEvent<HealthBehaviourSO>();

    public abstract float CurrentHealth { get; set; }
    public abstract float MaxHealth { get; }
    public abstract bool IsDead { get; protected set; }

    public abstract void TakeDamage(float damage);
}
