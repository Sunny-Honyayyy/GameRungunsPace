using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject bulletPrefab; // กระสุนของปืนกระบอกนี้
    public Transform firePoint;     // จุดปล่อยกระสุน
    public float fireRate = 0.5f;   // ความรัว
    public float detectRange = 10f; // ระยะเรดาร์

    private float nextFireTime = 0f;

    // ฟังก์ชันนี้จะถูก Player สั่งให้ทำงาน
    public void TryShoot(Vector2 defaultDirection)
    {
        if (Time.time >= nextFireTime)
        {
            Shoot(defaultDirection);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot(Vector2 facingDirection)
    {
        Vector2 shootDirection = facingDirection; 
        EnemyHealth[] enemies = Object.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (EnemyHealth enemy in enemies)
        {
            // วัดระยะจากปลายกระบอกปืนไปหาศัตรู
            float distance = Vector2.Distance(firePoint.position, enemy.transform.position);
            
            if (distance < closestDistance && distance <= detectRange)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        if (closestEnemy != null)
        {
            shootDirection = (closestEnemy.position - firePoint.position).normalized;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Setup(shootDirection);
    }
}