using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
   // Transforms to act as start and end markers for the journey.
public Transform startMarker;
public Transform endMarker;

Animator anim;
private bool facingLeft = true;

// Movement speed in units/sec.
public float speed = 2.5F;

// Time when the movement started.
private float startTime;

// Total distance between the markers.
private float journeyLength;

void Start()
     {
     // Keep a note of the time the movement started.
          startTime = Time.time;

     // Calculate the journey length.
          journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
     }

// Follows the target position like with a spring
void Update()
     {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
     if (facingLeft == false && hozMovement > 0)
          {
               Flip();
          }
          else if (facingLeft == true && hozMovement < 0)
          {
               Flip();
          }

       
     // Distance moved = time * speed.
          float distCovered = (Time.time - startTime) * speed;

     // Fraction of journey completed = current distance divided by total distance.
          float fracJourney = distCovered / journeyLength;

     // Set our position as a fraction of the distance between the markers and pingpong the movement.
          transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong (fracJourney, 1));
     }

     void Flip()
   {
     facingLeft = !facingLeft;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
