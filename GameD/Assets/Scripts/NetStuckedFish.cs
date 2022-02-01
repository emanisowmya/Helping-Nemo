using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetStuckedFish : MonoBehaviour
{
    [SerializeField]
    private Net net;
    private SpriteRenderer renderer;
    private Rigidbody2D rigid;
    private Vector3 pos;
    private Vector3 new_pos;
    public bool net_stucked = false;
    public bool isNearStuckedFish = false;
    private string NEMO = "Player";

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("FreeFromNet " + net.freeFromNet);
        if (net.freeFromNet)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            if (renderer.flipX)
                rigid.velocity = new Vector2(-6f, 0f);
            else
                rigid.velocity = new Vector2(6f, 0f);
        }

        if(transform.position.x < -32 || transform.position.x > 32)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            rigid.velocity = Vector3.zero;
            new_pos = pos + new Vector3(Random.Range(-10, 10), Random.Range(-1, 1), 0);
            if (new_pos[1] < -4 || new_pos[1] > 4) new_pos[1] = 0;
            transform.position = new_pos;
            net_stucked = true;
            pos = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(NEMO))
        {
            isNearStuckedFish = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(NEMO))
        {
            isNearStuckedFish = false;
        }
    }
}
