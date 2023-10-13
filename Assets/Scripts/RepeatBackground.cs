using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatBg;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        repeatBg = GetComponent<BoxCollider>().size.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < (startPosition.y - repeatBg))
        {
            transform.position = startPosition;
        }
    }
}
