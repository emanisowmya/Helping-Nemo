using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [SerializeField] private float blobbingSpeed;
    [SerializeField] private float decreaseSpeed;
    private float spillSize;
    private float increaseSpeed;
    private float movementSpeed;

    private bool isSuckOilOn = false;

    void Start()
    {
        // Initialise the oil spill size randomly
        spillSize = Random.Range(1, 5);
        transform.localScale = new Vector3(2.5f * spillSize, spillSize, spillSize);
    }

    // Update is called once per frame
    void Update()
    {
        // gradually keep making the oil blob bigger and bigger
        increaseSpeed = 0.0004f + Random.Range(-blobbingSpeed, blobbingSpeed);
        transform.localScale += new Vector3(increaseSpeed, increaseSpeed, increaseSpeed);

        // random small movements
        movementSpeed = Random.Range(-blobbingSpeed, blobbingSpeed);
        transform.position += new Vector3(movementSpeed, movementSpeed, movementSpeed);

        if (Input.GetKey(KeyCode.Alpha3) && isSuckOilOn)
        {

            // gradually suck the oil blob making it smaller

            transform.localScale -= new Vector3(2 * decreaseSpeed, decreaseSpeed, decreaseSpeed);
            if (transform.localScale.y <= 0.1f)
            {
                Destroy(gameObject);
            }

        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "pulse1")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nemo")
        {

            isSuckOilOn = true;
            Debug.Log("oil");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nemo")
        {
            isSuckOilOn = false;

        }
    }
}
