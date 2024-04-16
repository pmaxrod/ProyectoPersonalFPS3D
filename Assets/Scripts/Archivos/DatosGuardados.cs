using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados
{
    public double tiempoJugadoTotal;
    public int puntuacion;

    public int municion;
    public Vector3 posicion;
    public Quaternion rotacion;
    public double tiempoJugadoPartida;

    public DatosGuardados()
    {
        this.tiempoJugadoTotal = 0;
        this.puntuacion = 0;

        this.municion = Constantes.MUNICION_INICIAL;
        this.posicion = Constantes.POSICION_INICIAL;
        this.rotacion = new Quaternion();
        this.tiempoJugadoPartida = 0;
    }

    public DatosGuardados(double tiempoJugadoTotal, int puntuacion)
    {
        this.tiempoJugadoTotal = tiempoJugadoTotal;
        this.puntuacion = puntuacion;

        this.municion = 0;
        this.posicion = new Vector3(0, 0, 0);
        this.rotacion = new Quaternion();
        this.tiempoJugadoPartida = 0;
    }
    public DatosGuardados(double tiempoJugadoTotal, int puntuacion, int municion, Vector3 posicion, Quaternion rotacion)
    {
        this.tiempoJugadoTotal = tiempoJugadoTotal;
        this.puntuacion = puntuacion;
        this.municion = municion;
        this.posicion = posicion;
        this.rotacion = rotacion;
        this.tiempoJugadoPartida = 0;
    }

    public string TiempoFormateado(double tiempoJugadoTotal)
    {
        TimeSpan ts = TimeSpan.FromSeconds(tiempoJugadoTotal);

        return String.Format("{0:D2}:{1:D2}:{2:D2}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
    }
}
