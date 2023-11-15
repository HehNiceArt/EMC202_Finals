using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private GameObject panel;
    [SerializeField] private CanvasGroup panelGroup;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject player;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
       
        if (panel.activeInHierarchy == false)
        {
            StartCoroutine("ShowUI");
            StartCoroutine("HideUI");
        }
        else if(panel.activeInHierarchy == true)
        {
            StartCoroutine("HideUI");
        }
       
        return true ;
    }
    void Start()
    {
        panel.SetActive(false);
    }

    void Show()
    {
        panel.SetActive(true);
    }
    void Hide()
    {
        panel.SetActive(false);
    }
    private void LateUpdate()
    {
        panel.transform.LookAt(mainCam.transform.position);
    }
    IEnumerator ShowUI()
    {
        panelGroup.DOFade(0, 0f);
        yield return new WaitForSeconds(0f);
        Show();
        panelGroup.DOFade(1, 1f);
    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(10f);
        panelGroup.DOFade(0, 3f);
        yield return new WaitForSeconds(4f);
        Hide();
    }
}
