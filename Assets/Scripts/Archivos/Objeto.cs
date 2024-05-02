using UnityEngine;

[System.Serializable]
public class Objeto{

    public string id;
    public float px, py, pz;
    public float rx, ry, rz, rw;

    public Objeto(string id, Vector3 posicion, Quaternion rotacion){
        this.id = id;

        px = posicion.x;
        py = posicion.y;
        pz = posicion.z;

        rx = rotacion.x;
        ry = rotacion.y;
        rz = rotacion.z;
        rw = rotacion.w;

    }
    public Vector3 GetPosicion(){
        return new Vector3(px, py, pz);
    }

    public Quaternion GetRotacion(){
        return new Quaternion(rx, ry, rz, rw);
    }
}
