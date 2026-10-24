using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim; // สร้างตัวแปรไว้เรียกใช้ Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // ดึงคอมโพเนนต์ Animator มาใช้
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ส่งค่าการกดปุ่มไปบอก Animator
        if (movement != Vector2.zero) // ถ้ามีการกดปุ่มขยับตัว
        {
            anim.SetFloat("MoveX", movement.x);
            anim.SetFloat("MoveY", movement.y);
        }
        
        // ส่งค่าความเร็วไปให้ Animator รู้ว่าตอนนี้เดินอยู่หรือหยุดนิ่ง
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}