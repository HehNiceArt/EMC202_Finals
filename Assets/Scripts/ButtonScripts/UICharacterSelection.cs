using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UICharacterSelection : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] Sprite[] selectionImage;

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Label characterContainer = root.Q<Label>("Characters");
    }
}
