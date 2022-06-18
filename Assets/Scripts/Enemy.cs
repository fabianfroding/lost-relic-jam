using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public const int StartHealth = 100;
    int health;

    public GameManager gameManager;

    public float distance;

    [SerializeField] private int scoreYield = 1;
    [SerializeField] private GameObject deathSoundPrefab;
    [SerializeField] private GameObject deathSFXPrefab;
    [SerializeField] private GameObject hitSFXPrefab;

    public static event Action<int> OnEnemyDeath;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)health);
            //OnHealthChange?.Invoke((float)Health / StartHealth);
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
        InstantiateDeathVisuals();
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
        InstantiateHitVisuals();

        if(Health <= 0)
        {
            //OnDead?.Invoke();
            KillEnemy();
        }
        else
        {
            OnHit?.Invoke();   
        }
    }

    private void InstantiateDeathVisuals()
    {
        if (deathSoundPrefab != null)
        {
            GameObject deathSound = Instantiate(deathSoundPrefab);
            deathSound.transform.position = transform.position;
            deathSound.transform.parent = null;
        }

        if (deathSFXPrefab != null)
        {
            GameObject deathSFX = Instantiate(deathSFXPrefab);
            deathSFX.transform.position = transform.position;
            deathSFX.transform.parent = null;
        }
    }

    private void InstantiateHitVisuals()
    {
        if (hitSFXPrefab != null)
        {
            GameObject hitSFX = Instantiate(hitSFXPrefab);
            hitSFX.transform.position = transform.position;
            hitSFX.transform.parent = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            int colDamage = 0;
            float collisionSpeed = collision.relativeVelocity.magnitude;
            if(collisionSpeed > 5)
            {
                colDamage = 25;
            }
            else if((collisionSpeed < 5) && (collisionSpeed > 4))
            {
                colDamage = 20;
            }
            else if ((collisionSpeed < 4) && (collisionSpeed > 3))
            {
                colDamage = 15;
            }
            else if ((collisionSpeed < 3) && (collisionSpeed > 2))
            {
                colDamage = 10;
            }
            else if ((collisionSpeed < 2) && (collisionSpeed > 1))
            {
                colDamage = 5;
            }
            this.Hit(colDamage);
        }
    }

}
