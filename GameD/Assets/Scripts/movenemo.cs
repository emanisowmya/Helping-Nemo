using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveNemo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected int speed;
    private SpriteRenderer _renderer;

    private Rigidbody2D myBody;
    private Collision2D collision;
    private bool isNearGarbage = false;

    private string GARBAGE = "Garbage";
    private string SPAWNSHIP = "SpawnShip";

    AudioSource garbage;

    [SerializeField]
    protected ProgressBar Pb;

    [SerializeField]
    protected Image guideImage, scoreImage;

    [SerializeField]
    protected Text guideText, scoreText;

    [SerializeField]
    private TextMeshProUGUI textTimer;

    private bool playerWon = false, gameOver = false;
    private int scoreCollect = 0;

    // for checking if nemo is spawn ship, can be used by oil spill so kept public
    public bool isNearSpawnShip = false;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Pb.BarValue = 0;

        garbage = GetComponent<AudioSource>();
        garbage.volume = 0.5f;
        garbage.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
        MoveNemoShip();
        CheckKeyPress();
        CheckNextLevel();
    }

    private void CheckGameOver()
    {
        Scene scene = SceneManager.GetActiveScene();

        if ((textTimer.text == "Game Over!" || gameOver) && scene.name == "Level 1")
        {
            textTimer.text = string.Format("Game Over!");
            gameOver = true;
            if (playerWon)
            {
                guideText.text = "Congratulations, level complete.\nPress \"0\" to go to next level";
            }
            else
            {
                guideText.text = "Alas, you lost.\nPress \"1\" to restart";
            }
        }
    }
    private void CheckNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && playerWon)
        {
            Scene scene = SceneManager.GetActiveScene();
            //print(scene.name[scene.name.Length-1]);
            int bar = scene.name[scene.name.Length - 1] - '0';
            if (bar < 5)
                SceneManager.LoadScene("Level " + (bar + 1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && gameOver)
        {
            Scene scene = SceneManager.GetActiveScene();
            //print(scene.name[scene.name.Length-1]);
            int bar = scene.name[scene.name.Length - 1] - '0';
            SceneManager.LoadScene("Level " + bar);
        }
    }
    private void MoveNemoShip()
    {
        if (gameOver)
        {
            return;
        }

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

    private void CheckKeyPress()
    {
        if (gameOver)
        {
            return;
        }
        // key press check
        if (Input.GetKeyDown(KeyCode.Alpha2) && Pb.BarValue < 100)
        {
            if (isNearGarbage)
            {
                garbage.Play();
                Destroy(collision.gameObject);
                Pb.BarValue += 10;
                scoreCollect += 1;
                scoreText.text = "Score: " + scoreCollect;
                if (scoreCollect >= 20)
                {
                    playerWon = true;
                    gameOver = true;
                }
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
        guideText.text = "The collector is full.\ngo near ship and press \"9\" to release it.";

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        guideText.text = "Go near garbage and press \"2\" to collect it";
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GARBAGE))
        {
            this.collision = collision;
            isNearGarbage = true;
        }

        // for checking if nemo is spawn ship, can be used by oil spill
        if (collision.gameObject.CompareTag(SPAWNSHIP))
        {
            this.collision = collision;
            isNearSpawnShip = true;

        }
    }
    //Just stop hitting a collider 2D
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(GARBAGE))
        {
            isNearGarbage = false;
        }
        if (collision.gameObject.CompareTag(SPAWNSHIP))
        {
            isNearSpawnShip = false;
        }
    }

}

