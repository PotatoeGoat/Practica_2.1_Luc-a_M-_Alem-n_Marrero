using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    public Transform jugador; // Referencia al objeto del jugador.
    public float distanciaDeseada = 5.0f; // Distancia deseada entre la cámara y el jugador.
    public float alturaDeseada = 3.0f; // Altura deseada de la cámara sobre el jugador.
    public float velocidadSeguimiento = 5.0f; // Velocidad de seguimiento suave.

    void Update()
    {
        // Asegurarse de que el jugador esté asignado en el Inspector.
        if (jugador == null)
        {
            Debug.LogWarning("Asigna el jugador en el Inspector.");
            return;
        }

        // Calcular la dirección desde la cámara al jugador.
        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0; // Mantener la cámara a la misma altura del suelo.

        // Calcular la nueva posición de la cámara para mantener la distancia constante y la altura deseada.
        Vector3 posicionDeseada = jugador.position - direccion.normalized * distanciaDeseada;
        posicionDeseada.y = jugador.position.y + alturaDeseada; // Ajustar la altura de la cámara.

        transform.position = Vector3.MoveTowards(transform.position, posicionDeseada, velocidadSeguimiento * Time.deltaTime);

        // Hacer que la cámara mire al jugador y mantenga la rotación del eje Z del mundo.
        transform.LookAt(jugador, Vector3.up);
    }
}

