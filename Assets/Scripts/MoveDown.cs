using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private GameManager gameManagerScript;

    public float speed = 30;
    private float bottomBound = -12;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameManagerScript.isGameOver)
        {
            if (gameObject.CompareTag("Projectile"))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            else
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        //Destroy objects out of bound
        if ((transform.position.y < bottomBound || transform.position.y > 90) && (gameObject.CompareTag("5 Points") || gameObject.CompareTag("10 Points") || gameObject.CompareTag("Enemy")))
        {
            Destroy(gameObject);
        }

        
    }
}
