using UnityEngine;

public class PointAndClick2D : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                var interactable = hit.collider.GetComponent<Interactable2D>();
                if (interactable != null)
                    interactable.Interact();
            }
        }
    }
}
