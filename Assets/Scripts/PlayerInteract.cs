using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable currentInteractable;
    private int interactableLayer;

    private void Start()
    {
        interactableLayer = LayerMask.NameToLayer("Interactable");
    }
    private void OnEnable()
    {
        InputManager.OnInteractPressed += TryToInteract;
    }

    private void OnDisable()
    {
        InputManager.OnInteractPressed -= TryToInteract;
    }
    private void TryToInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == interactableLayer)
        {
            print("Interactable detectado");
            currentInteractable = collision.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == interactableLayer)
        {
            currentInteractable = null;
        }
    }
}
