using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class NavMeshEvenemyBehaviour : MonoBehaviour
{
    private static readonly int Hash_MovementSpeed = Animator.StringToHash("MovementSpeed");

    #region Inspector

    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private float aggroRange = 10f;
    [SerializeField] private float rotationDistance = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float attackingDistance = .5f;
    [SerializeField] private SphereCollider aggroTrigger;

    [FormerlySerializedAs("showAggroRange")]
    [Header("Gizmos")] 
    [SerializeField] private bool showGizmos = true;

    #endregion

    private NavMeshAgent navMeshAgent;
    private bool isChasing;
    private bool isAttacking;

    #region Unity Event Functions

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateAggroTriggerRadius();
    }

    private void Update()
    {
        anim.SetFloat(Hash_MovementSpeed, navMeshAgent.velocity.magnitude);
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        

        if (isChasing && !isAttacking)
        {
            navMeshAgent.destination = player.position;
        }
        
        if (distanceToPlayer <= rotationDistance)
        {
            SmoothLookAtPlayer();
        }

        if (distanceToPlayer <= attackingDistance)
        {
            isAttacking = true;
        }
        else if (isAttacking && distanceToPlayer > attackingDistance)
        {
            isAttacking = false;
        }
    }

    private void OnValidate()
    {
        UpdateAggroTriggerRadius();
    }

    #endregion

    #region Chasing Logic

    private void StartChasingPlayer()
    {
        if (!isChasing)
        {
            isChasing = true;
            navMeshAgent.isStopped = false;
        }
    }

    private void StopChasingPlayer()
    {
        if (isChasing)
        {
            isChasing = false;
            navMeshAgent.isStopped = true;
        }
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aggroRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rotationDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackingDistance);
        }
    }

    #endregion

    #region Helper Methods

    private void UpdateAggroTriggerRadius()
    {
        if (aggroTrigger != null)
        {
            aggroTrigger.radius = aggroRange;
        }
    }

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
}
