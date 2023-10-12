using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour, IDamager
{
    [SerializeField]
    private float _damage = 10f;

    public float Damage { 
        get { return _damage; } 
        set { _damage = value; }
    }

    public void Destruir()
    {
        
    }
}
