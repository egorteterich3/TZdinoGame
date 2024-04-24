using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject hitEffectEnemy;
    [SerializeField] private GameObject hitEffectOther;
    [SerializeField] private float lifeTime = 4f;

    private Vector3 direction;
    private float elapsedTime = 0f;

    private void Start()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    }

    private void Update()
    {
        MoveBullet();
        DestroyAfterLifetime();
    }

    private void MoveBullet()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void DestroyAfterLifetime()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyBullet();
        Instantiate(hitEffectOther, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Kill();
            Instantiate(hitEffectEnemy, transform.position, Quaternion.identity);
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
