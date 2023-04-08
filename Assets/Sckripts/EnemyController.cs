using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    public float viewRadius;
    [Range(0f, 360f)] public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform target;
    private float _distToTarget;
    private bool _isTargetInView;
    private Animator _animator;
    public float distForAttack = 2;
    public float distBreakAttack = 0.5f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindVisibleTargets();
        MoveEnemy();
    }
    private void FindVisibleTargets()
    {
        if (Physics.CheckSphere(transform.position, viewRadius, targetMask))
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToTarget, viewRadius, obstacleMask))
                {
                    _isTargetInView = true;
                    print("Target in view");
                }

            }
        }
        else
        {
            _isTargetInView = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngle1 = DirectionFromAngle(transform.eulerAngles.y, -viewAngle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(transform.eulerAngles.y, viewAngle / 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle1 * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle2 * viewRadius);

        if (_isTargetInView)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
        }

    }
    private Vector3 DirectionFromAngle(float angleY, float angleView)
    {
        angleView += angleY;
        return new Vector3(Mathf.Sin(angleView * Mathf.Deg2Rad), 0, Mathf.Cos(angleView * Mathf.Deg2Rad));
    }
    private void MoveEnemy()
    {
        _distToTarget = Vector3.Distance(transform.position, target.position);
        if (_isTargetInView)
        {
            if (_distToTarget > distForAttack + distBreakAttack)
            {
                SetStateEnemy(true, false, 1);
                _agent.SetDestination(target.position);
            }
            if (_distToTarget <= distForAttack)
            {
                SetStateEnemy(false, true, 0);
            }
        }
        else
        {
            SetStateEnemy(false, false, 0);
        }
    }
    private void SetStateEnemy(bool enabledAgent, bool isAttack, float animSpeed)
    {
        _agent.enabled = enabledAgent;
        _animator.SetBool("Attack", isAttack);
        _animator.SetFloat("Speed", animSpeed);

    }
}
