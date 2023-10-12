using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
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
    private Transform muzzle;

    [SerializeField] 
    private GameObject bulletPrefab;

    [SerializeField]
    private float fuerza = 500f;

    private Bullet _bullet;

    private void Start()
    {
        _bullet = bulletPrefab.GetComponent<Bullet>();
        _bullet.Fuerza = fuerza;
        _bullet.Damage = bulletDamage;

        StartCoroutine(Fire());
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
}
