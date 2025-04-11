using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    public float speed = 2f;

    private Vector3 target;
    private Rigidbody2D rb;
    private float multiplicadorVelocidade = 1f;
    private bool bonusAtivo = false;

    void Start()
    {
        // Verifica se os pontos foram configurados
        if (pontoA == null || pontoB == null)
        {
            Debug.LogError("Pontos A e B precisam ser configurados no objeto " + gameObject.name);
            enabled = false;
            return;
        }

        target = pontoB.position;
        rb = GetComponent<Rigidbody2D>();
        
        // Se não tiver Rigidbody2D, adiciona um
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
            rb.useFullKinematicContacts = true;
            rb.gravityScale = 0f;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        float velocidadeAtual = speed * multiplicadorVelocidade;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, velocidadeAtual * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            // Troca o alvo
            target = (target == pontoA.position) ? pontoB.position : pontoA.position;
        }
    }

    public void AplicarBonus(float multiplicador)
    {
        multiplicadorVelocidade = multiplicador;
        bonusAtivo = true;
    }

    public void DesativarBonus()
    {
        multiplicadorVelocidade = 1f;
        bonusAtivo = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Se estiver com bônus ativo, causa mais dano
            int dano = bonusAtivo ? 2 : 1;
            
            PlayerHealth vida = collision.gameObject.GetComponent<PlayerHealth>();
            if (vida != null)
            {
                vida.LevarDano(dano);
            }
        }
    }

    // Opcional: Desenha o raio de detecção na janela do editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
