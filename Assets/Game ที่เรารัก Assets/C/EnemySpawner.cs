using System.Collections.Generic; // ต้องมีบรรทัดนี้เพื่อใช้ระบบตะกร้า (List)
using UnityEngine;

// 👇 สร้างหมวดหมู่ย่อยสำหรับ "จัดเซ็ตมอนสเตอร์"
[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab; // ลากมอนสเตอร์มาใส่
    public int spawnCount;         // ระบุเลยว่าอยากให้ตัวนี้เกิดกี่ตัว!
}

[System.Serializable]
public class SpawnPhase
{
    public string phaseName = "Wave 1"; 
    public EnemyGroup[] enemyGroups;  // กล่องจัดเซ็ตมอนสเตอร์หลายๆ แบบรวมกัน
    public float spawnInterval = 2f;  
    public int spawnAmount = 3;       
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Phase Settings")]
    public SpawnPhase[] phases; 
    private int currentPhaseIndex = 0; 
    
    [Header("Spawn Distance")]
    public float minRadius = 15f; 
    public float maxRadius = 25f; 

    private Transform player;
    private float timer;

    // ตะกร้าลับ! เอาไว้เก็บมอนสเตอร์ทั้งหมดที่จะต้องเสกในเฟสนี้
    private List<GameObject> enemiesToSpawnThisPhase = new List<GameObject>();

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        PreparePhase(); // เตรียมมอนสเตอร์ลงตะกร้าสำหรับเวฟแรก!
    }

    // ฟังก์ชันนี้จะทำหน้าที่กวาดมอนสเตอร์ที่นายตั้งค่าไว้ ยัดลงตะกร้าให้พร้อมเสก
    void PreparePhase()
    {
        enemiesToSpawnThisPhase.Clear();
        if (currentPhaseIndex < phases.Length)
        {
            foreach (EnemyGroup group in phases[currentPhaseIndex].enemyGroups)
            {
                for (int i = 0; i < group.spawnCount; i++)
                {
                    // ก๊อปปี้มอนสเตอร์ลงตะกร้าตามจำนวนที่นายระบุเป๊ะๆ!
                    enemiesToSpawnThisPhase.Add(group.enemyPrefab);
                }
            }
        }
    }

    void Update()
    {
        // ถ้าเสกครบทุกเฟสแล้ว ก็หยุดทำงาน
        if (currentPhaseIndex >= phases.Length) return;

        timer += Time.deltaTime;
        SpawnPhase currentPhase = phases[currentPhaseIndex]; 

        if (timer >= currentPhase.spawnInterval)
        {
            Spawn(currentPhase);
            timer = 0f;
        }
    }

    void Spawn(SpawnPhase phase)
    {
        // ถ้าไม่มีคนเล่น หรือ ในตะกร้าไม่มีมอนสเตอร์เหลือแล้ว ก็ข้ามไป
        if (player == null || enemiesToSpawnThisPhase.Count == 0) return;

        // คำนวณว่าจะเสกทีละกี่ตัว (ถ้าในตะกร้าเหลือน้อยกว่าที่ตั้งไว้ ก็เสกแค่ที่เหลือ)
        int amountToSpawn = Mathf.Min(phase.spawnAmount, enemiesToSpawnThisPhase.Count);

        for (int i = 0; i < amountToSpawn; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            float distance = Random.Range(minRadius, maxRadius);
            Vector2 spawnPos = (Vector2)player.position + (direction * distance);

            // สุ่มล้วงมอนสเตอร์ออกมาจากตะกร้า 1 ตัว
            int randIndex = Random.Range(0, enemiesToSpawnThisPhase.Count);
            GameObject prefabToSpawn = enemiesToSpawnThisPhase[randIndex];
            
            // เสกมันออกมา!
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            
            // ล้วงออกมาแล้ว ก็เอาออกจากตะกร้าด้วย จะได้ไม่เสกเกิน!
            enemiesToSpawnThisPhase.RemoveAt(randIndex);
        }

        // ถ้าตะกร้าว่างเปล่า (เสกครบหมดแล้ว) ก็เปลี่ยนเวฟต่อไปเลย!
        if (enemiesToSpawnThisPhase.Count == 0)
        {
            currentPhaseIndex++;
            PreparePhase(); // เตรียมมอนสเตอร์ลงตะกร้าสำหรับเวฟใหม่!
        }
    }
}