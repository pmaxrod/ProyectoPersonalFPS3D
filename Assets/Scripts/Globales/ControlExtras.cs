using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public enum TipoExtra
{
    Vida,
    Bolas
}

public class ControlExtras : MonoBehaviour
{
    public TipoExtra tipo;
    public int cantidad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControlJugadorIS jugador = other.GetComponent<ControlJugadorIS>();
            switch (tipo)
            {
                case TipoExtra.Vida:
                    jugador.IncrementaVida(cantidad);
                    break;
                case TipoExtra.Bolas:
                    jugador.IncrementaNumBolas(cantidad);
                    break;
            }
			ControlJuego.instancia.objetos.Remove(gameObject);
            Destroy(gameObject);
        }
            
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!ControlJuego.instancia.objetos.Contains(gameObject))
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
