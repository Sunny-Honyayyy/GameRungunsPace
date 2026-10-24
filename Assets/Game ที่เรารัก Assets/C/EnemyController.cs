using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;
    private Animator anim;
    private Vector2 lastPos; // เอาไว้เทียบตำแหน่งเพื่อหาทิศทางเดิน

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        anim = GetComponent<Animator>();
        lastPos = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            // ตำแหน่งปัจจุบันก่อนเดิน
            Vector2 currentPos = transform.position;

            // ขยับตัวศัตรูเข้าหาผู้เล่น
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // คำนวณหาทิศทางที่กำลังเดิน (MoveX และ MoveY)
            Vector2 direction = ((Vector2)transform.position - currentPos).normalized;

            // ถ้าศัตรูกำลังเคลื่อนที่ ให้ส่งค่าทิศทางเข้าไปใน Animator Blend Tree
            if (anim != null)
            {
                if (direction.magnitude > 0.01f)
                {
                    anim.SetFloat("MoveX", direction.x);
                    anim.SetFloat("MoveY", direction.y);
                }
                
                // เผื่อโปรเจกต์ไหนมีใช้ Speed ควบคุมด้วย
                anim.SetFloat("Speed", speed);
            }
        }
    }
}