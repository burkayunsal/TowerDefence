using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Behaviours/Health")]

public class Health : HealthBehaviourSO
{
    
    [Header("Health Options")]
    [SerializeField] private float maxHealth;
    [SerializeField] private int prizeAmount;
    
    [Header("Animation")]
    [SerializeField] private Color onDamageTakenColor = Color.red;
    [SerializeField] private float onDamageTakenColorDuration = .15f;
    
    public override float CurrentHealth { get; set; }
    
    public override float MaxHealth => maxHealth;

    public override bool IsDead { get; protected set; }
    
    public override void TakeDamage(float damage)
    {
        if (IsDead) return;

        CurrentHealth -= damage;
        AnimateDamageTaken();
        if (CurrentHealth <= 0) 
            Die();
    }
    
    private void AnimateDamageTaken()
    {
        Controller.AnimationController.DoColorFlash(onDamageTakenColor, onDamageTakenColorDuration);
    }

    private void Die()
    {
        ScoreManager.I.ScoreGained(prizeAmount);
        OnDeath.Invoke(this);
        OnDeath.RemoveAllListeners();
        IsDead = true;
        CurrentHealth = 0;
        Controller.OnDeactivate();
    }
    
    public override void OnStart(BehaviourController controller)
    {
        ResetHealth();
    }

    void ResetHealth()
    {
        CurrentHealth = maxHealth;
    }
}
