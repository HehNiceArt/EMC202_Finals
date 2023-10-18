using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIStartButton : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button start = root.Q<Button>("StartButton");

        start.clicked += () => StartCoroutine(UnLoad());
    }
    IEnumerator UnLoad()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("UI_Home");
        while (!asyncload.isDone) { yield return null; } 
    }
        
}
