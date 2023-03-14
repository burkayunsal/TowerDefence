using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Behaviours/Attack")]
public class  Attack : AttackBehaviourSO
{
    [Header("Attack")]
    [SerializeField] private float attackRange = 100;
    public float attackDamage = 20;
    [SerializeField] private float delayBetweenAttacks = 2f;
    private float timer = 0;
    
    [Header("Bullet")]
    [SerializeField] private Vector2 bulletSpawnPosOffset;
    
    [Header("Animation")]
    [SerializeField] private float punchScaleDuration = 0.5f;
    [SerializeField] private float punchScaleAmount = 0.1f;
    
    public override List<HealthBehaviourSO> lsTargets { get; protected set; } = new List<HealthBehaviourSO>();

    public override void OnStart(BehaviourController controller)
    {
        attackDamage = Random.Range(10, 50);
    }

    public override void AttackOnUpdate()
    {
        if (!IsActive) return;
        FindTarget(); 
        CheckAttack();
    }
    
    void FindTarget()
    {
        lsTargets.Clear();
        HealthBehaviourSO target = BehaviourManager.I.GetNearestHealthBehaviour(Controller.transform.localPosition, attackRange);
        if (target) 
            lsTargets.Add(target);
    }
    
    void CheckAttack()
    {
        if (lsTargets.Count == 0) return;
        
        if (timer < delayBetweenAttacks)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            AnimateAttack();
            foreach (HealthBehaviourSO target in lsTargets)
            {
                BulletBehaviour(target);
            }
        }
    }

    private void AnimateAttack()
    {
        Controller.AnimationController.PunchScale(punchScaleAmount, punchScaleDuration);
    }
    
    void BulletBehaviour(HealthBehaviourSO target)
    {
        Vector2 _offset = (Controller.transform.right * bulletSpawnPosOffset.x) + (Controller.transform.up * bulletSpawnPosOffset.y);
        Vector2 spawnPos = (Vector2)Controller.transform.position + _offset;

        Bullet b = PoolHandler.I.GetObject<Bullet>();
        b.transform.position = spawnPos;
        b.transform.rotation = Controller.transform.rotation;
        b.transform.SetParent(Controller.transform);
        
        if (!b.TryGetBehaviour<Movement>(out var movement))
        {
            Debug.LogError("No CommandableMovementBehaviour found on projectile");
            return;
        }
        
        movement.MoveTarget = target.Controller;
        
        movement.OnTargetReached.AddListener((target) =>
        {
            target.HealthBehaviour.TakeDamage(attackDamage);
            b.OnDeactivate();
        });

        target.OnDeath.AddListener((target) =>
        {
            b.OnDeactivate();
        });
    }
   
}
