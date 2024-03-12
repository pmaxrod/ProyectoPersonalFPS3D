using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugadorIS : MonoBehaviour
{
    [Header("Vidas")]
    public int vidasActual;
    public int vidasMax;

    [Header("Movimiento")]
    public float velocidadMovimiento = 10.0f;
    public float velocidadRotacion = 1.0f;
    public float fuerzaSalto = 5.0f;
    [Header("Camara")]
    public float sensibilidadRaton = 3.0f;
    public float maxVistaX = 45.0f;
    public float minVistaX = -45.0f;
    private float rotacionX = 0.0f;

    private Camera camara;
    private Rigidbody fisica;
    private CapsuleCollider col;

    private Vector2 movimientoInput;
    private Vector2 rotacionInput;

    private bool puedeSaltar = true;

    private ControlArma arma;
    private PlayerInput playerInput;
    private int numeroSaltos = 2;

    private void Awake()
    {
        camara = Camera.main;
        fisica = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        arma = GetComponent<ControlArma>();
        Cursor.lockState = CursorLockMode.Locked; // Oculta el cursor
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        ControlHUD.instancia.ActualizarNumBolasTexto(arma.municionActual, arma.municionMax);
        ControlHUD.instancia.ActualizaBarraVida(vidasActual, vidasMax);
        ControlHUD.instancia.ActualizarPuntuacion(0);
    }

    // Update is called once per frame
    void Update()
    {
        // if (ControlJuego.instancia.juegoPausado) return;
    }

    void FixedUpdate()
    {
        if (ControlJuego.instancia.juegoPausado) return;

        // Obtener la dirección de movimiento
        Vector3 movimiento = transform.forward * movimientoInput.y + transform.right * movimientoInput.x;

        fisica.AddForce(movimiento * velocidadMovimiento);

        // Rotar el personaje en función de la entrada de rotación
        transform.Rotate(Vector3.up * rotacionInput.x * velocidadRotacion);

        // Rotar la cámara en función de la entrada de rotación vertical
        rotacionX += rotacionInput.y * velocidadRotacion;
        rotacionX = Mathf.Clamp(rotacionX, minVistaX, maxVistaX);
        camara.transform.localRotation = Quaternion.Euler(-rotacionX, 0, 0);

        if (arma.municionActual > arma.municionMax)
        {
            IncrementaNumBolas(arma.municionMax - arma.municionActual);
        }

        if (PuedeSaltar())
        {
            puedeSaltar = true;
            numeroSaltos = 2;
        }
    }


    void OnMove(InputValue value)
    {
        movimientoInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        rotacionInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        /* if (PuedeSaltar())
         {
             // Aplicar fuerza vertical para simular un salto
             fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
             puedeSaltar = false;
         }*/
        // Aplicar fuerza vertical para simular un salto
        if (puedeSaltar)
        {
            fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            numeroSaltos--;
        }
        if (numeroSaltos < 1)
        {
            puedeSaltar = false;
        }
        Debug.Log(numeroSaltos.ToString());
    }

    void OnFire()
    {
        Debug.Log("Is Pressed: " + playerInput.actions["Fire"].IsPressed());
        Debug.Log("Was Pressed: " + playerInput.actions["Fire"].WasPressedThisFrame());
        Debug.Log("Was Released: " + playerInput.actions["Fire"].WasReleasedThisFrame());
        if (arma.PuedeDisparar())
            arma.Disparar();

    }
    void OnRecargar()
    {
        if (arma.municionActual < arma.municionMax)
        {
            IncrementaNumBolas(arma.municionMax / 4);
        }
    }

    private bool PuedeSaltar()
    {
        //Test that we are grounded by drawing an invisible line(raycast)
        //If this hits a solid object e.g. floor then we are grounded.
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }

    /*    void OnCollisionEnter(Collision collision)
        {
            // Verificar si el personaje ha tocado el suelo
            if (collision.gameObject.CompareTag("Suelo"))
            {
                puedeSaltar = true;
            }
        }
    */

    public void QuitarVidasJugador(int cantidad)
    {
        vidasActual -= cantidad;

        ControlHUD.instancia.ActualizaBarraVida(vidasActual, vidasMax);

        if (vidasActual <= 0)
            TerminaJugador();
    }

    private void TerminaJugador()
    {
        Debug.Log("GAME OVER!!!");
        ControlHUD.instancia.EstablecerVentanaFinJuego(false);
    }

    public void IncrementaVida(int cantidadVida)
    {
        vidasActual = Mathf.Clamp(vidasActual + cantidadVida, 0, vidasMax);
        ControlHUD.instancia.ActualizaBarraVida(vidasActual, vidasMax);
    }

    public void IncrementaNumBolas(int cantidadBolas)
    {
        arma.municionActual = Mathf.Clamp(arma.municionActual + cantidadBolas, 0, arma.municionMax);
        ControlHUD.instancia.ActualizarNumBolasTexto(arma.municionActual, arma.municionMax);
    }
}
