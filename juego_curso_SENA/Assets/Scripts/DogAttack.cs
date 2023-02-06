using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttack : MonoBehaviour
{
    public GameObject bark;
    public Transform spawnPos;
    public AudioSource audioSource;
    private float delay = 2;
    private float fireRate = 2f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        InvokeRepeating("Shoot", delay, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Instantiate(bark, spawnPos.position, spawnPos.rotation);
        audioSource.Play();
    }
}
