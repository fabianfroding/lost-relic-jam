using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float impactField;
    public float force;
    public LayerMask impactLayer;
    public GameObject explosionPrefab;
    public GameObject deathSoundPrefab;

    public bool isSelected;

    private const float explodeDelay = 2f;
    private bool isExploding;
    [SerializeField] private float explodeTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    void Update()
    {
        ExplodeCountdown();
        CheckRbInterpolation();
    }

    public void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, impactLayer);
        foreach (Collider2D obj in objects)
        {
            Vector2 dir = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }
            // if affected object is a barrel, explode it too
            if (obj.gameObject.CompareTag("Barrel") && obj.gameObject != this.gameObject)
            {
                Barrel affectedBarrel = obj.GetComponent<Barrel>();
                // does not explode itself again
                if (affectedBarrel != null)
                {
                    if (!affectedBarrel.isExploding)
                    {
                        obj.GetComponent<Barrel>().BeginExplode();
                    }
                }
            }
        }
        Destroy(gameObject);
        InstantiateVisuals();
    }

    public void BeginExplode()
    {
        isExploding = true;
        explodeTimer = explodeDelay;
        Debug.Log("Beginning Explosion");
    }

    // terrible naming yes ik
    private void ExplodeCountdown()
    {
        // if affected by impact, explode in 2 secs
        if (isExploding)
        {
            explodeTimer -= Time.deltaTime;
            if (explodeTimer <= 0)
            {
                Explosion();
            }
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
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

    // private void OnDestroy()
    // {
   
    // }
}
