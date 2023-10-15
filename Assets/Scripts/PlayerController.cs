using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManagerScript;
    private AudioSource playerAudio;
    public GameObject projectilePrefab;
    public AudioClip pointsSound;
    public AudioClip shootingSound;

    [SerializeField] private float speed = 60;
    private float horizontalInput;
    private float xRange = 50;
    private bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Keep the player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y); 
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y);
        }

        //Get player's input and move it left/right
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //Player can shoot when pressing Space key without spamming it
        if (Input.GetKeyDown(KeyCode.Space) && !gameManagerScript.isGameOver && canShoot)
        {
            StartCoroutine(ShootWithDelay());
            playerAudio.PlayOneShot(shootingSound, 2.0f);

            //Player will lose points when they shoot
            if (gameManagerScript.GetScore() >= 5)
            {
                gameManagerScript.UpdateScore(-5);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManagerScript.GameOver();
        }

        else if (other.gameObject.CompareTag("5 Points"))
        {
            gameManagerScript.UpdateScore(5);
            playerAudio.PlayOneShot(pointsSound, 2.0f);
        }

        else if (other.gameObject.CompareTag("10 Points"))
        {
            gameManagerScript.UpdateScore(10);
            playerAudio.PlayOneShot(pointsSound, 2.0f);
        }

        Destroy(other.gameObject);
    }

    //Prevent the player from shooting continuously
    IEnumerator ShootWithDelay()
    {
        canShoot = false;
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        yield return new WaitForSeconds(1);

        canShoot = true;
    }
}
