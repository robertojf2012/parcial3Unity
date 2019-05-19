using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public SimpleObjectPool cartucho;
    public Transform cannon;
    public contadorScript disparo;
    public Rigidbody arma;
    private bool enableShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bulletEnemy")
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.grey;

            disparo.setDisparo();

            if (disparo.getDisparosRecibidos() >= 10)
            {
                gameObject.GetComponentInChildren<Renderer>().material.color = Color.cyan;
                arma.GetComponentInChildren<Rigidbody>().useGravity = true;
                enableShoot = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    public bool getEnableshoot()
    {
        return enableShoot;
    }

    public void shoot()
    {
        GameObject newBullet = cartucho.GetObject();
        newBullet.GetComponent<bulletScript>().shoot(cannon, cartucho);
    }
}
