using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1; 
    private Rigidbody2D rb;

    public void Setup(Vector2 moveDirection)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = moveDirection.normalized * speed;
        
        // --- เพิ่มโค้ด 2 บรรทัดนี้ เพื่อบิดหัวกระสุนให้หันถูกทิศ! ---
        // คำนวณองศาจากทิศทาง x, y
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        // สั่งให้ตัวกระสุนหมุนตามองศาที่คำนวณได้
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // ---------------------------------------------------

        Destroy(gameObject, 3f); 
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        
        if (enemy != null) 
        {
            enemy.TakeDamage(damage); 
            Destroy(gameObject); 
        }
    }
}