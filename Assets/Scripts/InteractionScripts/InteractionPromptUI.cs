using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private TextMeshProUGUI prompText;
    [SerializeField] private GameObject uiPanel;
    private void Start()
    {
        mainCam = Camera.main;
        uiPanel.SetActive(false);
    }
    private void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    public bool isDisplayed = false;
    public void SetUp(string promptText)
    {
        prompText.text = promptText;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }
    public void Close()
    {
        uiPanel.SetActive(false);
        isDisplayed= false;
    }
}
