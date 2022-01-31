using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MoveNemo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected int speed;
    private SpriteRenderer _renderer;

    private Rigidbody2D myBody;
    private bool isNearGarbage = false;
    private Collision2D collision;
    private string GARBAGE = "Garbage";

    AudioSource garbage;

    [SerializeField]
    protected ProgressBar Pb;

    [SerializeField]
    protected Image image;

    [SerializeField]
    protected Text text;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Pb.BarValue = 0;
        image.rectTransform.sizeDelta = new Vector2(0f, 0f);
        text.rectTransform.sizeDelta = new Vector2(0f, 0f);
        garbage = GetComponent<AudioSource>();
        garbage.volume = 0.5f;
        garbage.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        moveNemoShip();
        checkKeyPress();
        checkNextLevel();
    }

    private void checkNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
         {
            Scene scene = SceneManager.GetActiveScene();
            //print(scene.name[scene.name.Length-1]);
            int bar = scene.name[scene.name.Length - 1] - '0';
            if(bar<5)
            SceneManager.LoadScene("Level "+(bar+1));
        }
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
                garbage.Play();
                Destroy(collision.gameObject);
                Pb.BarValue += 20;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && Pb.BarValue == 100)
        {
            if (isNearGarbage)
            {
                StartCoroutine(errorMessage());
            }
        }

    }

    IEnumerator errorMessage()
    {
        //Print the time of when the function is first called.
        image.rectTransform.sizeDelta = new Vector2(650f, 82f);
        text.rectTransform.sizeDelta = new Vector2(650f, 82f);

        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds .
        image.rectTransform.sizeDelta = new Vector2(0f, 0f);
        text.rectTransform.sizeDelta = new Vector2(0f, 0f);
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

