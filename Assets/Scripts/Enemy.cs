using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int StartHealth = 100;
    [SerializeField]
    int health;

    public float distance;

    [SerializeField] private int scoreYield = 1;

    public static event Action<int> OnEnemyDeath;

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
    }

    public void KillEnemy()
    {
        OnEnemyDeath?.Invoke(scoreYield);
        Destroy(gameObject);
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
