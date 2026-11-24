using UnityEngine;

public class light : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
