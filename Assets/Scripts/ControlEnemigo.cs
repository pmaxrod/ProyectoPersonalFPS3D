using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class ControlEnemigo : MonoBehaviour
{
    [Header("Estadísticas")]
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

    // Start is called before the first frame update
    void Start()
    {
        arma = GetComponent<ControlArma>();
        objetivo = FindObjectOfType<ControlJugadorIS>();
        //Cada medio segundo repite el cálculo de la lista de caminos
        InvokeRepeating("ActualizarCaminos", 0.0f, 0.5f);

        if (!ControlJuego.instancia.enemigos.Contains(gameObject))
        {
            Destroy(gameObject);
        }
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
        // Rota al enemigo para que dispare en dirección al jugador
        Vector3 direccion = (objetivo.transform.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angulo;


    }

    public void QuitarVidasEnemigo(int cantidad)
    {
        vidasActual -= cantidad;

        int puntuacion = puntuacionEnemigo * ControlJugadorIS.instance.vidasActual * (int)(ControlJuego.instancia.tiempoJugado);

        ControlJuego.instancia.PonerPuntuacion(puntuacion);

        if (vidasActual <= 0)
        {
            ControlJuego.instancia.enemigos.Remove(gameObject);
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
