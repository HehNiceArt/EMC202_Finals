using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;
public class UIStartButton : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button start = root.Q<Button>("StartButton");
        //Label buildVersion = root.Q<Label>("BuildVersion");
        //Label buildVersionDate = root.Q<Label>("BuildVersionDate");
        //string date = string.Format(DateTime.Now.ToString("d"));
        //buildVersion.text = Application.version;
        //buildVersionDate.text = string.Format("{0}", DateTime.Now.ToString("d"));
        
        start.clicked += () => StartCoroutine(UnLoad());
    }

    private void Awake()
    {
        
    }
    //public void Awake()
    //{
    //    build
    //}
    IEnumerator UnLoad()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
        //while (!true) { yield return null; }
        //AsyncOperation asyncload = SceneManager.LoadSceneAsync("UI_Home");
        //while (!asyncload.isDone) { yield return null; } 
    }
        
}
