using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonShooting : MonoBehaviour
{
    public GameObject bulletPrefabs;        // Prefab vi�n ??n
    public Transform firePoint;             // V? tr� mi?ng r?ng
    public float bulletSpeed = 10f;         // T?c ?? bay
    public float shootingInterval = 0.5f;   // Th?i gian gi?a c�c ph�t b?n

    private float lastBulletTime;

    void Update()
    {
        if (Input.GetMouseButton(0)) // Gi? chu?t tr�i ?? b?n
        {
            if (Time.time - lastBulletTime > shootingInterval)
            {
                Fire();
                lastBulletTime = Time.time;
            }
        }
    }

    void Fire()
    {
        // T?o vi�n ??n t?i v? tr� mi?ng
        GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);

        // X�c ??nh h??ng b?n d?a v�o h??ng nh�n v?t
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        // �p d?ng v?n t?c theo chi?u nh�n v?t
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * bulletSpeed, 0f);

        // Xoay vi�n ??n theo chi?u b?n (n?u c?n l?t sprite)
        Vector3 bulletScale = bullet.transform.localScale;
        bulletScale.x = Mathf.Abs(bulletScale.x) * (direction > 0 ? 1 : -1);
        bullet.transform.localScale = bulletScale;
    }
}
