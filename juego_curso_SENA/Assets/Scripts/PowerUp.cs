using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float speed = 15;

    public int powerUpID;
    private GameManager gameManager;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        //movimiento del icono en el eje Z
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //si la posicion es menor a -10
        if (transform.position.z < -10)

            //se destruye este objeto
        {
            Destroy(this.gameObject);
        }

        if(gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // revisa si la etiqueta es Player
        if(other.tag == "Player")
        {
            //toma el componente Script para comunicarse entre scripts
            PlayerController player = other.GetComponent<PlayerController>();

            //si player no es nulo entonces...
            if(player != null)
            {
                // activa proyectil pizza si el ID de powerup es 0
                if(powerUpID == 0)
                {
                    player.PizzaShotOn();
                }
               

                // activa powerup velocidad si el ID es 1
                if  (powerUpID == 1)
                {
                    player.BananaSpeedOn();
                }

                // activa powerup escudo si el ID es 2
               if(powerUpID == 2)
                {
                    player.TankShieldOn();
                }

               
            }
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
       
    }
}
