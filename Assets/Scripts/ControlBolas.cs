using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBolas : MonoBehaviour
{
    public GameObject particulasExplosion;

    public int cantidadVida;
    public float tiempoActivo;
    private float tiempoDisparo;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - tiempoDisparo >= tiempoActivo)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        tiempoDisparo = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<ControlJugadorIS>().QuitarVidasJugador(cantidadVida);
        else if (other.CompareTag("Enemigo"))
            other.GetComponent<ControlEnemigo>().QuitarVidasEnemigo(cantidadVida);

        // Creamos las partículas de explosión
        GameObject particulas = Instantiate(particulasExplosion, transform.position, Quaternion.identity);
        // Se destruye cuando pasa un segundo
        Destroy(particulas, 1.0f);


        // Desactivación de la bola
        gameObject.SetActive(false);
    }

}
