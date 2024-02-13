using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArma : MonoBehaviour
{
    [Header("Configuración munición")]
    //public GameObject bolaPrefab;
    private PoolObjetos bolaPool;
    public Transform puntoSalida;
    public int municionActual=10;
    public int municionMax=50;
    public bool municionInfinita=false;
    public float velocidadBola=10;
    public float frecuenciaDisparo=0.2f;
    private float ultimoTiempoDisparo;
    private bool esJugador;

    private void Awake()
    {
        //Soy el jugador??
        if (GetComponent<ControlJugadorIS>())
            esJugador = true;
        
        bolaPool = GetComponent<PoolObjetos>();
    }

    public bool PuedeDisparar()
    {
        if (Time.time - ultimoTiempoDisparo >= frecuenciaDisparo)
            if (municionActual > 0 || municionInfinita == true)
                return true;
        return false;

    }

    public void Disparar()
    {
        ultimoTiempoDisparo = Time.time;
        municionActual --;
        //GameObject bola = Instantiate(bolaPrefab, puntoSalida.position, puntoSalida.rotation);
        GameObject bola = bolaPool.getObjeto();
        bola.transform.position = puntoSalida.position;
        bola.transform.rotation = puntoSalida.rotation;
        bola.GetComponent<Rigidbody>().velocity = puntoSalida.forward * velocidadBola;
    }

}
