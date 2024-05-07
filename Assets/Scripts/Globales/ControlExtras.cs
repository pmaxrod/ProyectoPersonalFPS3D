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
    public string id;
    public TipoExtra tipo;
    public int cantidad;

    private Objeto objeto;
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
            ControlJuego.instancia.objetos.RemoveAll(x => id == x.id);
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        /*if (!ControlJuego.instancia.objetos.Contains(gameObject))
        {
            Destroy(gameObject);
        }*/
        /*Objeto objeto = new Objeto(gameObject.GetInstanceID().ToString(), gameObject.transform.position, gameObject.transform.rotation);
        ControlJuego.instancia.objetos.Add(objeto);

        if(!ArchivosGuardados.instance.datosGuardados.objetos.Contains(objeto))
            Destroy(gameObject);
        else{
            Objeto objetoLista = ArchivosGuardados.instance.datosGuardados.objetos.Find(objeto.id);
            //gameObject.transform.position = ob
        }*/
        if (!ArchivosGuardados.instance.archivoCargado)
        {
            objeto = new Objeto(id, transform.position, transform.rotation);
        }
        else
        {
            objeto = ArchivosGuardados.instance.datosGuardados.objetos.Find(x => x.id.Equals(id));
        }

        ControlJuego.instancia.InstanciarObjetoJuego(objeto, this.gameObject);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
