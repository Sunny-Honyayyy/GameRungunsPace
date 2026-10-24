using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    // ตัวแปรนี้จะไปโผล่ใน Inspector ให้เราพิมพ์เลขด่านใส่เข้าไป
    public int levelNumber; 

    void OnMouseDown()
    {
        // สั่งให้เกมจำเลขด่านไว้ในหัว โดยตั้งชื่อความจำนี้ว่า "SelectedLevel"
        PlayerPrefs.SetInt("SelectedLevel", levelNumber);
        
        // จำเสร็จปุ๊บ ก็โหลดข้ามไปซีน Game ทันที
        SceneManager.LoadScene("Game");
    }
}