using UnityEngine;
using UnityEngine.UI; // ต้องมีเพื่อจัดการ Slider

public class SettingsMenu : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        // ตอนเปิดหน้า Setting ให้ขยับหลอด Slider ไปจุดที่เคยเซฟไว้
        if (bgmSlider != null) bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        if (sfxSlider != null) sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    public void OnBGMVolumeChanged(float volume)
    {
        if (BGMManager.instance != null) BGMManager.instance.SetBGMVolume(volume);
    }

    public void OnSFXVolumeChanged(float volume)
    {
        if (BGMManager.instance != null) BGMManager.instance.SetSFXVolume(volume);
    }
}