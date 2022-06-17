using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int StartHealth = 100;
    [SerializeField]
    int health;

    public GameManager gameManager;

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
        Health -= damage;

        if(Health <= 0)
        {
            //OnDead?.Invoke();
            // Here I tried it again to somehow remove the impacted enemies without much success
            gameManager.enemyNumberAtStart--;
            Debug.Log("Enemies left: " + gameManager.enemyNumberAtStart);

            Destroy(gameObject);
        }
        else
        {
            OnHit?.Invoke();
        }
    }

}
