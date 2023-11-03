using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coleccionable : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI CapsulasNum;

    [SerializeField]
    GameObject PantallaMeta;

    [SerializeField]
    TextMeshProUGUI textLabelTime;

    float tiempoDePartida = 0.0f;

    bool estaJugando = true;


    public Enemigo enemigoScript;

    private AudioSource monedaSound;

    private bool MonedaRecogida;

    private ParticleSystem particulas;


    private void Awake()
    {
        monedaSound = gameObject.GetComponent<AudioSource>();
        particulas = GetComponent<ParticleSystem>();
    }



    // Start is called before the first frame update
    void Start()
    {
        PantallaMeta.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RotateAround();

        if (estaJugando == true)
        {
            tiempoDePartida = tiempoDePartida + Time.deltaTime;
        }

        if (MonedaRecogida && !monedaSound.isPlaying)
        {
            // Destruye el caramelo
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            MonedaRecogida = true;

            monedaSound.enabled = true;

            Debug.Log("Capsula encontrada");
            GameController.capsulas++;
            CapsulasNum.text = GameController.capsulas.ToString();

            particulas.Play();

            //"destruye el objeto"
            gameObject.GetComponent<MeshRenderer>().enabled = false;
           
        }

        if ( GameController.capsulas == 8)
        {

            PantallaMeta.SetActive(true);
            other.GetComponent<JugadorBolita>().enabled = false;
            estaJugando = false;
            textLabelTime.text = tiempoDePartida.ToString();
            DesactivarEnemigo();
        }
    }

    void RotateAround()
    {
        transform.Rotate(0.0f, 100f * Time.deltaTime, 0.0f);
    }

    void DesactivarEnemigo()
    {
        // Desactivar el script del enemigo
        enemigoScript.enabled = false;
    }

}
