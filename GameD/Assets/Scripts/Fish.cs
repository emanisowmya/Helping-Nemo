using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
  private Rigidbody2D rigid;        // Random Fish Rigid Body
  private SpriteRenderer renderer;  // Random Fish Sprite Renderer
  private Vector2 pos;              // Random Fish Position

  // Start is called before the first frame update
  void Start()
  {
    renderer = GetComponent<SpriteRenderer>();  // Random Fish Sprite Renderer
    rigid = GetComponent<Rigidbody2D>();    // Random Fish Rigid Body
  }

  // Update is called once per frame
  void Update()
  {
    // Giving velocity to fish according to its face side
    if (renderer.flipX)
      rigid.velocity = new Vector2(Random.Range(-6f, -1f), Random.Range(-0.5f, 0.5f));
    else
      rigid.velocity = new Vector2(Random.Range(1f, 6f), Random.Range(-0.5f, 0.5f));

    // postion of fish
    pos = transform.position;

    // If fish reaches horizontal end, change facing side
    if (transform.position.x < -32)
      renderer.flipX = false;
    else if (transform.position.x > 32)
      renderer.flipX = true;

    // If fish reaches vertical end, change position
    if (transform.position.y < -4f)
    {
      transform.position = new Vector2(transform.position.x, -1 * transform.position.y);
      rigid.velocity = new Vector2(rigid.velocity.x, 2f);
    }
    else if (transform.position.y > 4)
      rigid.velocity = new Vector2(rigid.velocity.x, -1f);
  }
}
