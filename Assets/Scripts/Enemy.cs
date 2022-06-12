using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject barrel;
    public float health = 100;

   // float dist = Vector3.Distance(barrel.position, transform.position);


    private void Start()
    {
        
    }
    public void KillEnemy()
    {
        Destroy(gameObject);
    }

    public float UpdateHealth(float dist)
    {
        health -= dist;
        return health;
    }
}
