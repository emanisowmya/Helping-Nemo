using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

  // TODO:NOT COMPLETE
  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("he");
    Destroy(collision.gameObject);
  }
}
