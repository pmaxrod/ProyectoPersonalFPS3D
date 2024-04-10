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

        datosGuardados = new DatosGuardados(0, 0);
		
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
            datosGuardados.tiempoJugado = 0;
            datosGuardados.puntuacion = 0;
            SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO, datosGuardados);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BorrarDatos()
    {
        if (SaveGame.Exists(Constantes.NOMBRE_ARCHIVO_GUARDADO))
            SaveGame.Delete(Constantes.NOMBRE_ARCHIVO_GUARDADO);
    }
}
