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
  private bool isNearGarbage = false, baki_left = false;

  private string GARBAGE = "Garbage";
  private string SPAWNSHIP = "SpawnShip";

  AudioSource garbage;

  public ProgressBar Pb;

  [SerializeField]
  protected Image guideImage, scoreImage;

  public TextMeshProUGUI guideText, scoreText, scoreText_animal, scoreText_oil;

  [SerializeField]
  private RectTransform help;

  private bool open_help = true;

  [SerializeField]
  private TextMeshProUGUI textTimer;


  public bool playerWon = false, gameOver = false;
  public int scoreCollect = 0;

  // for checking if nemo is spawn ship, can be used by oil spill so kept public
  public bool isNearSpawnShip = false;


  public Animator transition;

  public GameObject levelLoader;

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

    if (scene.name == "Level 1" && (textTimer.text == "Game Over!" || gameOver))
    {
      textTimer.text = "Game Over!";
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
    else if (scene.name == "Level 2")
    {
      if (scoreText.text == "Score: 10")
      {
        textTimer.text = "Game Over!";
        playerWon = true;
        gameOver = true;
      }
    }
    else if (scene.name == "Level 3")
    {
      if (scoreText.text == "Score: 30")
      {
        textTimer.text = "Game Over!";
        playerWon = true;
        gameOver = true;
      }
    }
    else if (scene.name == "Level 4")
    {
      if (textTimer.text == "Game Over!" || (scoreText_animal.text == "Score: 10" &&
          scoreText_oil.text == "Score: 30" &&
          gameOver))
      {
        textTimer.text = "Game Over!";
        baki_left = false;
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
      else
      {
        baki_left = true;
      }
    }
    else if (scene.name == "Level 5")
    {
      if (textTimer.text == "Game Over!" || (scoreText_animal.text == "Score: 20" &&
          scoreText_oil.text == "Score: 60" &&
          gameOver))
      {
        Debug.Log("Enter");
        textTimer.text = "Game Over!";
        baki_left = false;
        gameOver = true;
        if (playerWon)
        {
          guideText.text = "Congratulations, you did it..\nPress \"0\" to go to main menu";
        }
        else
        {
          guideText.text = "Alas, you lost.\nPress \"1\" to restart";
        }
      }
      else
      {
        baki_left = true;
      }
    }
    else if (scene.name == "SurvivalLevel")
    {
      if ((scoreText_animal.text == "Score: 10" &&
          scoreText_oil.text == "Score: 30" &&
          gameOver))
      {
        gameOver = false;
        baki_left = false;
        textTimer.text = "Time Increased";
        scoreCollect = 0;
        Pb.BarValue = 0;
        scoreText_animal.text = "Score: 0";
        scoreText_oil.text = "Score: 0";
        scoreText.text = "Score: 0";
      }
      else if (textTimer.text == "Game Over!")
      {
        gameOver = true;
        baki_left = false;
        guideText.text = "Congratulations, You have made it this far.\nPress \"1\" to restart";

      }
      else
      {
        baki_left = true;
      }
    }
  }


  private void CheckNextLevel()
  {
    if (Input.GetKeyDown(KeyCode.Alpha0) && playerWon)
    {
      levelLoader.active = true;
      loadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.Alpha1) && gameOver)
    {
      Scene scene = SceneManager.GetActiveScene();
      // //print(scene.name[scene.name.Length-1]);
      // int bar = scene.name[scene.name.Length - 1] - '0';
      SceneManager.LoadScene(scene.name);
    }
  }

  private void loadNextLevel()
  {
    StartCoroutine(LoadLevel());
  }

  IEnumerator LoadLevel()
  {
    transition.SetTrigger("Start");

    yield return new WaitForSeconds(1);

    Scene scene = SceneManager.GetActiveScene();
    //print(scene.name[scene.name.Length-1]);
    int bar = scene.name[scene.name.Length - 1] - '0';
    if (bar < 5)
      SceneManager.LoadScene("Level " + (bar + 1));
    else if (bar == 5)
    {
      SceneManager.LoadScene("Intro");
    }
  }
  private void MoveNemoShip()
  {
    if (!baki_left && gameOver)
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

    Scene scene = SceneManager.GetActiveScene();

    if (Input.GetKeyDown(KeyCode.Alpha2) && Pb.BarValue < 100)
    {
      if (isNearGarbage)
      {
        garbage.Play();
        Destroy(collision.gameObject);
        Pb.BarValue += 10;
        scoreCollect += 1;
        scoreText.text = "Score: " + scoreCollect;

        if (!(scene.name == "Level 5") && scoreCollect >= 20)
        {
          playerWon = true;
          gameOver = true;
        }
        else if (scoreCollect >= 30)
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
    else if (Input.GetKeyDown(KeyCode.H))
    {
      open_help = !open_help;
    }
    if (open_help)
    {
      help.transform.position = new Vector3(help.transform.position.x, 0, 0);
    }
    else
    {
      help.transform.position = new Vector3(help.transform.position.x, -1000, 0);
    }

  }

  IEnumerator errorMessage()
  {
    Scene scene = SceneManager.GetActiveScene();

    //Print the time of when the function is first called.
    guideText.text = "The collector is full.\ngo near ship and press \"9\" to release it.";

    //yield on a new YieldInstruction that waits for 5 seconds.
    yield return new WaitForSeconds(5);

    if (scene.name == "Level 1")
    {
      guideText.text = "Go near garbage and press \"2\" to collect it";
    }
    else if (scene.name == "Level 4")
    {
      guideText.text = "Collect 20 garbage, 30 gallan oil and save 10 animals";
    }
    else if (scene.name == "Level 5")
    {
      guideText.text = "Collect 30 garbage, 60 gallan oil and save 20 animals";
    }
    else if (scene.name == "SurvivalLevel")
    {
      guideText.text = "Collect items till given maximum capacity, once all are full you will get 20 sec extra";
    }
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

