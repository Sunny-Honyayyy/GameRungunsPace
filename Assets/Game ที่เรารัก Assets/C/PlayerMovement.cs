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

    [Header("FirePoint Control")]
    public Transform mainFirePoint; 
    public float firePointDistance = 1f; 

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
            facingDirection = movement.normalized; 
            
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
        
        // 👇 ตะโกนบอก GameManager ว่าเพิ่งเก็บรองเท้ามา!
        if (GameManager.instance != null) GameManager.instance.AddUpgrade("Speed"); 
    }

    public void UpgradeAllWeaponsDamage(int amount)
    {
        foreach (Weapon w in weapons)
        {
            w.bonusDamage += amount;
        }
        
        // 👇 ตะโกนบอก GameManager ว่าเพิ่งเก็บดาบมา!
        if (GameManager.instance != null) GameManager.instance.AddUpgrade("Damage");
    }

    public void UpgradeAllWeaponsFireRate(float amount)
    {
        foreach (Weapon w in weapons)
        {
            w.fireRate *= 0.9f; 
            if (w.fireRate < 0.005f) 
            {
                w.fireRate = 0.005f; 
            }
        }
        
        // 👇 ตะโกนบอก GameManager ว่าเพิ่งเก็บสายฟ้ามา!
        if (GameManager.instance != null) GameManager.instance.AddUpgrade("FireRate");
    }
}