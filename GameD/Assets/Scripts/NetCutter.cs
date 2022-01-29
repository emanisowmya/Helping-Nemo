using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCutter : MonoBehaviour
{

    private SpriteRenderer _renderer;
    private int speed = 5;
    private bool CutterPressed = false;

    [SerializeField]
    private GameObject nemo;

    [SerializeField]
    private SpriteRenderer nemo_renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = nemo.transform.position;
        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (_renderer.flipX)
                transform.position = nemo.transform.position - speed * Time.deltaTime * (new Vector3(20f, 0, 0));
            else
                transform.position = nemo.transform.position + speed * Time.deltaTime * (new Vector3(20f, 0, 0));
            CutterPressed = true;
        }
        else if (CutterPressed)
        {
            transform.position = nemo.transform.position;
            CutterPressed = false;
        }
        _renderer.flipX = nemo_renderer.flipX;
    }
}