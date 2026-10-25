using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponIndexToGive = 1; // ไอเทมนี้จะให้ปืนหมายเลขอะไร (1 = ปืนกระบอกที่สอง)
    public float duration = 10f;      // ให้ใช้งานได้กี่วินาที

    void OnTriggerEnter2D(Collider2D other)
    {
        // ถ้าคนที่มาชนคือ Player
        if (other.CompareTag("Player")) 
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // สั่งให้ Player เปลี่ยนปืนและเริ่มจับเวลา!
                player.PickupTempWeapon(weaponIndexToGive, duration);
                
                Destroy(gameObject); // ไอเทมถูกเก็บแล้ว สั่งลบตัวเองทิ้ง
            }
        }
    }
}