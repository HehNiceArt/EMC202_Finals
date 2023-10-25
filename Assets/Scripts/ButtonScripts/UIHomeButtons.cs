using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIHomeButtons : MonoBehaviour
{
    private int nextSceneLoad;
    UILoading load;
    VisualElement root;
    VisualElement characterContainer;
    [SerializeField] Sprite spriteMaiko;
    [SerializeField] Sprite spriteYumi;
    [SerializeField] Sprite spriteNoName;
    private void OnEnable()
    {
         root = GetComponent<UIDocument>().rootVisualElement;

        #region Buttons
        #region
        characterContainer = root.Q<VisualElement>("Characters");
        Button changeCharacterYumi = root.Q<Button>("KohanaYumiButton");
        Button changeCharacterMaiko = root.Q<Button>("NanamiMaikoButton");
        Button changeCharacterNoName = root.Q<Button>("NoNameButton");
        #endregion
        #region Exploration
        Button explore = root.Q<Button>("Explore");
        Button formation = root.Q<Button>("Formation");
        #endregion
        #region BottmButtons
        Button recruits = root.Q<Button>("Recruits");
        Button armory = root.Q<Button>("Armory");
        Button dorm = root.Q<Button>("Dorm");
        Button gacha = root.Q<Button>("Gacha");
        Button mail = root.Q<Button>("Mail");
        #endregion
        #region Settings
        Button settings = root.Q<Button>("Settings");
        Button notice = root.Q<Button>("Notice");
        Button friends = root.Q<Button>("Friends");
        Button ranks = root.Q<Button>("Ranks");
        #endregion
        #endregion

        #region Button Triggers

        explore.clicked += () => StartCoroutine(UnLoad());
        changeCharacterYumi.clicked += () => ChangeCharacterSpriteYumi();
        changeCharacterMaiko.clicked += () => ChangeCharacterSpriteMaiko();
        changeCharacterNoName.clicked += () => ChangeCharacterSpriteNoName();
        
        #endregion

    }
    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    IEnumerator UnLoad()
    {
      //  load.UnLoad(nextSceneLoad);
       // Debug.Log(load);
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(nextSceneLoad);
        while (!asyncload.isDone) { yield return null; }

    }

    #region Character Sprites
    private void ChangeCharacterSpriteMaiko()
    {
        characterContainer.style.backgroundImage = new StyleBackground(spriteMaiko);
    }
    private void ChangeCharacterSpriteYumi()
    {
        characterContainer.style.backgroundImage = new StyleBackground(spriteYumi);
    }
    private void ChangeCharacterSpriteNoName()
    {
        characterContainer.style.backgroundImage = new StyleBackground(spriteNoName);
    }
    #endregion
    #region Switch Case Character Sprite
    //private void SetCharacterSprite(int index)
    //{
    //    switch (index)
    //    {
    //        case 0:
    //            characterContainer.style.backgroundImage = new StyleBackground(spriteYumi);
    //            break;
    //        case 1:
    //            characterContainer.style.backgroundImage = new StyleBackground(spriteMaiko);
    //            break;
    //        case 2:
    //            characterContainer.style.backgroundImage = new StyleBackground(spriteNoName);
    //            break;
    //    }
    //}
    #endregion
}
