using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //[SerializeField] private Animator anim;
    Animator anim;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damage;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    public void EndAttack()
    {
        anim.SetBool("Attack1", false);
    }
    public void Attack()
    {
        Collider[] enemy = Physics.OverlapCapsule(startPoint.transform.position,endPoint.transform.position, radius, enemyLayer);
        foreach (Collider enemyGameObject in enemy)
        {
            Debug.Log("hit enemy");
            enemyGameObject.GetComponent<Enemy>().health -= damage;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPoint.transform.position, radius);
        Gizmos.DrawWireSphere(endPoint.transform.position, radius);
    }
}
