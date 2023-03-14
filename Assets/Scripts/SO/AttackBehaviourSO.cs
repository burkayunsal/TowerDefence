using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviourSO : BehaviourSO, IAttack
{
    public abstract List<HealthBehaviourSO> lsTargets { get; protected set; }

    public abstract void AttackOnUpdate();
}
