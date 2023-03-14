using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{
    public List<HealthBehaviourSO> lsTargets { get; }

    public void AttackOnUpdate();
}
