using UnityEngine;

public class ExitController : MonoBehaviour
{
    void OnMouseDown()
    {
        // โชว์ข้อความใน Console เพื่อให้รู้ว่าปุ่มทำงานแล้ว (ตอนเทสใน Unity)
        Debug.Log(""); 
        
        // คำสั่งปิดเกมของจริง
        Application.Quit();
    }
}