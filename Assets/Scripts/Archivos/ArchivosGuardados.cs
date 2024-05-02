using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class ArchivosGuardados : MonoBehaviour
{
    public DatosGuardados datosGuardados;

    public bool archivoCargado = false;

    public static ArchivosGuardados instance;

    private void Awake()
    {

        datosGuardados = new DatosGuardados();

        /*if (instance == null)*/
        DontDestroyOnLoad(transform.gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SaveGame.Exists(Constantes.NOMBRE_ARCHIVO_GUARDADO))
        {
            datosGuardados = SaveGame.Load<DatosGuardados>(Constantes.NOMBRE_ARCHIVO_GUARDADO);
            //datosGuardados.municion = SaveGame.Load<int>(Constantes.NOMBRE_ARCHIVO_GUARDADO);
        }
        else
        {
            InicializarDatosGuardados();

            SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO, datosGuardados);
        }

        DebugDatosGuardados(datosGuardados);
    }

    // Update is called once per frame
    void Update()
    {
        /*bool datosLeidos = false;
		if (ControlJuego.instancia != null && !datosLeidos && !archivoCargado){
			datosGuardados.enemigos = GameObject.FindGameObjectsWithTag(Constantes.ETIQUETA_ENEMIGO).ToList();
            datosGuardados.objetos = GameObject.FindGameObjectsWithTag(Constantes.ETIQUETA_OBJETO).ToList();
			datosLeidos = true;
		}*/
    }

    public void CargarDatos()
    {
        if (SaveGame.Exists(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA))
        {
            datosGuardados = SaveGame.Load<DatosGuardados>(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA);
            archivoCargado = true;

            DebugDatosGuardados(datosGuardados);
        }
    }

    public void BorrarArchivo(string archivo)
    {
        if (SaveGame.Exists(archivo))
            SaveGame.Delete(archivo);
        else
        {
            Debug.Log($"El archivo {archivo} no existe. Compruebe que el nombre est� bien escrito.");
        }
    }

    public void BorrarArchivoCarga()
    {
        BorrarArchivo(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA);
    }

    public void DebugDatosGuardados(DatosGuardados datosGuardados)
    {
        /*Debug.Log("Tiempo Jugado Total: " + datosGuardados.tiempoJugadoTotal);
        Debug.Log("Puntuacion: " + datosGuardados.puntuacion);
        Debug.Log("Municion: " + datosGuardados.municion);
        Debug.Log("Posicion: " + datosGuardados.posicion);
        Debug.Log("Rotacion: " + datosGuardados.rotacion);
        Debug.Log("Tiempo Jugado Partida: " + datosGuardados.tiempoJugadoPartida);*/

        foreach (Objeto objeto in datosGuardados.objetos)
        {
            //Debug.Log($"Objeto: {objeto.id}");
            Debug.Log(objeto.ToString()); 
        }

    }

    public void GuardarDatosFin(bool ganado, int puntuacionMaxima, double tiempoArchivo, TextMeshProUGUI textoPuntuacion)
    {
        int puntos;

        if (puntuacionMaxima < ControlJuego.instancia.puntuacionActual && ganado)
        {
            puntos = ControlJuego.instancia.puntuacionActual;
        }
        else
        {
            puntos = puntuacionMaxima;
        }

        if (textoPuntuacion != null)
            textoPuntuacion.text = "Puntuación máxima: " + puntos;

        DatosGuardados datos = new DatosGuardados(tiempoArchivo + ControlJuego.instancia.tiempoJugado, puntos);

        SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO, datos);

        Debug.Log("Guardando...");
        ArchivosGuardados.instance.BorrarArchivoCarga();

        //Debug.Log($"Archivo: {puntuacionArchivo} - Partida: {ControlJuego.instancia.puntuacionActual}");
    }

    public void GuardarDatosCarga(double tiempoArchivo)
    {
        DatosGuardados datos = new DatosGuardados(tiempoArchivo + ControlJuego.instancia.tiempoJugado, ControlJuego.instancia.puntuacionActual, ControlJugadorIS.instance.vidasActual, ControlJugadorIS.instance.controlResistencia.resistenciaActual, ControlJugadorIS.instance.arma.municionActual, ControlJugadorIS.instance.gameObject.transform.position, ControlJugadorIS.instance.gameObject.transform.rotation);

        datos.tiempoJugadoPartida = ControlJuego.instancia.tiempoJugado;
        //datos.enemigos = ControlJuego.instancia.enemigos;
        datos.objetos = ControlJuego.instancia.objetos;

        DebugDatosGuardados(datos);

        SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA, datos);
    }

    public void NuevaPartida()
    {
        BorrarArchivoCarga();

        InicializarDatosGuardados();

        SceneManager.LoadScene("Nivel1");
    }

    public void CargarPartida()
    {
        CargarDatos();

        BorrarArchivoCarga();

        SceneManager.LoadScene("Nivel1");
    }

    public void BorrarDatos()
    {
        BorrarArchivo(Constantes.NOMBRE_ARCHIVO_GUARDADO);
        BorrarArchivoCarga();

        ControlHUD.instancia.CerrarBorrarDatos();
        ControlHUD.instancia.TextoTiempoJugado(0);

        ControlHUD.instancia.botonCargarPartida.interactable = false;
    }
    private void InicializarDatosGuardados()
    {
        datosGuardados.vida = Constantes.VIDA_INICIAL;
        datosGuardados.resistencia = Constantes.RESISTENCIA_INICIAL;
        datosGuardados.tiempoJugadoTotal = 0;
        datosGuardados.puntuacion = 0;
        datosGuardados.municion = Constantes.MUNICION_INICIAL;
        datosGuardados.posicion = Constantes.POSICION_INICIAL;
        datosGuardados.rotacion = new Quaternion(0, 0, 0, 0);
        datosGuardados.tiempoJugadoPartida = 0;
        datosGuardados.objetos = new List<Objeto>();
    }

}
