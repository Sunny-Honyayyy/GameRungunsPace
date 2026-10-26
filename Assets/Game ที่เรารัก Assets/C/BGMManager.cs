using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance; 
    
    [Header("Music Playlist")]
    public AudioClip[] bgmTracks; 

    private AudioSource audioSource;
    [HideInInspector] public float sfxVolume = 1f; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        float savedBGM = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        audioSource.volume = savedBGM;

        // ตอนเริ่มเกมด่านแรก (หน้า UI) สั่งให้เล่นเพลงเลย
        PlayRandomTrack();
    }

    public void SetBGMVolume(float volume)
    {
        if (audioSource != null) audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); 
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); 
    }

    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        if (clip == null) return;
        
        GameObject tempAudio = new GameObject("TempSFX");
        tempAudio.transform.position = position;
        AudioSource aSource = tempAudio.AddComponent<AudioSource>();
        
        aSource.clip = clip;
        aSource.volume = sfxVolume; 
        aSource.Play();
        
        Destroy(tempAudio, clip.length); 
    }

    // 👇 ระบบใหม่! เอาไว้รับคำสั่งเปลี่ยนเซ็ตเพลงจากด่านต่างๆ
    public void ChangeBGM(AudioClip[] newTracks)
    {
        if (newTracks == null || newTracks.Length == 0) return;

        // ตรวจสอบก่อนว่าเพลงที่เล่นอยู่ เป็นหนึ่งในเพลงของด่านนี้ไหม? (ถ้าใช่ จะได้ไม่เริ่มเล่นใหม่ให้สะดุด)
        bool isAlreadyPlaying = false;
        if (audioSource.clip != null)
        {
            foreach (AudioClip clip in newTracks)
            {
                if (audioSource.clip == clip)
                {
                    isAlreadyPlaying = true;
                    break;
                }
            }
        }

        // ถ้าต้องเปลี่ยนเพลงจริงๆ ก็สับเปลี่ยนเพลย์ลิสต์เลย!
        if (!isAlreadyPlaying)
        {
            bgmTracks = newTracks;
            PlayRandomTrack();
        }
    }

    // สุ่มเล่นเพลงจากตะกร้าปัจจุบัน
    private void PlayRandomTrack()
    {
        if (bgmTracks.Length > 0)
        {
            int randomIndex = Random.Range(0, bgmTracks.Length);
            audioSource.clip = bgmTracks[randomIndex];
            audioSource.loop = true; 
            audioSource.Play(); 
        }
    }
}