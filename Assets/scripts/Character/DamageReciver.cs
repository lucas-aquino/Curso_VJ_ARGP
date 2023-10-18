using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciver : MonoBehaviour
{
    [SerializeField]
    private GameObject damageableObject;

    [SerializeField]
    private HitType damageType;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("harmful"))
        {
            IDamager damager = other.gameObject.GetComponent<IDamager>();
            damager.Attack(damageableObject.GetComponent<IDamageable>(), damageType);
            damager.Destruir();
        }
    }
}
