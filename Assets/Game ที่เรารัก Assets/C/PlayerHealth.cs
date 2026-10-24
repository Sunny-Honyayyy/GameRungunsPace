using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 1. เพิ่มบรรทัดนี้เพื่อให้เรียกใช้ SceneManager ได้

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Image[] heartImages; 

    // ตัวแปรสำหรับหน่วงเวลาโดนดาเมจซ้ำ
    public float invincibilityTime = 1.5f; // เวลาอมตะหรือเวลารอให้โดนซ้ำได้ (วินาที)
    private float invincibilityCounter;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    void Update()
    {
        // นับเวลาถอยหลังลดคูลดาวน์ดาเมจ
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
    }

    // ใช้ OnTriggerStay2D ตามที่เราเซ็ตค่า Trigger ไว้
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ถ้าหมดเวลาคูลดาวน์แล้ว ถึงจะลดเลือดได้อีก
            if (invincibilityCounter <= 0)
            {
                TakeDamage(1);
                invincibilityCounter = invincibilityTime; // รีเซ็ตเวลาคูลดาวน์ใหม่
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
        Debug.Log("Player Died!");
        
        // 2. สั่งให้เด้งกลับไปหน้า UI เมนูหลัก (เปลี่ยนคำว่า "MainMenu" เป็นชื่อ Scene หน้าเมนูของเธอได้เลยนะ)
        SceneManager.LoadScene("๊Ui"); 
    }
}