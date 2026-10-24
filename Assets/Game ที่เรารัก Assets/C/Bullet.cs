using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    public void Setup(Vector2 moveDirection)
    {
        rb = GetComponent<Rigidbody2D>();
        // สั่งให้กระสุนพุ่งปรี๊ดดดด! (ใช้ linearVelocity ของ Unity 6)
        rb.linearVelocity = moveDirection.normalized * speed;
        
        // ทำลายตัวเองใน 3 วินาทีถ้าไม่โดนอะไรเลย จะได้ไม่รกหน่วยความจำเครื่อง!
        Destroy(gameObject, 3f); 
    }
}