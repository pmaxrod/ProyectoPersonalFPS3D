using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjetos : MonoBehaviour
{
    public GameObject objetoPrefab;
    public int numObjetosOnStart;

    private List<GameObject> objetosPooled = new List<GameObject>();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numObjetosOnStart; i++)
        {
            CrearNuevoObjeto();
        }
    }

    private GameObject CrearNuevoObjeto()
    {
        GameObject objeto = Instantiate(objetoPrefab);
        objeto.SetActive(false);
        objetosPooled.Add(objeto);
        return objeto;
    }

    public GameObject getObjeto()
    {
        GameObject objeto = objetosPooled.Find(x => x.activeInHierarchy == false);
        if(objeto == null)
        {
            objeto = CrearNuevoObjeto();
        }
        objeto.SetActive(true);
        return objeto;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
