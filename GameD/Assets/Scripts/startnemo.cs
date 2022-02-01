using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNemo : MonoBehaviour
{
    // Start is called before the first frame update
    private int speed = 2;
    private float start;
    private float end;
    void Start()
    {
        Vector2 pos = transform.position;
        start = pos.x;
        end = pos.y;
    }

    // Update is called once per frame
    void Update()
    {
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
            SceneManager.LoadScene("Menu");
        }
    }
}
