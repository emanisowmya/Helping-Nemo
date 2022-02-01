using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    // Start is called before the first frame update
    public ProgressBar Pb;
    private bool isNearNemo = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkKeyPress();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "pulse1")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Nemo")
        {
            isNearNemo = true;
        }
    }

    private void checkKeyPress()
    {
        // key press check
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (isNearNemo)
            {
                Pb.BarValue = 0;
            }

        }


    }
    //Just stop hitting a collider 2D
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Nemo")
        {
            isNearNemo = false;
        }
    }
}
