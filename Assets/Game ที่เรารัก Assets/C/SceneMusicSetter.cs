using UnityEngine;

public class SceneMusicSetter : MonoBehaviour
{
    [Header("เพลงประจำด่านนี้ (ใส่กี่เพลงก็ได้เดี๋ยวสุ่มเอง)")]
    public AudioClip[] sceneTracks;

    void Start()
    {
        // พอเข้าซีนปุ๊บ รีบวิ่งไปบอก BGMManager ให้เปลี่ยนแผ่นเพลงทันที!
        if (BGMManager.instance != null)
        {
            BGMManager.instance.ChangeBGM(sceneTracks);
        }
        else
        {
            Debug.LogWarning("ฮานะยังไม่เจอ BGMManager เลย! สงสัยเริ่มเทสข้ามด่านแน่ๆ");
        }
    }
}