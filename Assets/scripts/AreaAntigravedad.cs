using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaAntigravedad : MonoBehaviour
{
    int ctrl;
    private bool NoArea = true;

    public Transform jugador;
    public float velocidadPersecucion = 5.0f;
    public float velocidadMovimiento = 2.0f; // Velocidad de movimiento de la plataforma en el eje Z
    public float minZ = -25.0f; // Límite inferior en el eje Z
    public float maxZ = -15.0f; // Límite superior en el eje Z

    private Rigidbody jugadorRigidbody;

    private MeshRenderer meshRenderer;
    private Vector3 posicionOrigen = new Vector3(13.21f, 9.3f, -14.9f);

    [SerializeField]
    GameObject RecogeMasPills;

    float tiempoderrota = 1.0f;
    bool personajemuerto = false;

    void Start()
    {
        ctrl = 1;
        NoArea = true;

        meshRenderer = GetComponent < MeshRenderer>();

        RecogeMasPills.SetActive(false);
    }

    void Update()
    {
        if (NoArea == true)
        {
            if (transform.position.z <= minZ) ctrl = 1;
            if (transform.position.z >= maxZ) ctrl = -1;

            float newPositionZ = transform.position.z + velocidadMovimiento * Time.deltaTime * ctrl;
            transform.position = new Vector3(transform.position.x, transform.position.y, newPositionZ);
        }
       


        if (NoArea == false && GameController.capsulas == 4)
        {
            Vector3 direccion = jugador.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidadPersecucion * Time.deltaTime);
            
        }

        if (NoArea == false && GameController.capsulas < 4)
        {
            personajemuerto = true;
            Debug.Log("Necesitas mas pills");
            RecogeMasPills.SetActive(true);
        }

        Salirdelarea();




        if (personajemuerto)
        {
            tiempoderrota -= Time.deltaTime;

            if (tiempoderrota <= 0.0f)
            { 

                personajemuerto = false;

             
                RecogeMasPills.SetActive(false);

                // Reiniciar el tiempo para que siga funcionando
                tiempoderrota = 1.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            jugadorRigidbody = other.GetComponent<Rigidbody>();
            jugadorRigidbody.useGravity = false;
            jugadorRigidbody.isKinematic = true;
            NoArea = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            jugadorRigidbody.useGravity = true;
            NoArea = true;
            jugadorRigidbody.isKinematic = false;
        }
    }

    void Salirdelarea()
    {
        if (NoArea ==false && Input.GetKeyDown("x"))
        {
            NoArea = true;
            jugadorRigidbody.useGravity = true;
            jugadorRigidbody.isKinematic = false;

            // Desactiva el MeshRenderer del área
            meshRenderer.enabled = false;

            // Restablece la posición del área a su posición de origen
            transform.position = posicionOrigen;// Coloca aquí la posición de origen del área

            meshRenderer.enabled = true;
        }
    }
}


