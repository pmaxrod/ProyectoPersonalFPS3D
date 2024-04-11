using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;
using System;

public class ControlHUD : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI puntuacionTexto;
    public TextMeshProUGUI numBolasTexto;
    //public Image barraVidas;
    public Slider barraVidas;
    public TextMeshProUGUI tiempoJugadoTotal;

    [Header("Ventana de Pausa")]
    public GameObject ventanaPausa;

    [Header("Ventana Fin Juego")]
    public GameObject ventanaFinJuego;
    public TextMeshProUGUI resultadoTexto;
    public TextMeshProUGUI puntuacionTextoFin;
    public TextMeshProUGUI puntuacionMaximaTexto;

    [Header("Ventana Borrar Datos")]
    public GameObject ventanaBorrarDatos;
	
    public static ControlHUD instancia;

    private int puntuacionArchivo;
    private double tiempoArchivo;

    private void Awake()
    {
        instancia = this;
		if (ventanaBorrarDatos != null)
			ventanaBorrarDatos.SetActive(false);
    }

    public void ActualizaBarraVida(int vidaActual, int vidaMax)
    {
        //barraVidas.fillAmount = (float)vidaActual /(float)vidaMax;
        //barraVidas.value = (float)vidaActual / (float)vidaMax;
        barraVidas.value = (float)vidaActual;
    }

    public void ActualizarNumBolasTexto(int numBolasActual, int numBolasMax)
    {
        numBolasTexto.text = "Bolas: " + numBolasActual + " / " + numBolasMax;
    }

    public void ActualizarPuntuacion(int puntuacion)
    {
        puntuacionTexto.text = ControlJuego.instancia.puntuacionActual.ToString("00000");
    }

    public void CambiarEstadoVentanaPausa(bool pausa)
    {
        ventanaPausa.SetActive(pausa);
    }

    public void EstablecerVentanaFinJuego(bool ganado)
    {
        ventanaFinJuego.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;

        resultadoTexto.text = (ganado) ? "Has Ganado!!" : "Has Perdido!!";
        resultadoTexto.color = (ganado) ? Color.green : Color.red;

        if (ganado)
        {
            puntuacionTextoFin.gameObject.SetActive(true);
            puntuacionTextoFin.text = $"Puntuación: {ControlJuego.instancia.puntuacionActual}";
            puntuacionTextoFin.color = Color.green;
        }
        int puntos;

        if (puntuacionArchivo < ControlJuego.instancia.puntuacionActual && ganado)
        {
            puntos = ControlJuego.instancia.puntuacionActual;
        }
        else
        {
            puntos = puntuacionArchivo;
        }

        if (puntuacionMaximaTexto != null)
            puntuacionMaximaTexto.text = "Puntuación máxima: " + puntos;

        DatosGuardados datos = new DatosGuardados(tiempoArchivo + ControlJuego.instancia.tiempoJugado, puntos);

        SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO, datos);
        Debug.Log($"Archivo: {puntuacionArchivo} - Partida: {ControlJuego.instancia.puntuacionActual}");

    }

    public void OnBotonMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnBotonVolver()
    {
        ControlJuego.instancia.CambiarPausa();
    }

    public void OnBotonEmpezar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void OnBotonSalir()
    {
        Application.Quit();
    }
	
	public void AbrirBorrarDatos()
	{
		ventanaBorrarDatos.SetActive(true);
	}

	public void CerrarBorrarDatos()
	{
		ventanaBorrarDatos.SetActive(false);
	}


    public void BorrarDatos()
    {
        ArchivosGuardados.instance.BorrarDatos();
        CerrarBorrarDatos();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ArchivosGuardados.instance.datosGuardados.tiempoJugado);
        Debug.Log(ArchivosGuardados.instance.datosGuardados.puntuacion);

        tiempoArchivo = ArchivosGuardados.instance.datosGuardados.tiempoJugado;
        puntuacionArchivo = ArchivosGuardados.instance.datosGuardados.puntuacion;

        if (tiempoJugadoTotal != null){
			TextoTiempoJugado();
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempoJugadoTotal != null && tiempoArchivo == 0){
            TextoTiempoJugado();
		}

    }
	
	private void TextoTiempoJugado(){
		tiempoJugadoTotal.text = "Tiempo Jugado: " + ArchivosGuardados.instance.datosGuardados.TiempoFormateado(tiempoArchivo);
	}
	
}
