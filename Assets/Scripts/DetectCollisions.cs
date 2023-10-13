using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public AudioClip shootingRocksSound;
    //public ParticleSystem fireRocksParticle;




    //private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Instantiate(fireRocksParticle, transform.position, fireRocksParticle.transform.rotation);

            //fireRocksParticle.Play();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
