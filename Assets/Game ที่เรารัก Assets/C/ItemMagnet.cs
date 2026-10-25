using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float magnetSpeed = 4f;  // ความเร็วตอนไอเทมลอยเข้าหาตัว
    public float magnetRange = 5f;  // ระยะรัศมีแม่เหล็ก (ต้องเดินเข้าไปใกล้แค่ไหนมันถึงจะดูด)
    private Transform player;

    void Start()
    {
        // ให้ไอเทมมองหาตัว Player อัตโนมัติ
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // เช็คว่า Player เดินเข้ามาในระยะแม่เหล็กหรือยัง?
            float distance = Vector2.Distance(transform.position, player.position);
            
            if (distance <= magnetRange)
            {
                // สั่งให้ไอเทมลอยทะลุกำแพงพุ่งเข้าหา Player เลย!
                transform.position = Vector2.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);
            }
        }
    }
}