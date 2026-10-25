using System.Collections; 
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
    public int defaultWeaponIndex = 0; 
    private Weapon currentWeapon; 
    private Coroutine weaponTimer; 

    // 👇 เพิ่มระบบควบคุม FirePoint ตรงนี้!
    [Header("FirePoint Control")]
    public Transform mainFirePoint; // ลาก FirePoint มาใส่ช่องนี้
    public float firePointDistance = 1f; // ระยะห่างจากตัวละคร (ปรับได้นะ!)

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
            
            // ใช้ .normalized เพื่อให้ทิศทางเฉียงไม่ยาวเกินไป
            facingDirection = movement.normalized; 
            
            // 👇 สั่งให้ FirePoint ขยับไปตามทิศที่หันหน้า!
            if (mainFirePoint != null)
            {
                mainFirePoint.localPosition = facingDirection * firePointDistance;
            }
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

    public void PickupTempWeapon(int tempWeaponIndex, float duration)
    {
        EquipWeapon(tempWeaponIndex); 
        if (weaponTimer != null) StopCoroutine(weaponTimer); 
        weaponTimer = StartCoroutine(RevertWeaponAfterTime(duration)); 
    }

    IEnumerator RevertWeaponAfterTime(float time)
    {
        yield return new WaitForSeconds(time); 
        EquipWeapon(defaultWeaponIndex); 
    }

    public void UpgradeMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public void UpgradeAllWeaponsDamage(int amount)
    {
        foreach (Weapon w in weapons)
        {
            w.bonusDamage += amount;
        }
    }

    public void UpgradeAllWeaponsFireRate(float amount)
    {
        foreach (Weapon w in weapons)
        {
            w.fireRate -= amount;
            if (w.fireRate < 0.1f) w.fireRate = 0.1f; 
        }
    }
}