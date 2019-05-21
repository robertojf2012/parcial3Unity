using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    //Referencia al pool de balas
    public SimpleObjectPool cartucho;

    //Referencia a la mira del arma
    public Transform cannon;

    //Referencia al objeto que cuenta los disparos recibidos
    public contadorScript disparo;

    //Referencia al objeto del arma del jugador
    public Rigidbody arma;

    //Indica si el jugador puede disparar o no
    private bool enableShoot = true;

    void Start()
    {
        //Al iniciar, la arma no tiene ninguna colisión
        arma.GetComponentInChildren<BoxCollider>().isTrigger = true;
    }

    /// <summary>
    /// Verifica si hay colisión con un objeto
    /// </summary>
    /// <param name="collision">Objeto con el que se hizo colisión</param>
    private void OnCollisionEnter(Collision collision)
    {
        //Revisamos si el jugador ha recibido un disparo del enemigo...
        if (collision.gameObject.tag == "bulletEnemy")
        {
            //Aumentamos el contador de disparos recibidos
            disparo.setDisparo();

            //Verificamos si el contador ha recibido 10 o más disparos...
            //Si es verdadero, cambiamos el color del jugaor, hacemos que su arma caiga al suelo y desabilitamos que pueda disparar
            if (disparo.getDisparosRecibidos() >= 10)
            {
                gameObject.GetComponentInChildren<Renderer>().material.color = Color.cyan;
                arma.GetComponentInChildren<Rigidbody>().useGravity = true;
                arma.GetComponentInChildren<BoxCollider>().isTrigger = false;
                enableShoot = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Controles para mover al jugador (teclado)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enableShoot == true)
            {
                shoot();
            } 
        }
    }

    /// <summary>
    /// Devuelve true/false, dependiendo si el jugador puede disparar o no
    /// </summary>
    /// <returns></returns>
    public bool getEnableshoot()
    {
        return enableShoot;
    }

    /// <summary>
    /// Se encarga de disparar una bala
    /// </summary>
    public void shoot()
    {
        GameObject newBullet = cartucho.GetObject();
        newBullet.GetComponent<bulletScript>().shoot(cannon, cartucho);
    }
}
