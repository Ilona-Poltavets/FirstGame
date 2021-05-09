using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class menuRecords : MonoBehaviour
{
    public string nameFile;
    public Text fromFile;

    public void read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/" + nameFile);
        List<string> list = new List<string>();
        string str;
        while ((str = sr.ReadLine()) != null)
        {
            list.Add(str);
        }
        string str2 = String.Join("\n", list);
        fromFile.GetComponent<Text>().text = str2;
        Debug.Log(str2);
        sr.Close();
    }
}
