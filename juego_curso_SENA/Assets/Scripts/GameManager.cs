using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
   
    private UIManager uiManager;

    public void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }



    public void Update()
    {
        if(gameOver == true)
        {
           if(Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                uiManager.HideTitleScreen();
            }
        }

       
    }


}
