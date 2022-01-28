using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movenemo : MonoBehaviour
{
    // Start is called before the first frame update
    private int speed = 5;
    private SpriteRenderer _renderer;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
        /*
        if (v < 0)
        {
            _renderer.flipY = false;
        }
        else if (v > 0)
        {
            _renderer.flipY = true;
        }
        */
        Vector2 pos = transform.position;
        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;
        transform.position = pos;
    }
}
