using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth;

    [Header("Drop System")]
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
        // 👇 ฮานะเพิ่มตรงนี้! เพื่อรายงาน GameManager ว่าศัตรูตายแล้วนะ!
        if (GameManager.instance != null)
        {
            GameManager.instance.EnemyDied();
        }

        // ระบบดรอปไอเทมเด้งหลุดกำแพง (เหมือนเดิมเป๊ะๆ)
        if (dropItemPrefabs.Length > 0 && Random.Range(0, 100f) <= dropChance)
        {
            int randomIndex = Random.Range(0, dropItemPrefabs.Length);
            Vector2 dropPosition = transform.position;

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                Vector2 direction = (playerObj.transform.position - transform.position).normalized;
                dropPosition += direction * 1.5f; 
            }

            Instantiate(dropItemPrefabs[randomIndex], dropPosition, Quaternion.identity);
        }
        
        Destroy(gameObject); 
    }
}