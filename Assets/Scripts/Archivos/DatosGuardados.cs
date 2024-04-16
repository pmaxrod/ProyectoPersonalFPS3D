using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados
{
    public double tiempoJugado;
    public int puntuacion;

    public int municion;
    public Vector3 posicion;
    public Quaternion rotacion;

    public DatosGuardados()
    {
        this.tiempoJugado = 0;
        this.puntuacion = 0;

        this.municion = Constantes.MUNICION_INICIAL;
        this.posicion = Constantes.POSICION_INICIAL;
        this.rotacion = new Quaternion();
    }

    public DatosGuardados(double tiempoJugado, int puntuacion)
    {
        this.tiempoJugado = tiempoJugado;
        this.puntuacion = puntuacion;

        this.municion = 0;
        this.posicion = new Vector3(0, 0, 0);
    }
    public DatosGuardados(double tiempoJugado, int puntuacion, int municion, Vector3 posicion, Quaternion rotacion)
    {
        this.tiempoJugado = tiempoJugado;
        this.puntuacion = puntuacion;
        this.municion = municion;
        this.posicion = posicion;
        this.rotacion = rotacion;
    }

    public string TiempoFormateado(double tiempoJugado)
    {
        TimeSpan ts = TimeSpan.FromSeconds(tiempoJugado);

        return String.Format("{0:D2}:{1:D2}:{2:D2}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
    }
}
