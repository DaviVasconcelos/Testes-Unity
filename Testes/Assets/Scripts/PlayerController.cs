using UnityEngine;
using UnityEngine.Rendering;

// Garante que o script usar� o Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]

public class PlayerController : MonoBehaviour
{
    public Player player;

    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    bool isWalking = false;

    Rigidbody2D rig2d;
    // Vetor de 2 lados para o movimento, come�ando vazio com o .zero
    Vector2 movement = Vector2.zero;

    void Start()
    {
        // Quando o jogo iniciar, a anima��o idle come�a
        isWalking = false;

        // atualiza o rig2d com base no rigidbody2d da unity 
        rig2d = GetComponent<Rigidbody2D>();

        // Para pegar a speed da entidade player
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mec�nica de movimenta��o 
        isWalking = false;

        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        if (input_x != 0 || input_y != 0)
        {
            isWalking = true;
        }

        // Mec�nica de movimenta��o
        movement = new Vector2(input_x, input_y);

        if (isWalking)
        {
            // Anima��o
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);

        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("attack");
        }
    }

    private void FixedUpdate()
    {
        // rig2d.position � a posi��o atual do player, o + movement � para somar o input, assim movendo
        // o normalizedMovement normaliza o vetor, corrigindo a velocidade acelerada ao andar na diagonal
        Vector2 normalizedMovement = movement.normalized;
        rig2d.MovePosition(rig2d.position + normalizedMovement * player.entity.speed * Time.fixedDeltaTime);
    }
}
