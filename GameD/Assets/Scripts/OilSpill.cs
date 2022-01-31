using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [SerializeField] private float blobbingSpeed; // random blob speed
    [SerializeField] private float decreaseSpeed; // suck in speed by nemo
    private float spillSize; // oil size
    private float increaseSpeed; // gradual increase in oil
    private float movementSpeed; // gradual movement in oil

    private bool isSuckOilOn = false;

    AudioSource gun;

    void Start()
    {
        // Initialise the oil spill size randomly
        spillSize = Random.Range(1, 5);
        transform.localScale = new Vector3(2.5f * spillSize, spillSize, spillSize);
        gun = GetComponent<AudioSource>();
        gun.volume = 0.5f;
        gun.Stop();
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
            gun.Play();
            transform.localScale -= new Vector3(2 * decreaseSpeed, decreaseSpeed, decreaseSpeed);
            // if oil size reaches zero, destroy object
            if (transform.localScale.y <= 0.1f)
            {
                Destroy(gameObject);
                gun.Stop();
            }

        }
        else
        {
           // gun.Stop();
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nemo")
        {
            // When triggered, oil can be sucked into nemo
            isSuckOilOn = true;
            //Debug.Log("oil");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nemo")
        {
            // When leave the oil boundary, not able to suck now
            isSuckOilOn = false;

        }
    }
}
