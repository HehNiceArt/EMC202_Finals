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
        damage = GetComponentInParent<playercomponent>().playerDamage;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    public void Attack1()
    {
        Collider[] enemy = Physics.OverlapCapsule(startPoint.transform.position,endPoint.transform.position, radius, enemyLayer);
        foreach (Collider enemyGameObject in enemy)
        {
            enemyGameObject.GetComponent<Enemy>().health -= damage;

        }

    } 
    public void Attack2()
    {
        Collider[] enemy = Physics.OverlapCapsule(startPoint.transform.position,endPoint.transform.position, radius, enemyLayer);
        foreach (Collider enemyGameObject in enemy)
        {
            float attack2damage = damage + 2;
            enemyGameObject.GetComponent<Enemy>().health -= attack2damage;

        }

    } 
    public void Attack3()
    {
        Collider[] enemy = Physics.OverlapCapsule(startPoint.transform.position,endPoint.transform.position, radius, enemyLayer);
        foreach (Collider enemyGameObject in enemy)
        {
            float attack3damage = damage + 4;
            enemyGameObject.GetComponent<Enemy>().health -= attack3damage;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPoint.transform.position, radius);
        Gizmos.DrawWireSphere(endPoint.transform.position, radius);
    }
}
