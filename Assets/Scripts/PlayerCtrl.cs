using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float playerSpeed;
    public int playerHealth;
    private float input;
    Rigidbody2D rb;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");
        //print(input);

        rb.velocity = new Vector2(input * playerSpeed, rb.velocity.y);

    }

    public void TakeDamage(int damageAmount)
    {
        playerHealth -= damageAmount;

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
