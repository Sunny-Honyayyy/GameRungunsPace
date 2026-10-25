using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth;

    [Header("Drop System")]
    // เปลี่ยนมาใช้ [] เพื่อให้ใส่ไอเทมดรอปได้หลายๆ ชนิด!
    public GameObject[] dropItemPrefabs; 
    [Range(0, 100)] public float dropChance = 30f; 

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        // ถ้าโอกาสดรอปติด (เช่น สุ่มได้น้อยกว่า 30%) และมีของให้ดรอป
        if (dropItemPrefabs.Length > 0 && Random.Range(0, 100f) <= dropChance)
        {
            // ให้มันสุ่มเลือกไอเทม 1 ชิ้นจากลิสต์ที่เราใส่ไว้!
            int randomIndex = Random.Range(0, dropItemPrefabs.Length);
            Instantiate(dropItemPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject); 
    }
}