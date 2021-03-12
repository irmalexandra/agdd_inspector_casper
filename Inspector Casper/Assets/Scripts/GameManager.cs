using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting GameManager");
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(0, 7);
        Debug.Log("End GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
