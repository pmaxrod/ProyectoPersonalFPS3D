using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlJuego : MonoBehaviour
{
    public int puntuacionActual;
    public int puntuacionParaGanar;

    public bool juegoPausado;
    public double tiempoJugado;

    public bool ganado = false;

    public List<Objeto> objetos;
    public static ControlJuego instancia;



    private void Awake()
    {
        instancia = this;
        juegoPausado = false;
        //tiempoJugado = Constantes.TIEMPO_PARTIDA > 0 ? Constantes.TIEMPO_PARTIDA : 0;
        tiempoJugado = ArchivosGuardados.instance.datosGuardados.tiempoJugadoPartida > 0 ? ArchivosGuardados.instance.datosGuardados.tiempoJugadoPartida : 0;
        puntuacionActual = tiempoJugado > 0 ? ArchivosGuardados.instance.datosGuardados.puntuacion : 0;

        if (ArchivosGuardados.instance.archivoCargado)
        {
            objetos = ArchivosGuardados.instance.datosGuardados.objetos;
        }
        else
        {
            objetos = new List<Objeto>();
        }
        //Debug.Log(ArchivosGuardados.instance.archivoCargado);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CambiarPausa();
        }

        //Calcular el n�mero de enemigos
        int numEnemigos = GameObject.FindGameObjectsWithTag(Constantes.ETIQUETA_ENEMIGO).ToList().Count;
        //Debug.Log(numEnemigos);
        if (numEnemigos <= 0)
            GanarJuego();

        if (!juegoPausado && !ControlHUD.instancia.ventanaFinJuego.activeSelf)
        {
            tiempoJugado += Time.deltaTime;
        }
    }

    public void CambiarPausa()
    {
        juegoPausado = !juegoPausado;
        Time.timeScale = (juegoPausado) ? 0.0f : 1f;
        Cursor.lockState = (juegoPausado) ? CursorLockMode.None : CursorLockMode.Locked;

        ControlHUD.instancia.CambiarEstadoVentanaPausa(juegoPausado);
    }

    public void PonerPuntuacion(int puntuacion)
    {
        puntuacionActual += puntuacion;
        ControlHUD.instancia.ActualizarPuntuacion(puntuacionActual);

        // Para ganar por puntuaci�n
        //if (puntuacionActual >= puntuacionParaGanar)
        //    GanarJuego();
    }

    public void GanarJuego()
    {
        ganado = true;
        ControlHUD.instancia.EstablecerVentanaFinJuego(ganado);
    }

    public void InstanciarObjetoJuego(Objeto objeto, GameObject gameObject)
    {
        Debug.Log(objeto.ToString());
        if (!ArchivosGuardados.instance.archivoCargado)
        {
            objetos.Add(objeto);
        }
        else
        {
            if (objeto != null)
            {
                gameObject.transform.position = objeto.GetPosicion();
                gameObject.transform.rotation = objeto.GetRotacion();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //        Debug.Log(objeto.id);
    }
}
