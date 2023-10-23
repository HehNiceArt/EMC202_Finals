using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterChangeButton : MonoBehaviour
{
    [SerializeField] private Image original;
    [SerializeField] private Sprite newSprite;

    private void Start()
    {
        
    }
    public void NewImage()
    {
        original.sprite = newSprite;
    }
}
