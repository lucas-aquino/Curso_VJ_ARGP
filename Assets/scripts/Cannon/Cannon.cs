using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float bulletDamage = 10f;

    [SerializeField]
    private int bulletAmount = 50;

    [SerializeField]
    private float timeMax = 0.1f;

    [SerializeField]
    private float timeMin = 2f;


    [SerializeField]
    private float _maxLife = 50f;

    private float _currentLife = 50f;

    [SerializeField]
    private Transform muzzle;

    [SerializeField] 
    private GameObject bulletPrefab;

    [SerializeField]
    private float fuerza = 500f;

    private Bullet _bullet;

    public float MaxLife
    {
        get
        {
            return _maxLife;
        }
    }

    public float CurrentLife
    {
        get
        {
            return _currentLife;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > MaxLife)
            {
                value = MaxLife;
            }

            _currentLife = value;
        }
    }

    public bool IsDead
    {
        get { return CurrentLife <= 0; }
    }

    public bool IsTackingDamage => false;

    public string Tag => gameObject.tag;

    private void Start()
    {
        _bullet = bulletPrefab.GetComponent<Bullet>();
        _bullet.Fuerza = fuerza;
        _bullet.Damage = bulletDamage;

        CurrentLife = MaxLife;

        StartCoroutine(Fire());
    }

    private void Update()
    {
        if(IsDead)
        {
            Destruir();
        }
    }

    private void Atack()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(muzzle.forward * _bullet.Fuerza * 5f, ForceMode.Force);
        bulletAmount--;
    }


    IEnumerator Fire()
    {
        while (bulletAmount > 0)
        {
            Atack();
            float waitingTime = Random.Range(timeMin, timeMax);
            yield return new WaitForSeconds(waitingTime);
        }
    }

    public void TakeDamage(float damage, HitType tipo)
    {
        if (!IsDead)
        {
            damage *= ((float)tipo / 100f);

            CurrentLife -= damage;
        }
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}
