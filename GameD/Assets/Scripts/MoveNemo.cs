using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveNemo : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] protected int speed;         // Speed of Nemo
  private SpriteRenderer _renderer;             // Nemo Sprite Renderer

  private Rigidbody2D myBody;                   // Nemo body
  private Collision2D collision;                // Nemo collision 2D
  private bool isNearGarbage = false, baki_left = false;

  private string GARBAGE = "Garbage";       // Tag for Garbage
  private string SPAWNSHIP = "SpawnShip";   // tag for SpawnShip

  AudioSource garbage;                      // Audio for Garbage

  public ProgressBar Pb;                    // Progress Bar

    // Guide Image, Score Image
  [SerializeField]
  protected Image guideImage, scoreImage;

    // Guide Text, Score Text for all
  public TextMeshProUGUI guideText, scoreText, scoreText_animal, scoreText_oil;

    // Rect Transform for help menu
  [SerializeField]
  private RectTransform help;

    // To open help menu
  private bool open_help = true;

    // Text Timer
  [SerializeField]
  private TextMeshProUGUI textTimer;

    // Player Won
  public bool playerWon = false, gameOver = false;
  public int scoreCollect = 0;

  // for checking if nemo is spawn ship, can be used by oil spill so kept public
  public bool isNearSpawnShip = false;

    // For transition
  public Animator transition;

   // GameObject Level Loader
  public GameObject levelLoader;

  void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();       // Nemo Rigid Body

  }

  void Start()
  {
    _renderer = GetComponent<SpriteRenderer>();     // Nemo Sprite Renderer
    Pb.BarValue = 0;                                // Progress Bar

    garbage = GetComponent<AudioSource>();          // Audio Source for garbage collection
    garbage.volume = 0.5f;                          // Volume for audio of garbage collection
    garbage.Stop();                                 // Stopping audio
  }

  // Update is called once per frame
  void Update()
  {
    CheckGameOver();    // Check whether game is over
    MoveNemoShip();     // moving nemo ship
    CheckKeyPress();    // checking key press
    CheckNextLevel();   // upload next level
  }

    // Check whether game is over
    private void CheckGameOver()
  {
    Scene scene = SceneManager.GetActiveScene();

        // If game is over, advance to next level for level 1
    if (scene.name == "Level 1" && (textTimer.text == "Game Over!" || gameOver))
    {
      textTimer.text = "Game Over!";
      gameOver = true;
            // if player won
      if (playerWon)
      {
        guideText.text = "Congratulations, level complete.\nPress \"0\" to go to next level";
      }
      else // if player lost
      {
        guideText.text = "Alas, you lost.\nPress \"1\" to restart";
      }
    }
    // For level 2, game winning criteria
    else if (scene.name == "Level 2")
    {
      if (scoreText.text == "Score: 10")
      {
        textTimer.text = "Game Over!";
        playerWon = true;
        gameOver = true;
      }
    }
        // For level 3, game winning criteria
        else if (scene.name == "Level 3")
    {
      if (scoreText.text == "Score: 30")
      {
        textTimer.text = "Game Over!";
        playerWon = true;
        gameOver = true;
      }
    }
        // For level 4, game winning criteria
    else if (scene.name == "Level 4")
    {
      if (textTimer.text == "Game Over!" || (scoreText_animal.text == "Score: 10" &&
          scoreText_oil.text == "Score: 30" &&
          gameOver))
      {
        textTimer.text = "Game Over!";
        baki_left = false;
        gameOver = true;
        // If player won
        if (playerWon)
        {
          guideText.text = "Congratulations, level complete.\nPress \"0\" to go to next level";
        }
        // If player lost
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

    // For level 5, game winning criteria
    else if (scene.name == "Level 5")
    {
      if (textTimer.text == "Game Over!" || (scoreText_animal.text == "Score: 20" &&
          scoreText_oil.text == "Score: 60" &&
          gameOver))
      {
        textTimer.text = "Game Over!";
        baki_left = false;
        gameOver = true;
        if (playerWon)  // If player won
        {
          guideText.text = "Congratulations, you did it..\nPress \"0\" to go to main menu";
        }
        else            // If player lost
        {
          guideText.text = "Alas, you lost.\nPress \"1\" to restart";
        }
      }
      else
      {
        baki_left = true;
      }
    }
    // For Survival level
    else if (scene.name == "SurvivalLevel")
    {
        // Continue gaming
      if ((scoreText_animal.text == "Score: 10" &&
          scoreText_oil.text == "Score: 30" &&
          gameOver)
          )
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
      // Game is over
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

    // Next Gaming Level
  private void CheckNextLevel()
  {
    // If player won and pressed 0, advance to next level
    if (Input.GetKeyDown(KeyCode.Alpha0) && playerWon)
    {
      levelLoader.active = true;
      loadNextLevel();
    }
    // If player lost and press 1, repeat this level
    else if (Input.GetKeyDown(KeyCode.Alpha1) && gameOver)
    {
      Scene scene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(scene.name);
    }
  }

    // Loading next level
  private void loadNextLevel()
  {
    StartCoroutine(LoadLevel());
  }

  IEnumerator LoadLevel()
  {
    transition.SetTrigger("Start");

    yield return new WaitForSeconds(1);

    Scene scene = SceneManager.GetActiveScene();
    int bar = scene.name[scene.name.Length - 1] - '0';
    if (bar < 5)
      SceneManager.LoadScene("Level " + (bar + 1));
    else if (bar == 5)
    {
      SceneManager.LoadScene("Intro");
    }
  }

    // Moving Nemo Ship
  private void MoveNemoShip()
  {
    // If game over
    if (!baki_left && gameOver)
    {
      return;
    }

    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    // Flipping nemo ship
    if (h > 0)
    {
      _renderer.flipX = false;
    }
    else if (h < 0)
    {
      _renderer.flipX = true;
    }
    // New position of Nemo Ship
    Vector2 pos = transform.position;
    pos.x += h * speed * Time.deltaTime;
    pos.y += v * speed * Time.deltaTime;
    transform.position = pos;
  }

    // If key is pressed
  private void CheckKeyPress()
  {
        // If game is over
    if (gameOver)
    {
      return;
    }

    Scene scene = SceneManager.GetActiveScene();

        // If Bar value is less than 100 and player pressed 2, then collect garbage
    if (Input.GetKeyDown(KeyCode.Alpha2) && Pb.BarValue < 100)
    {
            // If near garbage
      if (isNearGarbage)
      {
        garbage.Play();
        Destroy(collision.gameObject);
        Pb.BarValue += 10;
        scoreCollect += 1;
        scoreText.text = "Score: " + scoreCollect;

        // Winning criteria according to level
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

    // If bar value is equal to 100 and player tries to collect garbage, show error message
    else if (Input.GetKeyDown(KeyCode.Alpha2) && Pb.BarValue == 100)
    {
      if (isNearGarbage)
      {
        StartCoroutine(errorMessage());
      }
    }

    // Press h to toggle help menu
    else if (Input.GetKeyDown(KeyCode.H))
    {
      open_help = !open_help;
    }
    // Open/Close help menu accordingly
    if (open_help)
    {
      help.transform.position = new Vector3(help.transform.position.x, 0, 0);
    }
    else
    {
      help.transform.position = new Vector3(help.transform.position.x, -1000, 0);
    }

  }

    // Showing Error Message
  IEnumerator errorMessage()
  {
    Scene scene = SceneManager.GetActiveScene();

    //Print the time of when the function is first called.
    guideText.text = "The collector is full.\ngo near ship and press \"9\" to release it.";

    //yield on a new YieldInstruction that waits for 5 seconds.
    yield return new WaitForSeconds(5);

        // Guide Text according to level
    if (scene.name == "Level 1")
    {
      guideText.text = "Go near garbage and press \"2\" to collect it";
    }
    else if (scene.name == "Level 4")
    {
      guideText.text = "Collect 20 garbage, 30 gallon oil and save 10 animals";
    }
    else if (scene.name == "Level 5")
    {
      guideText.text = "Collect 30 garbage, 60 gallon oil and save 20 animals";
    }
    else if (scene.name == "SurvivalLevel")
    {
      guideText.text = "Collect items till given maximum capacity, once all are full you will get 20 sec extra";
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
        // If nemo is near garbage
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

    // If nemo goes away from garbage
    if (collision.gameObject.CompareTag(GARBAGE))
    {
      isNearGarbage = false;
    }

    // for checking if nemo is spawn ship, can be used by oil spill
    if (collision.gameObject.CompareTag(SPAWNSHIP))
    {
      isNearSpawnShip = false;
    }
  }

}

