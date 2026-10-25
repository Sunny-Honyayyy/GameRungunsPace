using UnityEngine;

public class EnemyFollowFlip : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // ให้มันสแกนหาตัวละครที่ติด Tag ว่า "Player" อัตโนมัติ
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        // ดึงคอมโพเนนต์ภาพมาเตรียมพลิกซ้ายขวา
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // เดินเข้าไปหา Player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // เช็คตำแหน่งแกน X ว่า Player อยู่ซ้ายหรือขวา
            if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true; // ถ้า Player อยู่ซ้าย ให้พลิกรูปหันซ้าย
            }
            else
            {
                spriteRenderer.flipX = false; // ถ้าอยู่ขวา ก็หันขวาปกติ
            }
        }
    }
}