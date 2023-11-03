using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioModoCamara : MonoBehaviour
{
    public Camera camaraTerceraPersona; // Referencia a la cámara de tercera persona.
    public Camera camaraPlanoGeneral; // Referencia a la cámara de plano general.

    private bool modoPlanoGeneral = false;

    void Start()
    {
        // Inicialmente, activa la cámara de tercera persona y desactiva la cámara de plano general.
        camaraTerceraPersona.enabled = true;
        camaraPlanoGeneral.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            modoPlanoGeneral = !modoPlanoGeneral;
            CambiarModoCamara(modoPlanoGeneral);
        }
    }

    void CambiarModoCamara(bool planoGeneral)
    {
        if (planoGeneral)
        {
            // Modo Plano General
            camaraTerceraPersona.enabled = false;
            camaraPlanoGeneral.enabled = true;
        }
        else
        {
            // Modo Tercera Persona
            camaraTerceraPersona.enabled = true;
            camaraPlanoGeneral.enabled = false;
        }
    }
}
