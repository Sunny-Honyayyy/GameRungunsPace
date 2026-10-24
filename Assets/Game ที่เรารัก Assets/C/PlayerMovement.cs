using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;

    [Header("Shooting System")]
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    private Vector2 facingDirection = new Vector2(0, -1); 

    // เพิ่ม 2 บรรทัดนี้เข้ามาเพื่อทำระบบยิงอัตโนมัติ!
    public float fireRate = 0.5f; // ความเร็วในการยิง (หน่วงเวลา 0.5 วินาทีต่อนัด)
    private float nextFireTime = 0f; // ตัวเก็บเวลาว่านัดต่อไปยิงได้ตอนไหน

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        // ระบบยิงอัตโนมัติ! (เปลี่ยนจากเช็คปุ่มกด มาเช็คเวลาแทน)
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // ตั้งเวลาสำหรับการยิงนัดถัดไป!
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Setup(facingDirection);
    }
}