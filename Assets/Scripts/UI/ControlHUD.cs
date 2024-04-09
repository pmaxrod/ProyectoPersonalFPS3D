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

    public static ControlHUD instancia;

    private int puntuacionArchivo;
    private double tiempoArchivo;

    private void Awake()
    {
        instancia = this;
        puntuacionArchivo = 0;
        tiempoArchivo = 0;
    }

    public void ActualizaBarraVida(int vidaActual, int vidaMax)
    {
        //barraVidas.fillAmount = (float)vidaActual /(float)vidaMax;
        barraVidas.value = (float)vidaActual / (float)vidaMax;
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

            int puntos;

            if (puntuacionArchivo < ControlJuego.instancia.puntuacionActual)
            {
                puntos = ControlJuego.instancia.puntuacionActual;
            }
            else
            {
                puntos = puntuacionArchivo;
            }

            DatosGuardados datos = new DatosGuardados(tiempoArchivo + ControlJuego.instancia.tiempoJugado, puntos);

            SaveGame.Save(Constantes.NOMBRE_ARCHIVO_GUARDADO, datos);
            Debug.Log($"Archivo: {puntuacionArchivo} - Partida: {ControlJuego.instancia.puntuacionActual}");

        }
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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ArchivosGuardados.instance.datosGuardados);

        tiempoArchivo = ArchivosGuardados.instance.datosGuardados.tiempoJugado;
        puntuacionArchivo = ArchivosGuardados.instance.datosGuardados.puntuacion;

        Debug.Log(puntuacionArchivo);

        if (puntuacionMaximaTexto != null)
            puntuacionMaximaTexto.text = "Puntuación máxima: " + puntuacionArchivo;
        if (tiempoJugadoTotal != null)
            tiempoJugadoTotal.text = "Tiempo Jugado: " + ArchivosGuardados.instance.datosGuardados.TiempoFormateado(tiempoArchivo);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
