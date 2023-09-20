using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBox : MonoBehaviour
{

    //public float distanceThreshold;
    public float maxForceMagnitude;  // Maximum force to be applied
    public float attractionRange;    // The range within which force increases
    private bool isPush = true;

    private Rigidbody2D rb;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isPush = !isPush;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the trigger zone has been entered by another object
        if (other.CompareTag("Magnet")) // Change "Player" to the appropriate tag
        {
            // Calculate the direction from this object to the player
            Vector2 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Check if the player is within the attraction range
            if (distanceToPlayer < attractionRange)
            {
                
                // Calculate a normalized force direction towards the player
                Vector2 forceDirection = directionToPlayer.normalized;

                if (isPush)
                {
                    forceDirection = forceDirection * -1;
                }

                // Calculate the force magnitude based on proximity (increases as the player gets closer)
                float forceMagnitude = Mathf.Lerp(maxForceMagnitude, 0, distanceToPlayer / attractionRange);

                // Apply the force to the Rigidbody2D
                rb.AddForce(forceDirection * forceMagnitude);
            }
        }
    }

}
