using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float impactField;
    public float force;
    public LayerMask explosionLayer;
    public GameObject explosionPrefab;
    public GameObject deathSoundPrefab;

    private void Start()
    {
        InputManager.OnStartExplode += Explosion;
    }

    void Update()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }

    private void Explosion()
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

    private void InstantiateVisuals()
    {
        if (deathSoundPrefab != null)
        {
            Instantiate(deathSoundPrefab);
        }
    }

    private void OnDestroy()
    {
        InputManager.OnStartExplode -= Explosion;
    }
}
