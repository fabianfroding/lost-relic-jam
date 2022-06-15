using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Explosive barrel;

    public int StartHealth = 100;
    [SerializeField]
    int health;

    public int damage = 5;
    public float distance;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / StartHealth);
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit;



   // float dist = Vector3.Distance(barrel.position, transform.position);


    private void Start()
    {
        Health = StartHealth;
        barrel = GetComponent<Explosive>();
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }

    //NEED HELP!! 
    public float CalculateDamage()
    {
        distance = Vector3.Distance(gameObject.transform.position, barrel.transform.position);

        if (distance < 0.5)
        {
            damage = 100;
        }
        else if (distance < 1)
        {
            damage = 75;
        }
        else if (distance < 2)
        {
            damage = 50;
        }
        else if (distance < 3.5)
        {
            damage = 25;
        }

        return damage;
    }
    /* public float UpdateHealth(float dist)
     {
         health -= dist;
         return health;
     }*/

    internal void Hit(int damage)
    {
        if(Health <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

}
