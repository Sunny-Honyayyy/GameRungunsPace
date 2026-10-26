using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game Settings")]
    public int enemiesToKill = 50; 
    public TextMeshProUGUI enemyCountText; 

    [Header("Upgrade UI")]
    public TextMeshProUGUI upgradeText; 

    // 👇 เปลี่ยนมาใช้แบบรวมชุด (Panel เดียวจบ!)
    [Header("End Game Set")]
    public GameObject endGamePanel; // ลาก Panel รวมมาใส่ช่องนี้
    public TextMeshProUGUI endGameTitleText; // ลากตัวหนังสือหัวข้อมาใส่

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
        // ซ่อนหน้าจอจบเกมตอนเริ่มเกม
        if (endGamePanel != null) endGamePanel.SetActive(false);
        
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
            ShowVictoryScreen(); 
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
            upgradeText.text = "Upgrades\n" +
                               "Speed: +" + speedBuffCount + "\n" +
                               "Damage: +" + damageBuffCount + "\n" +
                               "Fire Rate: +" + fireRateBuffCount;
        }
    }

    // 👇 โชว์หน้าจอตอนชนะ!
    void ShowVictoryScreen()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
            if (endGameTitleText != null) 
            {
                endGameTitleText.text = "VICTORY!";
                endGameTitleText.color = Color.yellow; // เปลี่ยนอักษรเป็นสีทอง!
            }
        }
        Time.timeScale = 0f; 
    }

    // 👇 โชว์หน้าจอตอนแพ้!
    public void ShowDefeatScreen()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
            if (endGameTitleText != null) 
            {
                endGameTitleText.text = "GAME OVER!";
                endGameTitleText.color = Color.red; // เปลี่ยนอักษรเป็นสีแดง!
            }
        }
        Time.timeScale = 0f; 
    }

    // ฟังก์ชันสำหรับปุ่ม OK ตัวเดียวจบ!
    public void GoToUIScene()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("UI"); 
    }
}