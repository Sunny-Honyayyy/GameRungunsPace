using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; // 👇 ต้องมีบรรทัดนี้! ถึงจะสั่งเปลี่ยนฉากได้!

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game Settings")]
    public int enemiesToKill = 50; 
    public TextMeshProUGUI enemyCountText; 

    [Header("Upgrade UI")]
    public TextMeshProUGUI upgradeText; 

    // 👇 ช่องสำหรับใส่หน้าจอตอนชนะ!
    [Header("Victory Screen")]
    public GameObject victoryPanel; 

    [HideInInspector] 
    public int enemiesKilled = 0; 

    private int speedBuffCount = 0;
    private int damageBuffCount = 0;
    private int fireRateBuffCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // สั่งซ่อนหน้าจอชนะตอนเริ่มเกมไว้ก่อน
        if (victoryPanel != null) victoryPanel.SetActive(false);
        
        // คืนค่าเวลาให้เดินปกติ (เผื่อเล่นตาใหม่แล้วเวลาค้าง)
        Time.timeScale = 1f;

        UpdateUI();
        UpdateUpgradeUI(); 
    }

    public void EnemyDied()
    {
        enemiesToKill--; 
        enemiesKilled++; 

        if (enemiesToKill <= 0) 
        {
            enemiesToKill = 0;
            ShowVictoryScreen(); // 👇 เรียกฟังก์ชันโชว์หน้าจอชนะ!
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies Left: " + enemiesToKill;
        }
    }

    public void AddUpgrade(string buffType)
    {
        if (buffType == "Speed") speedBuffCount++;
        else if (buffType == "Damage") damageBuffCount++;
        else if (buffType == "FireRate") fireRateBuffCount++;

        UpdateUpgradeUI(); 
    }

    void UpdateUpgradeUI()
    {
        if (upgradeText != null)
        {
            upgradeText.text = "Upgrades:\n" +
                               "Speed: +" + speedBuffCount + "\n" +
                               "Damage: +" + damageBuffCount + "\n" +
                               "Fire Rate: +" + fireRateBuffCount;
        }
    }

    // 👇 ==========================================
    // 👇 ระบบฉากจบของนาย อยู่ตรงนี้แหละ!
    // 👇 ==========================================

    void ShowVictoryScreen()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true); // เปิดหน้าต่างโชว์คำว่าชนะ!
            Time.timeScale = 0f; // หยุดเวลาในเกม! ทุกอย่างจะหยุดนิ่ง
        }
    }

    // ฟังก์ชันนี้เอาไว้ให้ "ปุ่ม" ในหน้าจอชนะกดเรียกใช้!
    public void GoToUIScene()
    {
        Time.timeScale = 1f; // ต้องคืนค่าเวลาก่อนย้ายฉาก ไม่งั้นฉากหน้าจะค้าง!
        SceneManager.LoadScene("UI"); // โหลดฉากชื่อ "UI" (ชื่อฉากต้องตรงเป๊ะๆ นะ!)
    }
}