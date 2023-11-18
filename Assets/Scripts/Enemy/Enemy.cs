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
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float radius;
    [SerializeField] private bool isAttacking;
    [SerializeField] private int comboCount;
    public CharacterController playerCollider;
    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private Animator playerAnim;

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
            if (playerDetected == true)
            {
                anim.SetBool("enemyWalking", false);
                if( distanceToAttack > attackRadius)
                {
                    anim.SetBool("enemyAttacking", false);
                }
                if(distanceToAttack < attackRadius)
                {
                    anim.SetBool("enemyAttacking", true);
                }
                Vector3 fwd = player.transform.forward;
                fwd.y = 0;
                enemy.transform.rotation = Quaternion.LookRotation(fwd);
                // move the enemy towards the player once within range
                Vector3 pos = Vector3.MoveTowards(enemy.transform.position, player.transform.position, playerToEnemyDistance);
                agent.SetDestination(pos);
                Debug.Log(agent.SetDestination(pos));
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
        playerAnim.SetTrigger("isHurt");
        playerCollider.GetComponent<playercomponent>().playerHealth -= enemyDamage;
        anim.SetBool("enemyAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(enemy.transform.position, minRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere (transform.position, attackRadius);
    }
}
