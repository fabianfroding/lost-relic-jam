using System.Collections;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float impactField;
    public float force;
    public GameObject explosionPrefab;
    public GameObject deathSoundPrefab;

    public bool isSelected;
    [SerializeField] private float explodeDelay = 0.3f;

    //Enemy, damage and score vars
    public int damage = 5;
    public GameManager gameManager;

    [SerializeField]
    Enemy[] enemies;

    #region Unity Callback Functions
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField);
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag(EditorConstants.TAG_ENEMY))
            {
                Vector2 dir = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
                
                obj.GetComponent<Enemy>().Hit(CalculateDamage(damage, obj.gameObject));
                Debug.Log("Damage: " + CalculateDamage(damage, obj.gameObject));
            }

            // if affected object is a barrel, explode it too
            if (obj.gameObject.CompareTag(EditorConstants.TAG_EXPLOSIVE) && obj.gameObject != this.gameObject)
            {
                obj.GetComponent<Explosive>().Explode();
            }

            /*if (gameManager.enemies.Length > 0)
            {
                gameManager.LevelFailed();
            }

            else if (gameManager.enemies.Length == 0)
            {
                gameManager.LevelWon();
            }*/
        }

        //JUST FOR TEST!!! IT SHOULD BE -- WHEN PLACING SHROOMS - remaining placeable shroom count
        //SetShroomNr.shroomNr -= 1;
        //SAME
        //gameManager.Win();

        InstantiateVisuals();
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
        StopCoroutine(ExplodeDelayed());
        StartCoroutine(ExplodeDelayed());
    }

    private IEnumerator ExplodeDelayed()
    {
        yield return new WaitForSeconds(explodeDelay);
        Destroy(gameObject);
    }

    public int CalculateDamage(int baseDamage, GameObject target)
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        int newDamage = baseDamage;

        if (distance < 0.5)
        {
            newDamage = 100;
        }
        else if (distance < 1)
        {
            newDamage = 75;
        }
        else if (distance < 2)
        {
            newDamage = 50;
        }
        else if (distance < 3.5)
        {
            newDamage = 25;
        }

        return newDamage;
    }

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
}
