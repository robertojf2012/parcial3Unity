using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    //Referencia al pool de balas
    public SimpleObjectPool bulletPool;

    /// <summary>
    /// Se encarga de disparar la bala como tal, se le indica la dirección y velocidad a donde irá disparada
    /// De igual forma se hace referencia al pool de donde salió y se ejecuta la rutina del tiempo que le tomará regresar
    /// al pool de balas nuevamente
    /// </summary>
    /// <param name="pointer">Dirección a donde irá disparada la bala</param>
    /// <param name="bulletPool">Pool de balas donde de encuentra</param>
    public void shoot(Transform pointer, SimpleObjectPool bulletPool)
    {
        this.bulletPool = bulletPool;
        gameObject.transform.SetParent(null);
        transform.position = pointer.position;
        transform.rotation = pointer.rotation;
        GetComponent<Rigidbody>().velocity = pointer.transform.forward * 10;
        StartCoroutine(waitToReturn());
    }

    /// <summary>
    /// Función que se encarga de regresar la bala al pool después de 5 segundos de haber sido disparada
    /// </summary>
    /// <returns></returns>
    IEnumerator waitToReturn()
    {
        yield return new WaitForSeconds(5);
        bulletPool.ReturnObject(gameObject);
    }
}
