using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class in_gameMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject loadingScreen;

    [SerializeField] private AudioSource pauseStart;
    [SerializeField] private AudioSource pauseEnd;

    public AudioMixer st;
    public AudioMixer master;
    public AudioMixer effects;

    public Slider bar;

    private bool isFullScreen = true;
    public static bool GameIsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void SoundTrackCtrl()
    {
        settings.isSoundTrack = !settings.isSoundTrack;
        if (!settings.isSoundTrack)
            st.SetFloat("stVolume", -80f);
        else
            st.SetFloat("stVolume", 0f);
    }
    public void goMainMenu()
    {
        pauseMenuUI.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }
    public void Resume()
    {
        pauseEnd.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseStart.Play();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            yield return null;
        }
    }
}