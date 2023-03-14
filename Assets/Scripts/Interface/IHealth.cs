using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
   public float CurrentHealth { get; }
   
   public float MaxHealth { get;  }

   public bool IsDead { get; }

   public void TakeDamage(float damage);
}
