using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    private static readonly int Hash_Hit = Animator.StringToHash("Hit");
    private static readonly int Hash_Dead = Animator.StringToHash("Dead");
    private static readonly int Hash_MovementType = Animator.StringToHash("MovementType");
    private static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
    private static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
    
    #region Inspector

    [Header("Enemy Health")] 
    [SerializeField] private int maxEnemyHealth = 100;

    [SerializeField] private Image healthbar;
    
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] private float blendSpeed = 1;

    [Header("AttackTypes")] 
    
    [SerializeField] private  int attackId;
    [SerializeField] private  int attackDamage;
    [SerializeField] private float attackTime;
    #endregion

    private float attackTimer;
    private bool canAttack;
    private int currentHealthPoints;

    private void Start()
    {
        currentHealthPoints = maxEnemyHealth;
        RefreshHealthbar();
    }

    private void Update()
    {
        if (canAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
            {
                AttackPlayer();
            }
        }
    }

    public void CanAttackPlayer(bool value)
    {
        canAttack = value;
    }
    
    void AttackPlayer()
    {
        animator.SetTrigger(Hash_ActionTrigger);
        animator.SetInteger(Hash_ActionId, attackId);
        attackTimer = attackTime;
    }
    
    public void GetDamage(int damage)
    {
        if (currentHealthPoints < 1) return;
        
        currentHealthPoints -= damage;

        animator.SetTrigger(Hash_ActionTrigger);
        if (currentHealthPoints < 1)
        {
            OnDeath();
        }
        else
        {
            OnHit();
        }
        RefreshHealthbar();
    }
    
    public void OnDeath()
    {
        animator.SetTrigger(Hash_Dead);
    }
    
    public void OnHit()
    {
        animator.SetTrigger(Hash_Hit);
    }
    
    public void OnAggroEnter(int movementTypeId)
    {
        print("check");
        StartCoroutine(AggroMovementType(1));
    }
    
    public void OnAggroExit()
    {
        StartCoroutine(AggroMovementType(0));
    }

    void RefreshHealthbar()
    {
        healthbar.fillAmount =  (float)currentHealthPoints / (float)maxEnemyHealth;
    }
    
    
    IEnumerator AggroMovementType(float targetValue)
    {
        while (Mathf.Abs(animator.GetFloat(Hash_MovementType) - targetValue) > 0.01)
        {
            float movementTypeValue =
                ValueInterpolator.MoveTowards(animator.GetFloat(Hash_MovementType), 
                    targetValue, blendSpeed, Time.deltaTime);
            
            animator.SetFloat(Hash_MovementType, movementTypeValue);
            yield return null;
        }
        
        animator.SetFloat(Hash_MovementType, targetValue);
    }
}
