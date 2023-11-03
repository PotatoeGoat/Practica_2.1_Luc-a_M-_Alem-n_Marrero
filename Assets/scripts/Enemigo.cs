using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform jugador;  // Referencia al objeto que queremos perseguir (el jugador).
    public float velocidadPersecucion = 3.0f;  // Velocidad a la que el enemigo persigue al jugador.
    public float fuerzaSaltoEnemigo = 5.0f; // Fuerza del salto del enemigo.
    private bool enPlataforma = false; // Variable para verificar si el enemigo está en una plataforma.
    private Vector3 posicionInicial;


    void Start()
    {
        // Almacena la posición inicial al inicio del juego.
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (jugador == null)
        {
            return;
        }

        // Calculamos la dirección hacia la que queremos ir (jugador - posición actual).
        Vector3 direccion = jugador.position - transform.position;

        // Movemos el enemigo en la dirección del jugador usando MoveTowards.
        transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidadPersecucion * Time.deltaTime);

        // Hacemos que el enemigo siempre mire hacia el jugador usando LookAt.
        transform.LookAt(jugador);

        // Verificamos si el enemigo está en una plataforma.
        if (enPlataforma && EstaSaltandoElJugador())
        {
            Saltar();
        }
    }


    void OnTriggerStay(Collider other)
    {
        // Si el enemigo está en contacto con el objeto especial, realiza la acción deseada.
        if (other.CompareTag("sueloTransparente"))
        {
            // Restablece la posición del enemigo a la posición inicial.
            transform.position = posicionInicial;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            enPlataforma = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            enPlataforma = false;
        }
    }

    bool EstaSaltandoElJugador()
    {
        // Verificar si el jugador está saltando (puedes modificar esto según cómo detectes el salto del jugador).
        return Input.GetKeyDown("space");
    }

    void Saltar()
    {
        // Aplicar la fuerza de salto al enemigo.
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * fuerzaSaltoEnemigo, ForceMode.Impulse);
    }
}


