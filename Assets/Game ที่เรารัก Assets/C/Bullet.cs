using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1; 
    private Rigidbody2D rb;

    [Header("Explosion & VFX")]
    public GameObject hitEffectPrefab; // ช่องใส่เอฟเฟกต์ตอนกระสุนกระทบเป้า!
    public bool isAOE = false;         // ติ๊กถูกถ้าอยากให้กระสุนนี้ระเบิดหมู่!
    public float aoeRadius = 2f;       // รัศมีการระเบิด (กว้างแค่ไหน)

    public void Setup(Vector2 moveDirection)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = moveDirection.normalized * speed;
        
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, 3f); 
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        
        if (enemy != null) 
        {
            // 1. สร้างเอฟเฟกต์ระเบิดตู้มต้ามตรงจุดที่ชน!
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }

            // 2. เช็คว่าเป็นกระสุนแบบระเบิดหมู่ (AOE) หรือเปล่า?
            if (isAOE)
            {
                // ค้นหาศัตรู "ทั้งหมด" ที่อยู่ในรัศมีวงกลม แล้วแจกดาเมจเรียงตัว!
                Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
                foreach (Collider2D col in hitObjects)
                {
                    EnemyHealth nearbyEnemy = col.GetComponent<EnemyHealth>();
                    if (nearbyEnemy != null)
                    {
                        nearbyEnemy.TakeDamage(damage);
                    }
                }
            }
            else
            {
                // ถ้าไม่ใช่ AOE ก็ลดเลือดแค่ตัวที่โดนยิงตัวเดียว
                enemy.TakeDamage(damage); 
            }

            // ทำลายกระสุนทิ้งซะ
            Destroy(gameObject); 
        }
    }

    // วาดเส้นวงกลมสีแดงให้ดูใน Unity จะได้กะระยะระเบิดถูก!
    void OnDrawGizmosSelected()
    {
        if (isAOE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aoeRadius);
        }
    }
}