using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject bulletPrefab; // กระสุนของปืนกระบอกนี้
    public Transform firePoint;     // จุดปล่อยกระสุน
    public float fireRate = 0.5f;   // ความรัว
    public float detectRange = 10f; // ระยะเรดาร์
    
    public int bonusDamage = 0;     

    // 👇 ฮานะเพิ่มช่องใส่เสียงปืนตรงนี้!
    [Header("Audio Settings")]
    public AudioClip shootSound; 

    private float nextFireTime = 0f;

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
        
        // 👇 สั่งให้เล่นเสียงปืนตรงปลายกระบอกปืน! ปังๆๆ!
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
        }
        
        Bullet b = bullet.GetComponent<Bullet>();
        b.Setup(shootDirection);
        b.damage += bonusDamage; 
    }
}