using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime = 0.5f; // เวลาที่ปล่อยให้เอฟเฟกต์โชว์ (0.5 วินาที)

    void Start()
    {
        // สั่งทำลายตัวเอง (gameObject) เมื่อเวลาผ่านไปตามที่ตั้งไว้
        Destroy(gameObject, destroyTime);
    }
}