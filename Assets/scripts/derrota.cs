using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class derrota : MonoBehaviour
{

    [SerializeField]
    GameObject PantallaDerrota;

    float tiempoderrota = 2.0f; //tiempo que dura la derrota

    bool personajemuerto = false;

    private Rigidbody2D movimientoRestringido;

    private AudioSource caida;


    private void Awake()
    {
       caida = gameObject.GetComponent<AudioSource>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PantallaDerrota.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //TIEMPO muerto

        if (personajemuerto)
        {
            tiempoderrota -= Time.deltaTime;

            if (tiempoderrota <= 0.0f)
            {
                // Restablece las restricciones de movimiento
                
                personajemuerto = false;

                // Reiniciar la escena actual
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                // Para reiniciar otros elementos del juego si es necesario
                GameController.ReiniciarCapsulas();

                PantallaDerrota.SetActive(false);

                // Reiniciar el tiempo para que siga funcionando
                tiempoderrota = 2.0f;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            // Código para desactivar la musica de la camara

            GameObject camaraPrincipal = GameObject.FindGameObjectWithTag("MainCamera");

            if (camaraPrincipal != null)
            {
                // Acceder al componente para eliminar o desactivar
                AudioSource componente = camaraPrincipal.GetComponent<AudioSource>();

                if (componente != null)
                {
                    // Desactivar el componente
                    componente.enabled = false; // Esto desactivará el componente
                }
            }
            caida.enabled = true;
            caida.Play();

            PantallaDerrota.SetActive(true);
            other.GetComponent<JugadorBolita>().enabled = false;

            personajemuerto = true;
        }

    }

   
}
