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
    public Button botonCargarPartida;

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

        if (botonCargarPartida != null)
            botonCargarPartida.interactable = SaveGame.Exists(Constantes.NOMBRE_ARCHIVO_GUARDADO_CARGA);
    }

    // Start is called before the first frame update
    void Start()
    {
        tiempoArchivo = ArchivosGuardados.instance.datosGuardados.tiempoJugadoTotal;
        puntuacionArchivo = ArchivosGuardados.instance.datosGuardados.puntuacion;

        if (tiempoJugadoTotal != null)
        {
            TextoTiempoJugado(tiempoArchivo);
        }
    }

    // Update is called once per frame
    void Update()
    {
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

        ArchivosGuardados.instance.GuardarDatosFin(ganado, ControlJuego.instancia.puntuacionActual, ControlJuego.instancia.tiempoJugado, puntuacionMaximaTexto);

    }

    public void AbrirBorrarDatos()
    {
        ventanaBorrarDatos.SetActive(true);
    }

    public void CerrarBorrarDatos()
    {
        ventanaBorrarDatos.SetActive(false);
    }

    public void TextoTiempoJugado(double tiempo)
    {
        tiempoJugadoTotal.text = "Tiempo Jugado: " + ArchivosGuardados.instance.datosGuardados.TiempoFormateado(tiempo);
    }

    public void OnBotonMenu()
    {
        ArchivosGuardados.instance.GuardarDatosCarga(tiempoArchivo);

        SceneManager.LoadScene("Menu");
    }

    public void OnBotonVolver()
    {
        ControlJuego.instancia.CambiarPausa();
    }

    public void OnBotonSalir()
    {
        Application.Quit();
    }
}
