using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private float multiplicadorVelocidade = 1f;
    AudioSource coin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coin = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = move * speed * multiplicadorVelocidade;
    }

    public void AplicarMultiplicadorVelocidade(float multiplicador)
    {
        multiplicadorVelocidade = multiplicador;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletável"))
        {
            coin.Play();
            GameController.Collect();
            Destroy(other.gameObject);
        }
    }
}
