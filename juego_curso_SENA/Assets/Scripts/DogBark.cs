using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBark : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.z < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                if( player.tankShield == false)
                {
                    player.Damage();
                }
               
            }
                   
            Destroy(this.gameObject);
        }
    }
}
