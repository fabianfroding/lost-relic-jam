using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovableObjectType
{
    LIGHT,
    MEDIUM,
    CHONKY
}

public class MovableObject : MonoBehaviour
{
    private Rigidbody2D rb;
    public MovableObjectType type;

    private const float FAST_SPEED = 30;
    private const float MED_SPEED = 20;
    private const float SLOW_SPEED = 10;
    private const float SNAIL_SPEED = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if hit enemy, deal damage
        if (other.gameObject.CompareTag(EditorConstants.TAG_ENEMY))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            // Debug.Log(GetDamage());
            enemy.Hit(GetDamage());
        }

        if (other.gameObject.CompareTag(EditorConstants.TAG_EXPLOSIVE))
        {
            if (CanExplodeExplosive())
            {
                other.gameObject.GetComponent<Explosive>().DelayedExplode();
            }
        }
    }

    private bool CanExplodeExplosive()
    {
        float speed = rb.velocity.magnitude;
        if (ExplosiveSelect.Instance.hasTriggeredBarrel)
        {
            if (speed > SNAIL_SPEED) 
            { 
                return true; 
            }
        }
        return false;
    }

    private int GetDamage()
    {
        float speed = rb.velocity.magnitude;
        int damage = 0;
        // Debug.Log(speed);
        
        if (speed > FAST_SPEED) { damage = 100; }
        else if (speed > MED_SPEED && speed <= FAST_SPEED) { damage = 75; }
        else if (speed > SLOW_SPEED && speed <= MED_SPEED) { damage = 50; }
        else if (speed > SNAIL_SPEED && speed <= SLOW_SPEED) { damage = 30; }
        else { damage = 10; }

        switch(type)
        {
            case MovableObjectType.CHONKY:
                damage = damage * 3;
                break;
            case MovableObjectType.MEDIUM:
                damage = damage * 2;
                break;
            case MovableObjectType.LIGHT:
                break;
        }

        return damage;
    }


}
