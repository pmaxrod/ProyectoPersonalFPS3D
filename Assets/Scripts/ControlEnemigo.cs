using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class ControlEnemigo : MonoBehaviour
{
    [Header("Estad�sticas")]
    public int vidasActual;
    public int vidasMax;
    public int puntuacionEnemigo;

    [Header("Movimiento")]
    public float velocidad;
    public float rangoAtaque;
    public float rangoPerseguir;
    public float yPathOffset;
    public bool siemprePersigue;

    private List<Vector3> listaCaminos;

    private ControlArma arma;
    private ControlJugadorIS objetivo;
    private Objeto objeto;

    // Start is called before the first frame update
    void Start()
    {
        arma = GetComponent<ControlArma>();
        objetivo = FindObjectOfType<ControlJugadorIS>();
        //Cada medio segundo repite el c�lculo de la lista de caminos
        InvokeRepeating("ActualizarCaminos", 0.0f, 0.5f);

        /*if (!ControlJuego.instancia.objetos.Contains(gameObject))
        {
            Destroy(gameObject);
        }*/
        if (!ArchivosGuardados.instance.archivoCargado)
        {
            objeto = new Objeto(gameObject.GetInstanceID().ToString(), gameObject.transform.position, gameObject.transform.rotation);
        }
        else
        {
            objeto = ArchivosGuardados.instance.datosGuardados.objetos.Find(x => x.id.Equals(gameObject.GetInstanceID().ToString()));
        }

        ControlJuego.instance.InstanciarObjetoJuego(objeto);
    }

    // Update is called once per frame
    void Update()
    {
        // Distancia entre enemigo y jugador
        float distancia = Vector3.Distance(transform.position,
            objetivo.transform.position);

        // Hasta cuando puede perseguir antes de disparar
        if (distancia > rangoAtaque)
            if (siemprePersigue)
                PerseguirObjetivo();
            else if (distancia < rangoPerseguir)
                PerseguirObjetivo();
        // Si no, se para y dispara
        if (distancia <= rangoAtaque)
        {
            if (arma.PuedeDisparar())
                arma.Disparar();
        }
        // Rota al enemigo para que dispare en direcci�n al jugador
        Vector3 direccion = (objetivo.transform.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angulo;


    }

    public void QuitarVidasEnemigo(int cantidad)
    {
        vidasActual -= cantidad;

        int puntuacion = puntuacionEnemigo * ControlJugadorIS.instance.vidasActual * (int)(ControlJuego.instance.tiempoJugado);

        ControlJuego.instance.PonerPuntuacion(puntuacion);

        if (vidasActual <= 0)
        {
            ControlJuego.instance.objetos.Remove(objeto);
            Destroy(gameObject);
        }
    }

    private void PerseguirObjetivo()
    {
        if (listaCaminos.Count == 0)
            return;
        transform.position = Vector3.MoveTowards(transform.position,
            listaCaminos[0] + new Vector3(0, yPathOffset, 0), velocidad
            * Time.deltaTime);
        if (transform.position == listaCaminos[0] + new Vector3(0, yPathOffset, 0))
            listaCaminos.RemoveAt(0);

    }

    void ActualizarCaminos()
    {
        NavMeshPath caminoCalculado = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, objetivo.transform.position,
            NavMesh.AllAreas, caminoCalculado);
        listaCaminos = caminoCalculado.corners.ToList();
    }
}
