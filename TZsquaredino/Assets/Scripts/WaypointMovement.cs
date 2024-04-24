using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WaypointMovement : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyArray
    {
        public GameObject[] enemies;
    }

    public Transform[] waypoints;
    public EnemyArray[] enemyArrays;
    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isWalking = false;
    private bool isIdle = false;
    private bool atFinalWaypoint = false;
    private float timeAtFinalWaypoint = 0f;
    private float timeToRestart = 2f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the child objects.");
        }
        else
        {
            MoveToWaypoint(currentWaypointIndex);
        }

        if (navMeshAgent == null || animator == null)
        {
            Debug.LogError("NavMeshAgent component or Animator component is missing.");
        }
        else
        {
            MoveToWaypoint(currentWaypointIndex);
        }
    }

    void Update()
    {
        if (!isWalking && AreAllEnemiesDestroyed())
        {
            MoveToNextWaypoint();
        }

        if (currentWaypointIndex == waypoints.Length - 1 && !atFinalWaypoint)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.5f)
            {
                timeAtFinalWaypoint += Time.deltaTime;

                if (timeAtFinalWaypoint >= timeToRestart)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                timeAtFinalWaypoint = 0f;
            }
        }

        if (isWalking)
        {
            if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending)
            {
                isWalking = false;
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", true);
            }
        }
        else
        {
            animator.SetBool("IsIdle", false);
        }
    }

    void MoveToNextWaypoint()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }

        MoveToWaypoint(currentWaypointIndex);
    }

    void MoveToWaypoint(int index)
    {
        navMeshAgent.SetDestination(waypoints[index].position);
        isWalking = true;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
    }

    bool AreAllEnemiesDestroyed()
    {
        if (currentWaypointIndex < enemyArrays.Length)
        {
            GameObject[] enemiesForCurrentWaypoint = enemyArrays[currentWaypointIndex].enemies;

            if (enemiesForCurrentWaypoint != null && enemiesForCurrentWaypoint.Length > 0)
            {
                foreach (GameObject enemy in enemiesForCurrentWaypoint)
                {
                    if (enemy != null)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}