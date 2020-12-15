using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationEnemyAI : MonoBehaviour, IReactToHit
{    
    private NavMeshAgent _navMeshAgent = null;

    private PlayerController _playerController;
    private Transform _playerPos;

    private float _speedRotation = 50f;

    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private float _seeDistance = 20;

    [SerializeField] List<Vector3> _wayPoints = new List<Vector3>();
    private int _wayPointIndex = 0;

    bool _isAlive = true;

    private int IncrementWaypointIndex()
    {
        _wayPointIndex = (++_wayPointIndex) % _wayPoints.Count;
        return _wayPointIndex;
    }

    private const float _rayRechargeTimeMs = 1.5f;
    private float _rayTime = 0f;

    public void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.stoppingDistance = _stoppingDistance;
        _playerController = FindObjectOfType<PlayerController>();
        _playerPos = _playerController.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent.SetDestination(_wayPoints[IncrementWaypointIndex()]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAlive)
        {
            _navMeshAgent.ResetPath();

            Destroy(this.gameObject, 3f);
            return;
        }

        if (!IsPlayerSeen())
        {
            if (_navMeshAgent.stoppingDistance >=
                _navMeshAgent.remainingDistance)
            {
                _navMeshAgent.SetDestination(
                    _wayPoints[IncrementWaypointIndex()]);
            }
            return;
        }

        ToFollowThePlayer();

        _navMeshAgent.SetDestination(_playerPos.position);
        
        ShootThePlayerByRay();
    }

    private void ShootThePlayerByRay()
    {
        _rayTime += Time.deltaTime;
        if (_rayTime < _rayRechargeTimeMs)
            return;

        _rayTime = 0;
        Debug.DrawRay(transform.position,
            _playerPos.position, Color.red, 1400f);

        _playerController.ReactToHit(5);
    }

    private Ray GetRayToPlayer()
    {
        Vector3 playerDirection = _playerPos.position - transform.position;
        return new Ray(transform.position + new Vector3(0, 0.7f, 0),
            playerDirection);
    }

    bool IsPlayerSeen()
    {
        Ray rayToPlayer = GetRayToPlayer();
        float d = Vector3.Dot(Vector3.Normalize(transform.forward),
            Vector3.Normalize(rayToPlayer.direction));

        if (0.5f > Vector3.Dot(Vector3.Normalize(transform.forward), 
            Vector3.Normalize(rayToPlayer.direction)))
            return false;

        RaycastHit hit;
        if (Physics.Raycast(rayToPlayer, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hit.distance > _seeDistance || !hitObject.CompareTag("Player"))
                return false;
        }
        else return false;

        return true;
    }

    private void ToFollowThePlayer()
    {
        Vector3 direction = (_playerPos.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            lookRotation, Time.deltaTime * _speedRotation);
    }

    private int _health = 100;
    public void ReactToHit(int hitCount)
    {
        _health -= hitCount;

        if (_health <= 0)
            _isAlive = false;
    }
}
