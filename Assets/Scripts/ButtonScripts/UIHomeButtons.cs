using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIHomeButtons : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button goBack = root.Q<Button>("GoBack");
       // Button destroySelf = root.Q<Button>("Destroy");

        goBack.clicked += () => StartCoroutine(UnLoad());
       // goBack.clicked += () => UnLoad();
       // yield return new WaitForSeconds(10f);
      //  destroySelf.clicked += () => SceneManager.UnloadSceneAsync(1);
    }

    private void Update()
    {
      //  StartCoroutine(UnLoad());
    }
    IEnumerator UnLoad()
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Level_Exploration");
        while (!asyncload.isDone) { yield return null; }
    }

}
