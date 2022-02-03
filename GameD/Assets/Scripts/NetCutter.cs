using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCutter : MonoBehaviour
{

    private SpriteRenderer _renderer;   // Net Cutter Sprite Renderer
    private int speed = 5;              // Speed of Net Cutter
    private bool CutterPressed = false; // Cutter user or not

    [SerializeField]
    private GameObject nemo;            // Game Object Nemo

    [SerializeField]
    private SpriteRenderer nemo_renderer;   // Sprite Renderer Nemo

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();     // Net Cutter Sprite Renderer
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = nemo.transform.position;   // Update Net cutter position as Nemo position

        // If key 3 is pressed
        if (Input.GetKey(KeyCode.Alpha3))
        {
            // Movement of Net Cutter according to face nemo is facing
            if (_renderer.flipX)
                transform.position = nemo.transform.position - speed * Time.deltaTime * (new Vector3(20f, 0, 0));
            else
                transform.position = nemo.transform.position + speed * Time.deltaTime * (new Vector3(20f, 0, 0));
            CutterPressed = true;
        }
        // If Cutter is used
        else if (CutterPressed)
        {
            transform.position = nemo.transform.position;
            CutterPressed = false;
        }
        _renderer.flipX = nemo_renderer.flipX;
    }
}