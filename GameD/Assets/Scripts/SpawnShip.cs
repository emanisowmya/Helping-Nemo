using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    // Progress Bar
    public ProgressBar Pb;
    private bool isNearNemo = false; // Bool to check spawn ship is near nemo

    // Update is called once per frame
    void Update()
    {
        checkKeyPress();
    }

    // Called on collision between objects
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If collides with pulse
        if (collision.gameObject.name == "pulse1")
        {
            Destroy(collision.gameObject);
        }

        // If collides with Nemo
        if (collision.gameObject.name == "Nemo")
        {
            isNearNemo = true;
        }
    }

    // key press check
    private void checkKeyPress()
    {
        // If key pressed is Keypad 9
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // If Nemo is near Spawn Ship
            if (isNearNemo)
            {
                // Garbage or Oil dumped into Spawn Ship
                Pb.BarValue = 0;
            }

        }


    }

    //Just stop hitting a collider 2D
    private void OnCollisionExit2D(Collision2D collision)
    {
        // If Nemo goes away from Spawn Ship
        if (collision.gameObject.name == "Nemo")
        {
            isNearNemo = false;
        }
    }
}
