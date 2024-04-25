using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlResistencia : MonoBehaviour
{
    public float resistenciaMax = 100f;
    public float resistenciaActual;
    public float regeneracionResistencia = 5f;
    public float usoResistenciaCorrer = 10f;
    public float velocidadCorrer = 15f;
    private float velocidadAndar;

    private ControlJugadorIS controlJugador;

    public Slider barraResistencia;

    public bool estaCorriendo = false;

    private PlayerInput playerInput;
	
    private void Start()
    {
        controlJugador = GetComponent<ControlJugadorIS>();
        velocidadAndar = controlJugador.velocidadMovimiento;
        resistenciaActual = ArchivosGuardados.instance.datosGuardados.resistencia;

        // Pone el valor inicial de la barra de resistencia
        ActualizarBarraResistencia();

        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // Corre mientras el jugador pulse la tecla Shift
        // estaCorriendo = Input.GetKey(KeyCode.LeftShift);

        // Usar resistencia al correr
        if (estaCorriendo && resistenciaActual > 0)
        {
            controlJugador.velocidadMovimiento = velocidadCorrer;
            UsarResistencia(usoResistenciaCorrer * Time.deltaTime);
        }
        else // Regenerar resistencia cuando no se está corriendo
        {
            controlJugador.velocidadMovimiento = velocidadAndar;
            RegenerarResistencia(regeneracionResistencia * Time.deltaTime);
        }
  
        if (resistenciaActual <= 0)
            estaCorriendo = false;

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
        if (playerInput.actions["Correr"].WasPressedThisFrame())
        {
            estaCorriendo = true;
        }
        if (playerInput.actions["Correr"].WasReleasedThisFrame())
        {
            estaCorriendo = false;
        }
    }
}
