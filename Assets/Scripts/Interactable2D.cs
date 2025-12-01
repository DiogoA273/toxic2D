using UnityEngine;

public class Interactable2D : MonoBehaviour
{
    public string interactionName;

    public virtual void Interact()
    {
        Debug.Log("Interagiu com: " + interactionName);
    }
}
