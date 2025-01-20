using UnityEngine;
using UnityEngine.AI;


enum EnemyPosBehaviour
{
    RandomPos, 
    FrontPos, 
    BackPos, 
    SitePos
}
public class NavMeshChase : MonoBehaviour
{
private static readonly int Hash_MovementSpeed = Animator.StringToHash("MovementSpeed");
    
    #region Inspector

    [SerializeField] private EnemyPosBehaviour enemyPosBehaviour;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float attackingDistance =.5f;
    [SerializeField] private float rotationDistance =4f;
    [SerializeField] private float positioningDistance = 3f;

    [SerializeField] private bool showGizmos = true;
    #endregion
    
    private NavMeshAgent navMeshAgent;
    private EnemyBehaviour _enemyBehaviour;
    private bool chasePlayer;
    private bool attackReady;
    private bool hasPositioningPoint;
    
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

        if (!navMeshAgent.isStopped && chasePlayer )
        {
            if (!attackReady && !hasPositioningPoint)
            {
                navMeshAgent.destination = player.position;
            }
        }
        
        if (distanceToPlayer <= rotationDistance)
        {
            SmoothLookAtPlayer();
        }

        if (distanceToPlayer <= positioningDistance && !hasPositioningPoint)
        {
            hasPositioningPoint = true;
            switch (enemyPosBehaviour)
            {
                case EnemyPosBehaviour.RandomPos:
                    navMeshAgent.destination = player.gameObject.GetComponent<PlayerPositioningBehaviour>().GetRandomPositioningPoint().position;
                    break;
                
                case EnemyPosBehaviour.BackPos:
                    navMeshAgent.destination = player.gameObject.GetComponent<PlayerPositioningBehaviour>().GetBackPositioningPoint().position;
                    break;
                
                case EnemyPosBehaviour.FrontPos:
                    navMeshAgent.destination = player.gameObject.GetComponent<PlayerPositioningBehaviour>().GetFrontPositioningPoint().position;
                    break;
                
                case EnemyPosBehaviour.SitePos:
                    navMeshAgent.destination = player.gameObject.GetComponent<PlayerPositioningBehaviour>().GetSitePositioningPoint().position;
                    break;
            }

        }
        else if (distanceToPlayer > positioningDistance && hasPositioningPoint)
        {
            hasPositioningPoint = false;
        }
        
        if (distanceToPlayer <= attackingDistance && !attackReady)
        {
            attackReady = true;
            StopChasingPlayer();
            _enemyBehaviour.CanAttackPlayer(attackReady);
        }
        else if (distanceToPlayer > attackingDistance && attackReady)
        {
            attackReady = false;
            ChasePlayer();
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
        if (!showGizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackingDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rotationDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, positioningDistance);
    }

    #endregion
}
