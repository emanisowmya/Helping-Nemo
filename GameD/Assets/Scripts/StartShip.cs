using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartShip : MonoBehaviour
{
    // Start is called before the first frame update
    private int speed = 2;
    private float start;
    private float end;
    AudioSource ship;

    void Start()
    {
        // To store positions
        Vector2 pos = transform.position;
        start = pos.x;
        end = pos.y;
        // Audio Source
        ship = GetComponent<AudioSource>();
        ship.volume = 0.5f;
        ship.Play();

    }

    // Update is called once per frame
    void Update()
    {
        // Movement of Carrier Ship
        Vector2 pos = transform.position;
        if (pos.x <= -start)
        {
            pos.x += speed * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            SceneManager.LoadScene("Nemo Intro");   // Load Introduction Scene
        }
        if (pos.x >= start / 2)
        {
            speed = 4;
        }
    }
}
