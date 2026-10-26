using UnityEngine;
using UnityEngine.SceneManagement;

public class EscManager : MonoBehaviour
{
    void Update()
    {
        // ถ้านายกดปุ่ม ESC บนคีย์บอร์ด
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            // ถ้าเล่นอยู่หน้า Game ให้เด้งกลับไปหน้า Ui
            if (currentScene == "Game")
            {
                Time.timeScale = 1f; // ปลดล็อคเวลาเผื่อเกมหยุดอยู่
                SceneManager.LoadScene("Ui"); // ฮานะเห็นในรูปก่อนหน้าว่านายตั้งชื่อฉากแรกว่า Ui นะ!
            }
            // แต่ถ้าอยู่หน้า Ui ให้ปิดเกมไปเลย!
            else
            {
                Debug.Log("หนีไปนอนแล้วเหรอตาบ๊อง! ปิดเกมให้ก็ได้!");
                Application.Quit(); // คำสั่งปิดเกม!
            }
        }
    }
}