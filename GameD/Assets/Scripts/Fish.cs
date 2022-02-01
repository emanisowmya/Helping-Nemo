using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer renderer;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.flipX)
            rigid.velocity = new Vector2(Random.Range(-6f, -1f), Random.Range(-1f, 1f));
        else
            rigid.velocity = new Vector2(Random.Range(1f, 6f), Random.Range(-1f, 1f));
        pos = transform.position;
        if (transform.position.x < -32)
            renderer.flipX = false;
        else if (transform.position.x > 32)
            renderer.flipX = true;
        if (transform.position.y < -4)
            rigid.velocity = new Vector2(rigid.velocity.x, 1f);
        else if (transform.position.y > 4)
            rigid.velocity = new Vector2(rigid.velocity.x, -1f);
    }
}
