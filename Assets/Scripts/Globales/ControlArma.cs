using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ControlArma : MonoBehaviour
{
    [Header("Configuraci�n munici�n")]
    //public GameObject bolaPrefab;
    private PoolObjetos bolaPool;
    public Transform puntoSalida;
    public int municionActual;
    public int municionMax = Constantes.MUNICION_INICIAL;
    public bool municionInfinita = false;
    public float velocidadBola = 10;
    public float frecuenciaDisparo = 0.2f;
    private float ultimoTiempoDisparo;
    private bool esJugador;

    public static ControlArma instance;
    private void Awake()
    {
        //Soy el jugador??
        if (GetComponent<ControlJugadorIS>())
            esJugador = true;

        bolaPool = GetComponent<PoolObjetos>();

        municionActual = ArchivosGuardados.instance.datosGuardados.municion;

        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
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
        ControlHUD.instance.ActualizarNumBolasTexto(municionActual, municionMax);

    }
}
