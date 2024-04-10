using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados
{
    public double tiempoJugado;
    // public int municion;
	public int puntuacion;


    public DatosGuardados(double tiempoJugado, int puntuacion)
    {
        this.tiempoJugado = tiempoJugado;
        this.puntuacion = puntuacion;
    }

    public string TiempoFormateado(double tiempoJugado)
    {
        TimeSpan ts = TimeSpan.FromSeconds(tiempoJugado);

        return String.Format("{0}:{1}:{2}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
    }
}
