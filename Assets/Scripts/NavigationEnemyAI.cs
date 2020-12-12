using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationEnemyAI : MonoBehaviour
{    
    private NavMeshAgent _navMeshAgent = null;

    private PlayerController _playerController;
    private Transform _playerPos;

    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private float _seeDistance = 20;

    [SerializeField] List<Vector3> _wayPoints = new List<Vector3>();
    private int _wayPointIndex = 0;

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
        RaycastHit hit;
        if (!IsPlayerSeen(out hit))
        {
            if (_navMeshAgent.stoppingDistance >=
                _navMeshAgent.remainingDistance)
            {
                _navMeshAgent.SetDestination(
                    _wayPoints[IncrementWaypointIndex()]);
            }

            return;
        }

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

        Debug.Log("Hit by " + this.name);

        _playerController.ReactToHit(5);
    }

    private Ray GetRayToPlayer()
    {
        Vector3 playerDirection = _playerPos.position - transform.position;
        return new Ray(transform.position + new Vector3(0, 0.7f, 0),
            playerDirection);
    }

    bool IsPlayerSeen(out RaycastHit hit)
    {
        Ray rayToPlayer = GetRayToPlayer();
        float d = Vector3.Dot(Vector3.Normalize(transform.forward),
            Vector3.Normalize(rayToPlayer.direction));

        if (0.5f > Vector3.Dot(Vector3.Normalize(transform.forward), 
            Vector3.Normalize(rayToPlayer.direction)))
        {
            hit = default;
            return false;
        }

        if (Physics.Raycast(rayToPlayer, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hit.distance > _seeDistance || !hitObject.CompareTag("Player"))
                return false;
        }
        else return false;

        ToFollowThePlayer();

        return true;
    }

    private void ToFollowThePlayer()
    {
        Vector3 targetPos = new Vector3(_playerPos.position.x,
            this.transform.position.y,
            _playerPos.position.z);
        this.transform.LookAt(targetPos);
    }


}
