using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Determine if we've collided with something collectable, then tell it to collect itself

    static private string COLLECTABLE = "Collectable";
    private void OnCollisionEnter2D(Collision2D collision)

    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag(COLLECTABLE))
        {
            // Try to get the ICollectable component from the collided object
            ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect();
            }
        }
    }
}
