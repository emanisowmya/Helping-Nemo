using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetStuckedFish : MonoBehaviour
{
  [SerializeField]
  private Net net;                           // Net
  private SpriteRenderer renderer;           // Turtle Sprite Renderer
  private Rigidbody2D rigid;                // Turtle Rigid Body
  private Vector3 pos;                      // Turtle position
  private Vector3 new_pos;                  // Turtle new position
  public bool net_stucked = false;          // Turtle is stucked or not
  public bool isNearStuckedFish = false;    // Nemo is near stucked turtle or not
  private string NEMO = "Player";           // Storing Nemo Tag

  // Start is called before the first frame update
  void Start()
  {
    renderer = GetComponent<SpriteRenderer>();      // Store Turtle Sprite Renderer
    rigid = GetComponent<Rigidbody2D>();            // Store turtle rigid body
    pos = transform.position;                       // Store turtle position
  }

  // Update is called once per frame
  void Update()
  {
        // If turtle is free from net
    if (net.freeFromNet)
    {
      GetComponent<BoxCollider2D>().enabled = false;    // Disable Turtle Collider
      if (renderer.flipX)                               // Movement according to Turtle facing side
        rigid.velocity = new Vector2(-3f, 0f);
      else
        rigid.velocity = new Vector2(3f, 0f);
    }

    // If turtle is out of box, generate new position
    if (transform.position.x < -32 || transform.position.x > 32)
    {
      GetComponent<BoxCollider2D>().enabled = true;     // Enable Collider
      rigid.velocity = Vector3.zero;                    // Stop Turtle
      new_pos = pos + new Vector3(Random.Range(-10, 10), Random.Range(-1, 1), 0);   // Give random position
      if (new_pos[1] < -4 || new_pos[1] > 4) new_pos[1] = 0;
      transform.position = new_pos;
      net_stucked = true;                               // Stuck turtle in plastic
      pos = transform.position;
    }
  }

    // If nemo is near stuck turtle
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag(NEMO))
    {
      isNearStuckedFish = true;
    }
  }

    // If Nemo goes away from stucked turtle
  private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag(NEMO))
    {
      isNearStuckedFish = false;
    }
  }
}
