using UnityEngine;
using UnityEngine.UI;
// ฮานะเอา SceneManager ออกไปแล้วนะ! เพราะปุ่ม OK บนหน้าจอแพ้จะเป็นคนจัดการย้ายฉากให้เอง!

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Image[] heartImages; 

    public float invincibilityTime = 1.5f; 
    private float invincibilityCounter;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (invincibilityCounter <= 0)
            {
                TakeDamage(1);
                invincibilityCounter = invincibilityTime; 
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }

    void Die()
    {
        Debug.Log("โดนมอนสเตอร์งับตายแล้วตาบ๊อง!");
        
        // 👇 ฮานะแก้ตรงนี้แหละ! สั่งให้ GameManager โชว์ป้าย Game Over ขึ้นมา!
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowDefeatScreen();
        }

        // ซ่อนศพตัวละครทิ้งไปซะ จะได้ไม่โดนบอสรุมทึ้งตอนจอค้าง!
        gameObject.SetActive(false); 
    }
}