using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle fullscreenToggle;

    private void Start()
    {
        if (ES3.KeyExists("audioVolume"))
        {
            mixer.SetFloat("volume", ES3.Load<float>("audioVolume"));
            volumeSlider.value = ES3.Load<float>("audioVolume");
        }

        if (ES3.KeyExists("isFullscreen"))
        {
            bool isFullscreen = ES3.Load<bool>("isFullscreen");
            Screen.fullScreen = isFullscreen;
            fullscreenToggle.isOn = isFullscreen;
        }
    }

    public void SetVolume (float volume)
    {
        mixer.SetFloat("volume", volume);
        ES3.Save<float>("audioVolume", volume);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        ES3.Save<bool>("isFullscreen", isFullscreen);
    }
}
