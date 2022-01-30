using UnityEngine;

public class MoveNemo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int speed;
    private SpriteRenderer _renderer;

    private Rigidbody2D myBody;
    private bool isNearGarbage = false;
    private Collision2D collision;
    private string GARBAGE = "Garbage";

    public ProgressBar Pb;
    private int value = 0;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Pb.BarValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveNemoShip();
        checkKeyPress();
    }

    private void moveNemoShip()
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
        Vector2 pos = transform.position;
        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;
        transform.position = pos;
    }

    private void checkKeyPress()
    {
        // key press check
        if (Input.GetKeyDown(KeyCode.Alpha2) && Pb.BarValue < 100)
        {
            if (isNearGarbage)
            {
                Destroy(collision.gameObject);
                Pb.BarValue += 20;
            }
        }


    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GARBAGE))
        {
            this.collision = collision;
            isNearGarbage = true;
        }
    }

    //Just stop hitting a collider 2D
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(GARBAGE))
        {
            isNearGarbage = false;
        }
    }

}

