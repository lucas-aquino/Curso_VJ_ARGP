using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour, IDamager
{
    [SerializeField]
    private float _damage = 10f;

    private bool _isAttack = false;

    public float Damage { 
        get { return _damage; } 
        set { _damage = value; }
    }

    public bool IsAttack 
    {
        get { return _isAttack; }
    }

    public void Attack(IDamageable damageable, HitType hitType)
    {
        if (damageable.Tag.Equals("player"))
            return;

        damageable.TakeDamage(_damage, hitType);
    }

    public void Destruir() 
    { 
        
    }
}
