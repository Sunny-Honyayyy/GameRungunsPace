using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // สร้างกล่องมารับ GameObject ของทั้ง 3 ด่าน
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    void Start()
    {
        // 1. ปิดทุกด่านไปก่อนเพื่อความชัวร์
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        // 2. ถามเกมว่าเมื่อกี้หน้าเมนูจำเลขอะไรมา? (ถ้าลืมจำมา ให้ถือว่าเป็นด่าน 1)
        int selected = PlayerPrefs.GetInt("SelectedLevel", 1);

        // 3. เปิดด่านตามเลขที่ได้มา
        if (selected == 1) {
            level1.SetActive(true);
        } else if (selected == 2) {
            level2.SetActive(true);
        } else if (selected == 3) {
            level3.SetActive(true);
        }
    }
}