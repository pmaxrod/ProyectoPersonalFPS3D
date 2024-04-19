using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using System.Linq;

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
            // datosGuardados.tiempoJugadoTotal = 0;
            //datosGuardados.puntuacion = 0;
            //datosGuardados.municion = Constantes.MUNICION_INICIAL;
            //datosGuardados.posicion = Constantes.POSICION_INICIAL;
            //datosGuardados.rotacion = new Quaternion(0,0,0,0);
            //datosGuardados.tiempoJugadoPartida = 0;
            //datosGuardados.enemigos = new List<GameObject>();
            //datosGuardados.objetos = new List<GameObject>();
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
        if (SaveGame.Exists(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA)){
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

	public void DebugDatosGuardados(DatosGuardados datosGuardados){
		Debug.Log("Tiempo Jugado Total: " + datosGuardados.tiempoJugadoTotal);
        Debug.Log("Puntuacion: " + datosGuardados.puntuacion);
        Debug.Log("Municion: " + datosGuardados.municion);
        Debug.Log("Posicion: " + datosGuardados.posicion);
        Debug.Log("Rotacion: " + datosGuardados.rotacion);
        Debug.Log("Tiempo Jugado Partida: " + datosGuardados.tiempoJugadoPartida);

		foreach (Objeto enemigo in datosGuardados.enemigos)
		{
			Debug.Log("Enemigo: " + enemigo.ToString());
		}

		foreach (Objeto objeto in datosGuardados.objetos)
		{
			Debug.Log("Objeto: " + objeto.ToString());
		}

	}
}
