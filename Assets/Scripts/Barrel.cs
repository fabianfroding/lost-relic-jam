using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float impactField;
    public float force;
    public LayerMask explosionLayer;
    public GameObject explosionPrefab;
    public GameObject deathSoundPrefab;

    public bool isSelected;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
    }

    void Update()
    {
    }

    public void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, explosionLayer);
        foreach (Collider2D obj in objects)
        {
            Vector2 dir = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
        InstantiateVisuals();
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
