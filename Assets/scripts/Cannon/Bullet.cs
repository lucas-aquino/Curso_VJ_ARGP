using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamager
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float _fuerza = 500f;

    [SerializeField]
    private float _damage = 10f;

    public float Fuerza
    {
        get { return _fuerza; }
        set { 
            if(value < 0f)
            {
                value = 0f;
            }
            _fuerza = value;
        }
    }

    public float Damage
    {
        get { return _damage; }
        set
        {
            if(value < 0f)
            {
                value = 1f;
            }
            _damage = value;
        }
    }

    private void Start()
    {
        Invoke(nameof(Destruir), 5);
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
