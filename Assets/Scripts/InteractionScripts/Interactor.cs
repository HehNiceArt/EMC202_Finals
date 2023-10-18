using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    private readonly Collider[] colliders = new Collider[3];

    [SerializeField] private int numFound;
    private IInteractable interactable;
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactionLayerMask);

        if (numFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();
            if(interactable != null)
            {
                interactionPromptUI.isDisplayed = true;
                if (interactionPromptUI.isDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);
                if (Keyboard.current.eKey.wasPressedThisFrame) interactable.Interact(this);
            }

        }
        else
        {
           if(interactable !=null) interactable = null;
           if(interactionPromptUI.isDisplayed) interactionPromptUI.Close();
        }
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

}
