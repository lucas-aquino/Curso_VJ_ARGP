using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciver : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HitType damageType;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("bullet"))
        {
            IDamager damager = other.gameObject.GetComponent<IDamager>();
            playerController.TakeDamage(damager.Damage * (float)damageType, damageType);
            damager.Destruir();
        }

        if (other.tag.Equals("punch"))
        {

        }
    }
}
