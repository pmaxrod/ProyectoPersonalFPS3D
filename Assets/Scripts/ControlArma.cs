using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ControlArma : MonoBehaviour
{
    [Header("Configuración munición")]
    //public GameObject bolaPrefab;
    private PoolObjetos bolaPool;
    public Transform puntoSalida;
    public int municionActual = 10;
    public int municionMax = 50;
    public bool municionInfinita = false;
    public float velocidadBola = 10;
    public float frecuenciaDisparo = 0.2f;
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

        if (!municionInfinita)
            municionActual--;

        //GameObject bola = Instantiate(bolaPrefab, puntoSalida.position, puntoSalida.rotation);
        GameObject bola = bolaPool.getObjeto();

        bola.transform.position = puntoSalida.position;
        bola.transform.rotation = puntoSalida.rotation;
        bola.GetComponent<Rigidbody>().velocity = puntoSalida.forward * velocidadBola;

        if (esJugador)
        {
            // Crea un rayo desde la camara al medio de la pantalla
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            Vector3 targetPoint;

            // Comprueba si estas apuntando y ajusta la direccion
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(5); //5m

            bola.GetComponent<Rigidbody>().velocity = (targetPoint - bola.transform.position).normalized * velocidadBola;
        }
        else
        {
            // Disparo enemigo
            bola.GetComponent<Rigidbody>().velocity = puntoSalida.forward * velocidadBola;
        }
        ControlHUD.instancia.ActualizarNumBolasTexto(municionActual, municionMax);

    }
}
