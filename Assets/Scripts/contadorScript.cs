using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contadorScript : MonoBehaviour
{
    private int disparosRecibidos = 0;

    /// <summary>
    /// Se encarga de devolver el numero de disparos recibidos que ha recibido el jugador
    /// </summary>
    /// <returns> (int) disparos recibidos</returns>
    public int getDisparosRecibidos()
    {
        return disparosRecibidos;
    }

    /// <summary>
    /// Agrega un disparo más al contador
    /// </summary>
    public void setDisparo()
    {
        disparosRecibidos = disparosRecibidos + 1;
    }
}
