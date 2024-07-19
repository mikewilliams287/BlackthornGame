using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");
        //print(input);

        rb.velocity = new Vector2(input * playerSpeed, rb.velocity.y);

    }
}
