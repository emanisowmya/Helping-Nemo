using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

  void OnCollisionEnter2D(Collision2D collision)
  {
        // If Pulse hits collector, destroy pulse
    if (collision.gameObject.name == "pulse1")
    {
      Destroy(collision.gameObject);
    }
  }
}
