using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithinBounds : MonoBehaviour
{
    private GameManager gameManagerScript;

    private float bottomBound = -12;
    private float topBound = 90;
    public float speed = 30;

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
            //Move the projectile up and the rocks down
            if (gameObject.CompareTag("Projectile"))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            else
                transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        //Destroy objects out of bound
        if ((transform.position.y < bottomBound || transform.position.y > topBound) && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}
