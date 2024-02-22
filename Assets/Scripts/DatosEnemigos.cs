using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevos Datos Enemigos", menuName = "Enemigo/Datos", order = 0)]
public class DatosEnemigos : ScriptableObject
{
    [SerializeField] private string nombreEnemigo;
    [SerializeField] private string descripcion;
    [SerializeField] private float velocidad;
    [SerializeField] private float frecuenciaDisparo;
    [SerializeField] private Material materialEnemigo;
    [SerializeField] private int vidaMaxima;

    public float Velocidad { get => velocidad; }
    public float FrecuenciaDisparo { get => frecuenciaDisparo; }
    public Material MaterialEnemigo { get => materialEnemigo; }
    public int VidaMaxima { get => vidaMaxima; }
}
