using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private float speed = 16;
    private GameManager gameManager;
    private GameObject player;
    private UIManager uimanager;
    [SerializeField]
    private AudioClip audioClip;
   

    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uimanager.UpdateScoreHuman();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }

        if(other.CompareTag("Bone"))
        {
            uimanager.UpdateScore();
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Pizza"))
        {
            uimanager.UpdateScore();
            Destroy(other.gameObject);
        }
    }

}
