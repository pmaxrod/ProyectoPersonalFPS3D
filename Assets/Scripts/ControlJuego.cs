using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJuego : MonoBehaviour
{
    public int puntuacionActual;
    public int puntuacionParaGanar;

    public bool juegoPausado;

    public static ControlJuego instancia;

    private void Awake()
    {
        instancia = this;
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
        int numEnemigos = GameObject.FindGameObjectsWithTag("Enemigo").Length;
        Debug.Log(numEnemigos);
        if(numEnemigos <= 0)
            GanarJuego();
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
