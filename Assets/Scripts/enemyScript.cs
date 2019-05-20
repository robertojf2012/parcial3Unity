using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    //Referencia al pool de enemigos
    public SimpleObjectPool enemyPool; 
    
    //Referencia al pool de balas
    public SimpleObjectPool cartucho;

    //Referencia a la posición del jugador
    public Transform playerPosition;

    //Referencia a la posición de la mira, donde será lanzada la bala
    public Transform cannon;

    //Referencia al body del enemigo, que es el que rotará a la hora de mirar al jugador
    public Transform body;

    //Indica si el enemigo está vivo o no
    public bool alive = true;


    /// <summary>
    /// Se encarga de generar al enemigo mismo en la posición definida.
    /// </summary>
    /// <param name="tracker">Posición donde será generado el enemigo</param>
    /// <param name="enemyPool">Pool de donde proviene el enemigo (pool de enemigos)</param>
    /// <param name="playerPosition">Posición del player, para poder empezar a disparar hacia el desde el inicio</param>
    public void generateMe(Transform tracker, SimpleObjectPool enemyPool, Transform playerPosition)
    {
        //Asignamos la posición del jugador y el pool de enemigos
        this.playerPosition = playerPosition;
        this.enemyPool = enemyPool;
        
        //El enemigo está vivo
        alive = true;

        //Hacemos al enemigo hijo de la imagen que se usa como track de vuforia
        gameObject.transform.SetParent(tracker);

        //Rescalamos el tamaño del enemigo
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        //Asignamos la posición y rotación en la que se encontrará el enemigo
        transform.position = new Vector3(tracker.position.x, tracker.position.y + 2, tracker.position.z);
        transform.rotation = tracker.rotation;

        //Iniciamos rutina que se encarga de disparar al jugador cada cierto tiempo
        StartCoroutine(waitToShootAgain(playerPosition));
    }

    /// <summary>
    /// Verifica si se hace colisión con otro objeto en la escena
    /// si hay colisión, cambiamos el color del enemigo, habilitamos su gravedad y rotación, se inicia la rutina que se encarga de
    /// devolver el enemigo al pool de enemigos, y cambiamos la variable alive a FALSE, para prevenir que siga disparando al jugador.
    /// </summary>
    /// <param name="collision">Objeto con el que se hizo colisión</param>
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "bullet")
        //{
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.grey;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            StartCoroutine(waitToReturn());
            alive = false;
        //}
    }

    /// <summary>
    /// Función que se encarga de disparar al jugador cada cierto tiempo si el enemigo está vivo, en un rango de 2 a 5 segundos
    /// </summary>
    /// <param name="playerPosition">Posición actual del jugador</param>
    /// <returns></returns>
    public IEnumerator waitToShootAgain(Transform playerPosition)
    {
        int seconds = Random.Range(2, 5);
        yield return new WaitForSeconds(seconds);
        if (alive)
        {
            shoot(playerPosition);
            StartCoroutine(waitToShootAgain(playerPosition));
        }
    }

    /// <summary>
    /// Se encarga de regresar al enemigo al pool de enemigos después de 5 segundos de haber muerto.
    /// Después de los 5 segundos... restablecemos sus propiedades originales de (velocidad,gravedad,rotación)
    /// y finalmente lo devolvemos al pool de enemigos
    /// </summary>
    /// <returns></returns>
    IEnumerator waitToReturn()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        enemyPool.ReturnObject(gameObject);
    }

    /// <summary>
    /// Se encarga de rotar al enemigo de tal forma que apunte al jugador, para poder disparar
    /// </summary>
    /// <param name="target">Dirección a mirar o apuntar</param>
    public void aim(Transform target)
    {
        body.transform.LookAt(target);       
    }


    /// <summary>
    /// Se encarga de disparar al jugador, sacamos la bala del pool de balas y la disparamos con la función shoot del script de la bala
    /// </summary>
    /// <param name="playerPosition">Posición del jugador</param>
    public void shoot(Transform playerPosition)
    {
        aim(playerPosition);
        GameObject newBullet = cartucho.GetObject();
        newBullet.GetComponent<bulletScript>().shoot(cannon, cartucho);
    }
}
