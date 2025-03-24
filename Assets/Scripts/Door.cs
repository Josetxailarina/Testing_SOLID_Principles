using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        bool isOpen = animator.GetBool("Open");
        animator.SetBool("Open", !isOpen);
    }
}
