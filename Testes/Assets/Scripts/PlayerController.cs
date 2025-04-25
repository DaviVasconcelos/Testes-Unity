using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    public float speed = 2.5f;
    bool isWalking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Quando o jogo iniciar, a anmima��o idle come�a
        isWalking = false;
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

        if (isWalking)
        {
            var move = new Vector3 (input_x, input_y, 0).normalized;

            transform.position += move * speed * Time.deltaTime;

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
}
