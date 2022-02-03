using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{

  void OnCollisionEnter2D(Collision2D collision)
  {
        // If garbage is near Pulse, destroy pulse
    if (collision.gameObject.name == "pulse1")
    {
      Destroy(gameObject);
      Destroy(collision.gameObject);
    }
  }

} // class 