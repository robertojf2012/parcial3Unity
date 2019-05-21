using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GENERATOR : MonoBehaviour
{
    //Referencia al pool de enemigos
    public SimpleObjectPool enemyPool;
    
    //Referencias a las posiciones donde será generado un enemigo
    public Transform generatorPosition;
    public Transform generatorPosition2;

    //Referencia a la posición del jugador
    public Transform playerPosition;
    
    void Start()
    {
        //Al inicio generamos un enemigo en sus posiciones respectivas
        generateEnemy(generatorPosition);
        generateEnemy(generatorPosition2);
    }
    
    /// <summary>
    /// Se encarga de generar un enemigo en la posición indicada
    /// El enemigo es obtenido del pool de enemigos
    /// </summary>
    /// <param name="positionToGenerate">Posición a generar</param>
    public void generateEnemy(Transform positionToGenerate)
    {
        positionToGenerate.position = new Vector3(Random.Range(-0.60f, 0.45f), positionToGenerate.position.y , positionToGenerate.position.z + Random.Range(0,0.10f));
        GameObject enemy = enemyPool.GetObject();
        enemy.GetComponent<enemyScript>().generateMe(positionToGenerate, enemyPool, playerPosition);
    }

    void Update()
    {
        //Validamos si existen enemigos vivos en las posiciones donde se generaron..
        //Si no hay.. regeneramos nuevos enemigos
        if (generatorPosition.transform.childCount == 0)
        {
            generateEnemy(generatorPosition);
        }

        if (generatorPosition2.transform.childCount == 0)
        {
            generateEnemy(generatorPosition2);
        }
    }
}
