using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    public GameObject player; //Referencia al jugador
    public Text textDisparos; //Referencia al Text de la UI
    public GameObject disparos; //Referencia al objeto del contador de disparos


    void Update()
    {
        //Revisamos que la referencia al texto de disparos exista primero, antes de asignar el valor de disparos recibidos al player
        if (textDisparos != null)
        {
            textDisparos.text = "Daño: " + disparos.GetComponent<contadorScript>().getDisparosRecibidos();
        }
    }

    /// <summary>
    /// Se encarga de ejecutar la función disparar del player, al precionar el boton de la UI shoot
    /// </summary>
    public void shootButton()
    {
        if (player.GetComponent<playerScript>().getEnableshoot() == true)
        {
            player.GetComponent<playerScript>().shoot();
        }
    }

    /// <summary>
    /// Se encarga de rotar al player a la derecha al precionar el boton de la UI right
    /// </summary>
    public void right()
    {
        player.GetComponent<Transform>().Rotate(Vector3.up, 90 * Time.deltaTime);
    }

    /// <summary>
    /// Se encarga de rotar al player a la izquierda al precionar el boton de la UI left
    /// </summary>
    public void left()
    {
        player.GetComponent<Transform>().Rotate(Vector3.up, -90 * Time.deltaTime);
    }


    public void startGame()
    {
        //StartCoroutine(enemigo.GetComponent<enemyScript>().waitToShootAgain());
        //StartCoroutine(enemigo2.GetComponent<enemyScript>().waitToShootAgain());
    }
}
