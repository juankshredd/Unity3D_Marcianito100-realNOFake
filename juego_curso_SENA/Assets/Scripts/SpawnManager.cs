using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public GameObject human1;
    private Vector3 spawnPos; 
    private Vector3 spawnPosPUs;
    private float startDelay = 2;
    private float repeatRate = 1.5f;
    private float startPU1 = 10;
    private float repeatPU1 = 7.5f;
    private float startPU2 = 20;
    private float repeatPU2 = 7.5f;
    private float startPU3 = 30;
    private float repeatPU3 = 7.5f;
    private float startHuman = 3.5f;
    private float repeatHuman = 25.5f;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // spawn de enemigos y de powerups
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
        InvokeRepeating("SpawnPowerUp", startPU1, repeatPU1);
        InvokeRepeating("SpawnPowerUp2", startPU2, repeatPU2);
        InvokeRepeating("SpawnPowerUp3", startPU3, repeatPU3);
        InvokeRepeating("SpawnHuman", startHuman, repeatHuman);
    }

    // Update is called once per frame
    void Update()

        
    {
      spawnPos = new Vector3(Random.Range(-9.5f, 9.5f), 0, 60);
        spawnPosPUs  = new Vector3(Random.Range(-9.5f, 9.5f), 2, 60);

       

        if(gameManager.gameOver == true)
        {
            //termina metodos de spawn y termina el juego
        }
    }

    
    //metodos 
    private void SpawnEnemy()
    {
        if(gameManager.gameOver == false)
        {
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
      
    }

    private void SpawnPowerUp()
    {
        if(gameManager.gameOver == false)
        {
            Instantiate(powerUp1, spawnPosPUs, powerUp1.transform.rotation);
        }
       
    }

    private void SpawnPowerUp2()
    {
        if(gameManager.gameOver == false)
        {
            Instantiate(powerUp2, spawnPosPUs, powerUp2.transform.rotation);
        }
        
    }
    private void SpawnPowerUp3()
    {
        if(gameManager.gameOver == false)
        {
            Instantiate(powerUp3, spawnPosPUs, powerUp3.transform.rotation);
        }
        
    }

    private void SpawnHuman()

    {
        if(gameManager.gameOver == false)
        {
            Instantiate(human1, spawnPos, human1.transform.rotation);
        }
       
    }
   
}
