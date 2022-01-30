using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNemo : MonoBehaviour
{
  // Start is called before the first frame update
  private int speed = 5;
  private SpriteRenderer _renderer;

  private Rigidbody2D myBody;
  private bool isNearGarbage = false;
  private Collision2D collision;
  private string GARBAGE = "Garbage";

  void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();
  }
  void Start()
  {
    _renderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    moveNemoShip();
    checkKeyPress();
  }

  private void moveNemoShip()
  {

    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    if (h > 0)
    {
      _renderer.flipX = false;
    }
    else if (h < 0)
    {
      _renderer.flipX = true;
    }
    Vector2 pos = transform.position;
    pos.x += h * speed * Time.deltaTime;
    pos.y += v * speed * Time.deltaTime;
    transform.position = pos;
  }

  private void checkKeyPress()
  {
    // key press check
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      if (isNearGarbage)
      {
        Destroy(collision.gameObject);
      }
    }


  }


  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag(GARBAGE))
    {
      this.collision = collision;
      isNearGarbage = true;
    }
  }

  //Just stop hitting a collider 2D
  private void OnCollisionExit2D(Collision2D collision)
  {

    if (collision.gameObject.CompareTag(GARBAGE))
    {
      isNearGarbage = false;
    }
  }

}

