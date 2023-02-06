using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives = 2;
    public float speed;
    public float superSpeed = 20.5f;
    
    public float xRange = 9.5f;
    public float zRange = 1.5f;
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public GameObject BonePrefab;
    public GameObject pizaPrefab;
    private float fireRate = 0.7f;
    private float nextFire = 0.0f;
    public bool pizzaShot = false;
    public bool bananaSpeed = false;
    public bool tankShield = false;

    public GameObject explosionPlayer;
    public GameObject escudoPlayer;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private AudioClip audioClip1;
    [SerializeField]
    AudioClip audioClip2;
    private UIManager uiManager;
    private GameManager gameManager;
    public Animator animator;
    private float x, y;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
     
        if(uiManager != null)
        {
            uiManager.UpdateLives(lives);
        }
        //toma componente RBoby
        playerRb = GetComponent<Rigidbody>();
        //toma las fisicas de player en el inspector para modificar
        Physics.gravity *= gravityModifier;
        animator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
        Shoot();
        Movement();

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        Dance();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }

    private void Shoot()
    {
        //si oprime B o boton mouse izquierdo 
        if (Input.GetKeyDown(KeyCode.B))
        {
            //si el tiempo de juego es mayor a firerate
            if (Time.time > fireRate)
            {
                //si pizzashot es verdadero
                if(pizzaShot == true)
                {
                    
                    Instantiate(pizaPrefab, transform.position + new Vector3(0, 0.92f, 1.5f), Quaternion.identity);
                    
                }
                //si pizzashot es falso
                if(pizzaShot == false)
                {
                    //dispara huesos
                    Instantiate(BonePrefab, transform.position + new Vector3(0, 0.92f, 0.38f), Quaternion.identity);
                    
                }
                // cadencia de tiro
                
            }

            nextFire = Time.time + fireRate;
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
    }

    private void Movement()
    {
        //controladores horizontales flechas o A y D
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (bananaSpeed == true)
        {
            // se mueve 1.5x mas rapido
            transform.Translate(Vector3.right * x * Time.deltaTime * superSpeed);
            transform.Translate(Vector3.forward * y * Time.deltaTime * superSpeed);
        }

        if( tankShield == true)
        {
            transform.Translate(Vector3.right * x * Time.deltaTime * speed);
            transform.Translate(Vector3.forward * y * Time.deltaTime * speed);

        }

        else if(bananaSpeed == false)
        {
            //mueven a player en el eje X
            transform.Translate(Vector3.right * x * Time.deltaTime * speed);
            transform.Translate(Vector3.forward * y * Time.deltaTime * speed);

        }


        //restriccion en eje X
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);

        }

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);

        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3( transform.position.x, transform.position.y, zRange);

        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3( transform.position.x, transform.position.y, -zRange);

        }


        //anti spam para el salto
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.Play("Jump");
            AudioSource.PlayClipAtPoint(audioClip1, Camera.main.transform.position);

        }

       
    }

    public void Damage()
    {

        if(tankShield == true)
        {
            tankShield = false;            
            return;
        }
        lives--;
        uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Instantiate(explosionPlayer, transform.position, Quaternion.identity);
            gameManager.gameOver = true;
            uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
            Debug.Log("GAME OVER!!");
        }
        
        
       
    }

    public void Dance()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            animator.SetBool("Other", false);
            animator.Play("Dance1");           
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            animator.SetBool("Other", false);
            animator.Play("Dance2");           
        }

        if( x < 0 || x > 0|| y < 0 || y > 0 )
        {
            animator.SetBool("Other", true);
        }
    }
    public void PizzaShotOn()
    {
        pizzaShot = true;
        StartCoroutine(PowerUp1DownRoutine());
    }


    public IEnumerator PowerUp1DownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        pizzaShot = false;

    }

    public void BananaSpeedOn()
    {
        bananaSpeed = true;
        StartCoroutine(PowerUp2DownRoutine());
    }

    public IEnumerator PowerUp2DownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        bananaSpeed = false;
    }

    public void TankShieldOn()
    {
        tankShield = true;
        StartCoroutine(PowerUp3DownRoutine());
        escudoPlayer.SetActive(true);
    }

    public IEnumerator PowerUp3DownRoutine()
    {
        yield return new WaitForSeconds(5.4f);
        tankShield = false;
        escudoPlayer.SetActive(false);
    }
       
    

    
   
}
