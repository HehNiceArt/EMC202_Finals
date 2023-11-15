using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
   

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("Enemy is killed");
            Destroy(enemy);
        }
    }
   
}
