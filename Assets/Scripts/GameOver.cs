using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject gameOver;

    public Text fromFile;
    public Text score;
    public Text text;

    public InputField input;
    public Slider bar;

    public string nameFile;
    private string player;

    public void OnSave()
    {
        player = input.GetComponent<InputField>().text;
        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + nameFile, true);
        sw.WriteLine(player + " - " + Score.score);
        sw.Close();
        PlayerCtrl.attempts = 3;
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }
    void Update()
    {
        if (PlayerCtrl.attempts <= 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            score.GetComponent<Text>().text = "Score: " + Score.score;
            StreamReader sr = new StreamReader(Application.dataPath + "/" + nameFile);
            List<string> list = new List<string>();
            string str;
            while ((str = sr.ReadLine()) != null)
            {
                list.Add(str);
            }
            sr.Close();
            string str2 = Sort(list);
            fromFile.GetComponent<Text>().text = str2;
        }
    }

    private string Sort(List<string> list)
    {
        int rows = list.Count;
        string[,] strArr = new string[rows, 3];
        for (int i = 0; i < rows; i++)
        {
            string[] temp = list[i].Split(' ');
            for (int j = 0; j < 3; j++)
            {
                strArr[i, j] = temp[j];
            }
        }
        for (int j = 0; j < 3; j++)
        {
            bool swap = true;
            while (swap)
            {
                swap = false;
                for (int i = 1; i < rows - 1; i++)
                {
                    int a = Convert.ToInt32(strArr[i, 2]);
                    int b = Convert.ToInt32(strArr[i + 1, 2]);
                    if (a < b)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            string temp = strArr[i, k];
                            strArr[i, k] = strArr[i + 1, k];
                            strArr[i + 1, k] = temp;
                            swap = true;
                        }
                    }
                }
            }
        }
        if (rows > 11)
            rows = 11;
        string[] arr = new string[rows];
        string[] arr2 = new string[rows];
        for (int i = 0; i < rows; i++)
        {
            string[] temp = new string[3];
            for (int j = 0; j < 3; j++)
            {
                temp[j] = strArr[i, j];
            }
            if (i == 0)
            {
                arr[i] = "Player - score";
                arr2[i] = "Player - score";
            }
            else
            {
                arr[i] = i + ". " + String.Join(" ", temp);
                arr2[i] = String.Join(" ", temp);
            }
        }
        string str2 = String.Join("\n", arr);
        string temp2= String.Join("\n", arr2);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + nameFile, false);
        sw.WriteLine(temp2);
        sw.Close();
        return str2;
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level1");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                bar.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
