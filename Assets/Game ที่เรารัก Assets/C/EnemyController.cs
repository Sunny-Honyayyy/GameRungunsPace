using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f; // ความเร็วในการเดินเข้าหาผู้เล่น
    private Transform player;

    void Start()
    {
        // ค้นหาตำแหน่งของผู้เล่นจาก Tag "Player" อัตโนมัติ
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // ขยับตัวศัตรูให้เคลื่อนที่เข้าหาตำแหน่งของผู้เล่นในทุกๆ เฟรม
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}