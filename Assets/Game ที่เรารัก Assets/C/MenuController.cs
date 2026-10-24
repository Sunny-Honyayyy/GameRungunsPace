using UnityEngine;

public class MenuController : MonoBehaviour
{
    // สร้างกล่องรับข้อมูล GameObject ของหน้าเมนูต่างๆ
    public GameObject startMenu;
    public GameObject disMenu;

    // ฟังก์ชันนี้จะทำงานเมื่อคลิกเมาส์ที่ตัวที่มี Collider
    void OnMouseDown()
    {
        // ปิดหน้า Start แล้วเปิดหน้า Play แทน
        startMenu.SetActive(true);
        disMenu.SetActive(false);
    }
}