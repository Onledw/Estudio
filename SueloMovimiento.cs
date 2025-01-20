using UnityEngine;

public class SueloMovimiento : MonoBehaviour
{
    private Rigidbody2D rb;

    // Configuración del movimiento
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private Vector2 destino;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destino = puntoB.position;
    }

    void FixedUpdate()
    {
        MoverPlataforma();
    }

    private void MoverPlataforma()
    {
        Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, destino, velocidad * Time.fixedDeltaTime);
        rb.MovePosition(nuevaPosicion);

        // Actualizar la velocidad manualmente
        rb.linearVelocity = (destino - rb.position).normalized * velocidad;

        if (Vector2.Distance(rb.position, destino) < 0.1f)
        {
            destino = destino == (Vector2)puntoA.position ? (Vector2)puntoB.position : (Vector2)puntoA.position;
        }
    }
}
