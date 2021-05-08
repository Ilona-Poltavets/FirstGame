using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject loadingScreen;

    public Slider bar;
    public Text text;

    public string scene;
    public void Load()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                bar.gameObject.SetActive(false);
                text.gameObject.SetActive(true);
                if (Input.anyKeyDown)
                    asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}