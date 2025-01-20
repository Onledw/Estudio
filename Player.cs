using UnityEngine;

public class Player : MonoBehaviour
{
    // Componentes
    private new Rigidbody2D rigidbody2D;
    private Rigidbody2D sueloMovimiento;

    // Variables públicas
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    public LayerMask suelo;
    public Transform sueloCheck;
    public Vector2 sueloCheckSize = new Vector2(0.5f, 0.1f);

    // Variables privadas
    private int saltosRestantes;
    private int maxSaltos = 2;
    private bool enSuelo = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        saltosRestantes = maxSaltos;
    }

    void Update()
    {
        Salto();
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector2 velocidadPlataforma = sueloMovimiento != null ? sueloMovimiento.linearVelocity : Vector2.zero;
        rigidbody2D.linearVelocity = new Vector2(inputX * velocidad + velocidadPlataforma.x, rigidbody2D.linearVelocity.y);
    }

    private void Salto()
    {
        if (Input.GetButtonDown("Jump") && saltosRestantes > 0)
        {
            rigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltosRestantes--;
        }
        ComprobarSuelo();
    }

    private void ComprobarSuelo()
    {
        enSuelo = Physics2D.OverlapBox(sueloCheck.position, sueloCheckSize, 0f, suelo);

        if (enSuelo)
        {
            saltosRestantes = maxSaltos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plataforma"))
        {
            sueloMovimiento = collision.collider.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plataforma"))
        {
            sueloMovimiento = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(sueloCheck.position, sueloCheckSize);
    }
}
