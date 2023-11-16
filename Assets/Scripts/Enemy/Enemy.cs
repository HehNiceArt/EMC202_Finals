using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;

    [Header("AI Behaviour")]
    [SerializeField] private Transform[] points;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int destPoint;
    [SerializeField] private float minRange;

    [Header("Follow")]
    [SerializeField] private float playerToEnemyDistance;
    [SerializeField] private bool playerDetected;

    [Header("Attack")]
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float radius;
    public CharacterController playerCollider;
    [Header("Animation")]
    [SerializeField] private Animator anim;

    float distance;
    void Update()
    {
        if(player == null)
        {
            return;
        }
        Follow();
        if (health <= 0)
        {
            Debug.Log("Enemy is killed");
            Destroy(enemy);
        }
    }
    private void FixedUpdate()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }
    void GoToNextPoint()
    {
        if(points.Length == 0)
        {
            return;
        }
        anim.SetBool("enemyWalking", true);
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void Follow()
    {
        float distanceToTarget = Vector3.Distance(enemy.transform.position, player.transform.position);
        float distanceToAttack = Vector3.Distance(startPoint.transform.position, player.transform.position);
        if(distanceToTarget < minRange)
        {
            playerDetected = true;
            if (playerDetected)
            {
                anim.SetBool("enemyWalking", false);
                if(distanceToAttack < minRange)
                {
                    anim.SetBool("enemyAttacking", true);
                    StartCoroutine("AttackInterval");
                }
                Vector3 fwd = player.transform.forward;
                fwd.y = 0;
                enemy.transform.rotation = Quaternion.LookRotation(fwd);
                //enemy.transform.LookAt(fwd);
                Vector3 pos = Vector3.MoveTowards(-transform.position, player.transform.position, playerToEnemyDistance);
                agent.SetDestination(pos);
            }
            else
            {
                playerDetected = false;
                GoToNextPoint();
            }
        }
    }
    public void Attack()
    {
        Debug.Log("player hit");
        playerCollider.GetComponent<playercomponent>().playerHealth -= enemyDamage;
        Debug.Log(playerCollider.GetComponent<playercomponent>().playerHealth -= enemyDamage);
        anim.SetBool("enemyAttacking", false);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        Debug.Log("oncontrollercolliderhit test");
        if (body == null || body.isKinematic)
        {
            return;
        }
    }
    IEnumerator AttackInterval()
    {
        yield return new WaitForSeconds(5f);
        //anim.SetBool("enemyAttacking", true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(enemy.transform.position, minRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere (endPoint.transform.position, radius);
        Gizmos.DrawWireSphere (startPoint.transform.position, radius);
    }
}
