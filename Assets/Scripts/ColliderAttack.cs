using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    [SerializeField] bool destroyEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Idamageable damageable))
        {
            damageable.GetDamage(destroyEnemy);
        }
    }
}
