using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public SimpleObjectPool enemyPool;
    public SimpleObjectPool cartucho;

    public Transform playerPosition;
    public Transform cannon;
    public Transform body;
    public bool alive = true;

    void Start()
    {
    }

    public void generateMe(Transform tracker, SimpleObjectPool enemyPool, Transform playerPosition)
    {
        this.playerPosition = playerPosition;
        this.enemyPool = enemyPool;
        alive = true;
        gameObject.transform.SetParent(tracker);
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        transform.position = new Vector3(tracker.position.x, tracker.position.y + 2, tracker.position.z);
        transform.rotation = tracker.rotation;
        StartCoroutine(waitToShootAgain(playerPosition));
    }

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

    IEnumerator waitToReturn()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        enemyPool.ReturnObject(gameObject);
    }

    public void aim(Transform target)
    {
        body.transform.LookAt(target);       
    }
    public void shoot(Transform playerPosition)
    {
        aim(playerPosition);
        GameObject newBullet = cartucho.GetObject();
        newBullet.GetComponent<bulletScript>().shoot(cannon, cartucho);
    }
}
