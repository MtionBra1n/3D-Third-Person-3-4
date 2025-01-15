using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavMeshChase : MonoBehaviour
{
private static readonly int Hash_MovementSpeed = Animator.StringToHash("MovementSpeed");
    
    #region Inspector

    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float attackingDistance =.5f;


    [SerializeField] private bool showGizmos = true;
    #endregion
    
    private NavMeshAgent navMeshAgent;
    private EnemyBehaviour _enemyBehaviour;
    private bool chasePlayer;
    private bool attackReady;
    
    #region Unity Event Functions

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        navMeshAgent.autoBraking = true;
    }

    private void Update()
    {
        anim.SetFloat(Hash_MovementSpeed, navMeshAgent.velocity.magnitude);
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!navMeshAgent.isStopped && chasePlayer)
        {
            if (!attackReady)
            {
                navMeshAgent.destination = player.position;
            }

            SmoothLookAtPlayer();
        }
        
        if (distanceToPlayer <= attackingDistance && !attackReady)
        {
            attackReady = true;
            _enemyBehaviour.CanAttackPlayer(attackReady);
        }
        else if (distanceToPlayer > attackingDistance && attackReady)
        {
            attackReady = false;
            _enemyBehaviour.CanAttackPlayer(attackReady);
        }
    }

    #endregion
    
    #region Navigation
    
    public void StopChasingPlayer()
    {
        chasePlayer = false;
        navMeshAgent.isStopped = true;
    }

    public void ChasePlayer()
    {
        chasePlayer = true;
        navMeshAgent.isStopped = false;
    }
    
    #endregion
   
    #region Helper Methods

    private void SmoothLookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    
    #endregion
    

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackingDistance);
        }
    }

    #endregion
}
