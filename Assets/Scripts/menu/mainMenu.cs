using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public Slider bar;
    public GameObject loadingScreen;
    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
    public void Level1()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync("level1"));
    }
    public void Level2()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync("level2"));
    }
    IEnumerator LoadAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            if (asyncLoad.progress >= .9f)
            {
                bar.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
