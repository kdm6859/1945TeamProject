using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerDown : MonoBehaviour
{
    public float Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(1,-1);
        movement.Normalize();
        transform.Translate(movement*Time.deltaTime*Speed);
    }
}
