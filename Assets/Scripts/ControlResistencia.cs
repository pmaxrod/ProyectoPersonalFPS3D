using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlResistencia : MonoBehaviour
{
    public float resistenciaMax = 100f;
    private float resistenciaActual;
    public float regeneracionResistencia = 5f;
    public float usoResistenciaCorrer = 10f;
    public float velocidadCorrer = 10f;
    private float velocidadAndar;

    private ControlJugadorIS controlJugador;

    public Slider barraResistencia;

    private bool estaCorriendo = false;

    private void Start()
    {
        controlJugador = GetComponent<ControlJugadorIS>();
        velocidadAndar = controlJugador.velocidadMovimiento;
        resistenciaActual = resistenciaMax;

        // Pone el valor inicial de la barra de resistencia
        ActualizarBarraResistencia();
    }

    private void Update()
    {
        // Corre mientras el jugador pulse la tecla Shift
        // estaCorriendo = Input.GetKey(KeyCode.LeftShift);

        // Usar resistencia al correr
        if (estaCorriendo)
        {
            UsarResistencia(usoResistenciaCorrer * Time.deltaTime);
            if (barraResistencia.value > 0)
                controlJugador.velocidadMovimiento = velocidadCorrer;
            else
            {
                estaCorriendo = false;
            }
        }
        else // Regenerar resistencia cuando no se está corriendo
        {
            controlJugador.velocidadMovimiento = velocidadAndar;
            RegenerarResistencia(regeneracionResistencia * Time.deltaTime);
        }

        // Actualizar la interfaz
        ActualizarBarraResistencia();
    }

    private void UsarResistencia(float amount)
    {
        resistenciaActual = Mathf.Clamp(resistenciaActual - amount, 0f, resistenciaMax);
    }

    private void RegenerarResistencia(float amount)
    {
        resistenciaActual = Mathf.Clamp(resistenciaActual + amount, 0f, resistenciaMax);
    }

    private void ActualizarBarraResistencia()
    {
        barraResistencia.value = resistenciaActual / resistenciaMax;
    }

    void OnCorrer()
    {
        estaCorriendo = !estaCorriendo;
    }
}
