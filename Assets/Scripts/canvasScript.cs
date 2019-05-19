using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    public GameObject player;
    public Text textDisparos;
    public GameObject disparos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (textDisparos != null)
        {
            textDisparos.text = "Daño: " + disparos.GetComponent<contadorScript>().getDisparosRecibidos();
        }
    }

    public void shootButton()
    {
        if (player.GetComponent<playerScript>().getEnableshoot() == true)
        {
            player.GetComponent<playerScript>().shoot();
        }
    }

    public void right()
    {
        player.GetComponent<Transform>().Rotate(Vector3.up, 90 * Time.deltaTime);
    }

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
