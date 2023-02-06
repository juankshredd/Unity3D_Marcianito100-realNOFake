using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject explosionPrefab;
    private UIManager uiManager;
    private GameManager gameManager;
    private AudioSource audioSource;
    public float speed;
    private float moveRate = 0.1f;
    private float delay = 0.2f;
    private float xLimit = 9.5f;
    [SerializeField]
    private AudioClip audioClip;


    private void Awake()
    {
        InvokeRepeating("SideMove", delay, moveRate);
    }


    // Start is called before the first frame update
    void Start()
    {
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
                 
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z < -10)
        {
            float xRange = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(xRange, transform.position.y, 60);
        }

        if(transform.position.x < -xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
        }

        if(transform.position.x > xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
        }

        if (gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void SideMove()
    {
        transform.Translate(Vector3.right * Random.Range(-4f, 4f) * speed * Time.deltaTime); 
    }

  

    private void OnTriggerEnter(Collider other)
    {
        

        if(other.CompareTag( "Bone"))
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
            
        }

        if(other.CompareTag( "Pizza"))
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }

        else if (other.CompareTag ("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {               
                player.Damage();
            }
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }

       
    }
}
