using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth;

    [Header("Drop System")]
    public GameObject[] dropItemPrefabs; 
    [Range(0, 100)] public float dropChance = 30f; 

    // 👇 เพิ่มช่องใส่เสียงตอนตายตรงนี้!
    [Header("Audio Settings")]
    public AudioClip deathSound; 

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
        if (GameManager.instance != null)
        {
            GameManager.instance.EnemyDied();
        }

        // 👇 สั่งให้เล่นเสียงตรงจุดที่ตายก่อนที่ตัวมันจะหายไป!
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

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