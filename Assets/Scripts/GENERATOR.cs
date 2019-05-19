using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GENERATOR : MonoBehaviour
{
    public SimpleObjectPool enemyPool;
    public Transform generatorPosition;
    public Transform generatorPosition2;
    public Transform playerPosition;

    void Start()
    {
        generateEnemy(generatorPosition);
        generateEnemy(generatorPosition2);
    }
    
    public void generateEnemy(Transform positionToGenerate)
    {
        GameObject enemy = enemyPool.GetObject();
        enemy.GetComponent<enemyScript>().generateMe(positionToGenerate, enemyPool, playerPosition);
    }

    void Update()
    {
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
