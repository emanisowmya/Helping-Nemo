using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.name == "pulse1")
    {
      Destroy(collision.gameObject);
    }
  }
}
