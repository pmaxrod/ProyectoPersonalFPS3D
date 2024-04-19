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

    public List<GameObject> enemigos, objetos;

    public static ControlJuego instancia;


    private void Awake()
    {
        instancia = this;
        //tiempoJugado = Constantes.TIEMPO_PARTIDA > 0 ? Constantes.TIEMPO_PARTIDA : 0;
        tiempoJugado = ArchivosGuardados.instance.datosGuardados.tiempoJugadoPartida > 0 ? ArchivosGuardados.instance.datosGuardados.tiempoJugadoPartida : 0;
        puntuacionActual = tiempoJugado > 0 ? ArchivosGuardados.instance.datosGuardados.puntuacion : 0;

		enemigos = ArchivosGuardados.instance.archivoCargado ? ArchivosGuardados.instance.datosGuardados.enemigos 
		: GameObject.FindGameObjectsWithTag(Constantes.ETIQUETA_ENEMIGO).ToList();
		objetos = ArchivosGuardados.instance.archivoCargado ?  ArchivosGuardados.instance.datosGuardados.objetos
		: GameObject.FindGameObjectsWithTag(Constantes.ETIQUETA_OBJETO).ToList();
     }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            CambiarPausa();

        //Calcular el número de enemigos
        int numEnemigos = enemigos.Count;
        //Debug.Log(numEnemigos);
        if(numEnemigos <= 0)
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

        // Para ganar por puntuación
        //if (puntuacionActual >= puntuacionParaGanar)
        //    GanarJuego();
    }

    public void GanarJuego()
    {
        ControlHUD.instancia.EstablecerVentanaFinJuego(true);
    }
}
