using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaTrampa : MonoBehaviour
{
    public Color nuevoColor; // Color a aplicar cuando el jugador toque la plataforma.
    private bool tocada = false; // Variable para verificar si el jugador tocó la plataforma.
    private float tiempoDesaparicion = 1.0f; // Tiempo hasta que la plataforma desaparezca.
    private float tiempoTranscurrido = 0.0f; // Tiempo transcurrido desde que el jugador tocó la plataforma.
    private bool desapareciendo = false; // Variable para verificar si la plataforma está desapareciendo.

    private Material materialOriginal; // Material original de la plataforma.

    private void Start()
    {
        materialOriginal = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (tocada)
        {
            // Cambiar el color de la plataforma al color deseado.
            GetComponent<Renderer>().material.color = nuevoColor;

            // Incrementar el tiempo transcurrido.
            tiempoTranscurrido += Time.deltaTime;

            // Verificar si ha pasado suficiente tiempo para que la plataforma desaparezca.
            if (tiempoTranscurrido >= tiempoDesaparicion && !desapareciendo)
            {
                // Desactivar la plataforma 
                gameObject.SetActive(false);
                desapareciendo = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !tocada)
        {
            tocada = true;
        }
    }
}

