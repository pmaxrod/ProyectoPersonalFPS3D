using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class ArchivosGuardados : MonoBehaviour
{
    public DatosGuardados datosGuardados;

    public static ArchivosGuardados instance;

    private void Awake()
    {
        instance = this;

        datosGuardados = new DatosGuardados();

        DontDestroyOnLoad(transform.gameObject);
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
            // datosGuardados.enemigos = new List<GameObject>();
            //datosGuardados.objetos = new List<GameObject>();
            
            SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO, datosGuardados);
        }

        Debug.Log(datosGuardados.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CargarDatos()
    {
        if (SaveGame.Exists(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA))
            datosGuardados = SaveGame.Load<DatosGuardados>(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA);
        Debug.Log(datosGuardados.ToString());
    }

    public void BorrarArchivo(string archivo)
    {
        if (SaveGame.Exists(archivo))
            SaveGame.Delete(archivo);
        else
        {
            Debug.Log($"El archivo {archivo} no existe. Compruebe que el nombre esté bien escrito.");
        }
    }

    public void BorrarArchivoCarga()
    {
        BorrarArchivo(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA);
    }
}
