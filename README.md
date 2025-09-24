using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;            // Velocidade de movimento
    public Animator animator;               // Referência ao Animator (opcional)

    private Rigidbody2D rb;                 // Referência ao Rigidbody2D
    private Vector2 moveInput;              // Armazena a direção de movimento

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura a entrada do jogador (setas, WASD, etc.)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor de entrada para evitar velocidade maior na diagonal
        moveInput = new Vector2(moveX, moveY).normalized;

        // Atualiza os parâmetros do Animator (se houver)
        if (animator != null)
        {
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude); // .sqrMagnitude é mais performático que .magnitude
        }
    }

    void FixedUpdate()
    {
        // Move o personagem
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
