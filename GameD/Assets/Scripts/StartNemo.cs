using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNemo : MonoBehaviour
{
    private int speed = 2;  // Speed of Nemo
    private float start;
    private float end;

    // Start is called before the first frame update
    void Start()
    {
        // Storing position of Nemo
        Vector2 pos = transform.position;
        start = pos.x;
        end = pos.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement of Nemo while introducing
        Vector2 pos = transform.position;
        if (pos.x < start / 2)
        {
            pos.x += speed * Time.deltaTime;
            transform.position = pos;
        }
        else if (pos.y > 0)
        {

            pos.y -= speed * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            // Loading Menu Scene after Nemo reached a particular location
            SceneManager.LoadScene("Menu");      
        }
    }
}
