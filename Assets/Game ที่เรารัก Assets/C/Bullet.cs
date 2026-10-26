using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1; 
    private Rigidbody2D rb;

    [Header("Explosion & VFX")]
    public GameObject hitEffectPrefab; 
    public bool isAOE = false;         
    public float aoeRadius = 2f;       

    // 👇 ฮานะเพิ่มช่องใส่เสียงกระทบเป้าหมายไว้ตรงนี้นะ!
    [Header("Audio Settings")]
    public AudioClip hitSound; 

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

            // 👇 2. สั่งให้เล่นเสียง "ฉึก!" หรือเสียงระเบิดตรงจุดที่ชนทันที!
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            // 3. เช็คว่าเป็นกระสุนแบบระเบิดหมู่ (AOE) หรือเปล่า?
            if (isAOE)
            {
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
                enemy.TakeDamage(damage); 
            }

            // ทำลายกระสุนทิ้งซะ
            Destroy(gameObject); 
        }
    }

    void OnDrawGizmosSelected()
    {
        if (isAOE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aoeRadius);
        }
    }
}