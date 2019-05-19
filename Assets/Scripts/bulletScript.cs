using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public SimpleObjectPool bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(Transform pointer, SimpleObjectPool bulletPool)
    {
        this.bulletPool = bulletPool;
        gameObject.transform.SetParent(null);
        transform.position = pointer.position;
        transform.rotation = pointer.rotation;
        GetComponent<Rigidbody>().velocity = pointer.transform.forward * 10;
        StartCoroutine(waitToReturn());
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "enemy")
    //    {
    //        bulletPool.ReturnObject(collision.gameObject);
    //    }
    //}

    IEnumerator waitToReturn()
    {
        yield return new WaitForSeconds(5);
        bulletPool.ReturnObject(gameObject);
    }
}
