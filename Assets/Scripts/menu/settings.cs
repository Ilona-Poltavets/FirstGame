using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
public class settings : MonoBehaviour
{
    Resolution[] rsl;
    List<string> resolutions;

    public AudioMixer st;
    public AudioMixer master;
    public AudioMixer effects;

    public Dropdown dd;

    private bool isFullScreen = true;
    public static bool isSoundTrack = true;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void SoundTrackCtrl()
    {
        isSoundTrack = !isSoundTrack;
        if (!isSoundTrack)
            st.SetFloat("stVolume", -80f);
        else
            st.SetFloat("stVolume", 0f);
    }
    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        dd.ClearOptions();
        dd.AddOptions(resolutions);
    }
    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }
    public void MasterAudioVolume(float sliderVolume)
    {
        master.SetFloat("masterVolume", sliderVolume);
    }
    public void STAudioVolume(float sliderVolume)
    {
        st.SetFloat("stVolume", sliderVolume);
    }
    public void EffectsAudioVolume(float sliderVolume)
    {
        effects.SetFloat("effectsVolume", sliderVolume);
    }
}