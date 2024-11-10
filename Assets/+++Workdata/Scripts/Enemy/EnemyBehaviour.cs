using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private static readonly int Hash_Hit = Animator.StringToHash("Hit");
    private static readonly int Hash_Dead = Animator.StringToHash("Dead");
    private static readonly int Hash_MovementType = Animator.StringToHash("MovementType");
    private static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");

    #region Inspector

    [SerializeField] int enemyHealth;
    [SerializeField] Animator animator;

    #endregion
    
    #region Private Variables

    #endregion
    
    
    public void GetDamage(int damage)
    {
        if (enemyHealth < 0) return;
        
        enemyHealth -= damage;

        animator.SetTrigger(Hash_ActionTrigger);
        if (enemyHealth < 0)
        {
            OnDeath();
        }
        else
        {
            OnHit();
        }
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
        animator.SetFloat(Hash_MovementType, movementTypeId);
    }
    
    public void OnAggroExit()
    {
        animator.SetFloat(Hash_MovementType, 0);
    }
    
}
