using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class ArchivosGuardados : MonoBehaviour
{
    [Tooltip("Archivo de guardado")]
    public DatosGuardados datosGuardados;

	public static ArchivosGuardados instance;
	
    // Start is called before the first frame update
    void Start()
    {
         if (SaveGame.Exists("archivo.fps")){
            datosGuardados.tiempoJugado = SaveGame.Load<double>("archivo.fps");
            datosGuardados.municion = SaveGame.Load<int>("archivo.fps");			 
		 }
		 
		 instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
