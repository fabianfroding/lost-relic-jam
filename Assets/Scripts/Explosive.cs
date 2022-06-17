using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Explosive : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public static event Action OnExplosion;

    public float impactField;
    public LayerMask impactLayer;
    public float force;
    public GameObject explosionPrefab;
    public GameObject deathSoundPrefab;
    public bool isSelected;
    [SerializeField] private float explodeDelay = 0.3f;

    // idk weird ass bug that explodes same shroom many times if multiple other shrooms overlap
    private bool hasAlreadyExploded = false;

    //Enemy, damage and score vars
    public int damage = 0;

    #region Unity Callback Functions
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {

    }

    private void OnMouseEnter()
    {
        spriteRenderer.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            spriteRenderer.material.color = Color.white;
        }
        else if (isSelected)
        {
            spriteRenderer.material.color = Color.red;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }
    #endregion

    public void Explode()
    {
        // prevent stackoverflows by destroying it first so other explosives cant see it in their explosions
        Destroy(this.gameObject);
        
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, impactLayer);
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag(EditorConstants.TAG_ENEMY))
            {
                Vector2 dir = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);

                //Calculate damage + assign score
                damage = CalculateDamage(damage, obj.gameObject);
		        obj.GetComponent<Enemy>().Hit(damage);
                // Debug.Log("Damage: " + damage);
                SetScore.scoreValue += damage; //score works this way, can't make it work the other way

            }

            // if affected object is explosive, explode it too, and is not own explosive
            if (obj.gameObject.CompareTag(EditorConstants.TAG_EXPLOSIVE) && obj.gameObject != this.gameObject)
            {
                obj.GetComponent<Explosive>().DelayedExplode();
            }
        }

        OnExplosion?.Invoke();
        InstantiateVisuals();
        hasAlreadyExploded = true;
    }

    public void DelayedExplode()
    {
        StopCoroutine(ExplodeAfterDelay());
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explodeDelay);
        if (!hasAlreadyExploded)
        {
            Explode();
        }
    }

    public int CalculateDamage(int newDamage, GameObject target)
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

        if (distance < 0.5)
        {
            newDamage = 100;
        }
        else if (distance < 1 && distance > 0.5)
        {
            newDamage = 75;
        }
        else if (distance < 2 && distance > 1)
        {
            newDamage = 50;
        }
        else if (distance < 3 && distance > 2)
        {
            newDamage = 25;
        }
        else
        {
            newDamage = 15;
        }

        return newDamage;
    }

    //should check if there are any enemies left, if no level won, if yes level failed
   /* void CheckRemainingEnemies()
    {
        Debug.Log("Check remaining enemies called");
        if (gameManager.enemyNumberAtStart <= 0)
        {
            gameManager.LevelWon();
        }
        else if (gameManager.enemyNumberAtStart > 0)
        {
            gameManager.LevelFailed();
        }
    }*/

    public void SetSelected()
    {
        spriteRenderer.material.color = Color.red;
        isSelected = true;
    }

    public void SetDeselect()
    {
        spriteRenderer.material.color = Color.white;
        isSelected = false;
    }

    private void CheckRbInterpolation()
    {
        if (rb.velocity.magnitude > 40)
        {
            // continuous more expensive, use to prevent phasing through walls
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }

    private void InstantiateVisuals()
    {
        if (deathSoundPrefab != null)
        {
            Instantiate(deathSoundPrefab);
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    int collidedEnemyHealth = collision.gameObject.GetComponent<Enemy>().Health;
    //    int newHealth = collidedEnemyHealth - CalculateDamage(0, collision.gameObject); //adding 0 as base dmg since im not quite sure what its purpose is 
    //    collision.gameObject.GetComponent<Enemy>().Health = newHealth;
    //}

}
