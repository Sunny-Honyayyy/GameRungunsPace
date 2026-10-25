using System.Collections; // ต้องมีบรรทัดนี้นะถึงจะใช้ระบบนับเวลาได้!
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private Vector2 facingDirection = new Vector2(0, -1); 

    [Header("Weapon System")]
    public Weapon[] weapons; 
    public int defaultWeaponIndex = 0; // ปืนหลัก (ปืนตอนเริ่มเกม)
    
    private Weapon currentWeapon; 
    private Coroutine weaponTimer; // ตัวแปรเก็บเวลาปืนชั่วคราว

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        EquipWeapon(defaultWeaponIndex); 
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero) 
        {
            anim.SetFloat("MoveX", movement.x);
            anim.SetFloat("MoveY", movement.y);
            facingDirection = movement;
        }
        
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (currentWeapon != null)
        {
            currentWeapon.TryShoot(facingDirection);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    public void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Length) return;

        foreach (Weapon w in weapons)
        {
            w.gameObject.SetActive(false);
        }

        currentWeapon = weapons[weaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }

    // ฟังก์ชันนี้โดนเรียกตอนเก็บไอเทมปืนได้!
    public void PickupTempWeapon(int tempWeaponIndex, float duration)
    {
        EquipWeapon(tempWeaponIndex); // เปลี่ยนไปถือปืนที่เก็บได้
        
        if (weaponTimer != null) StopCoroutine(weaponTimer); // รีเซ็ตเวลาถ้าเก็บซ้ำ
        
        weaponTimer = StartCoroutine(RevertWeaponAfterTime(duration)); // เริ่มนับเวลาถอยหลัง!
    }

    // ระบบนับเวลาถอยหลัง
    IEnumerator RevertWeaponAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // รอจนกว่าเวลาจะหมด
        EquipWeapon(defaultWeaponIndex); // สลับกลับมาปืนหลัก!
    }
}