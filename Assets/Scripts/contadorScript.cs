using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contadorScript : MonoBehaviour
{
    private int disparosRecibidos = 0;
    
    void Start()
    {
        
    }

    public int getDisparosRecibidos()
    {
        return disparosRecibidos;
    }

    public void setDisparo()
    {
        disparosRecibidos = disparosRecibidos + 1;
    }
}
