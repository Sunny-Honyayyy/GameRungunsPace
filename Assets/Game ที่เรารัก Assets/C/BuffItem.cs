using UnityEngine;

public class BuffItem : MonoBehaviour
{
    // สร้างหมวดหมู่ให้เลือกใน Unity
    public enum BuffType { Speed, FireRate, Damage }
    public BuffType buffType; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ถ้าคนที่เดินมาชนคือ Player
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // เช็คว่าไอเทมชิ้นนี้เป็นบัฟอะไร แล้วแจกพลังตามนั้น!
                if (buffType == BuffType.Speed) 
                    player.UpgradeMoveSpeed(0.25f); // เดินไวขึ้นทีละ 0.25
                else if (buffType == BuffType.FireRate) 
                    player.UpgradeAllWeaponsFireRate(0.05f); // ยิงรัวขึ้น 0.05 วิ
                else if (buffType == BuffType.Damage) 
                    player.UpgradeAllWeaponsDamage(1); // ยิงแรงขึ้น 1 หน่วย

                Destroy(gameObject); // เก็บเสร็จก็ทำลายไอเทมทิ้ง!
            }
        }
    }
}