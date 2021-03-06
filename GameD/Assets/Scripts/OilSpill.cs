using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OilSpill : MonoBehaviour
{
  [SerializeField] private float blobbingSpeed; // random blob speed
  [SerializeField] private float decreaseSpeed; // suck in speed by nemo
  private float spillSize; // oil size
  private float increaseSpeed; // gradual increase in oil
  private float movementSpeed; // gradual movement in oil

  private bool isSuckOilOn = false; // Is oil sucking functionality is available
  private static float suckedInInt = 0; // Quantity of oil sucked

  protected ProgressBar progressBar; // Progress Bar

    // Guide Text, Score Text, Text Timer
  [SerializeField]
  protected TextMeshProUGUI guideText, scoreText;
  private TextMeshProUGUI textTimer;


  // Game won/lost vars
  private static float scoreCollect = 0;
  private bool playerWon = false, gameOver = false;

  public Animator transition;

  public GameObject levelLoader;




  AudioSource gun;
  private void Awake()
  {
    progressBar = GameObject.Find("UI ProgressBar Oil").GetComponent<ProgressBar>();
    guideText = GameObject.Find("InstructionTextBg").GetComponent<TextMeshProUGUI>();
    scoreText = GameObject.Find("ScoreText_oil").GetComponent<TextMeshProUGUI>();
    textTimer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
  }

  void Start()
  {
    // Initialise the oil spill size randomly
    spillSize = Random.Range(1, 5);
    transform.localScale = new Vector3(2.5f * spillSize, spillSize, spillSize);

    scoreCollect = 0;
    suckedInInt = 0;
    progressBar.BarValue = 0;

    // Initate Audio sources
    gun = GetComponent<AudioSource>();
    gun.volume = 0.5f;
    gun.Stop();
  }

  // Update is called once per frame
  void Update()
  {
    CheckGameOver();    // Check whether game is over
    CheckKeyPress();    // Check key is pressed
    CheckNextLevel();   // Check to next level

    // In survival mode, if time is increased
    if (textTimer.text == "Time Increased")
    {
      scoreCollect = 0;
      suckedInInt = 0;
      progressBar.BarValue = 0;
      gameOver = false;
    }

  }

    // Check whether game is over
  private void CheckGameOver()
  {
    Scene scene = SceneManager.GetActiveScene();
    if (scene.name == "Level 3" && (textTimer.text == "Game Over!" || gameOver))
    {
      gameOver = true;

      if (scoreText.text == "Score: 30")    // If Score equals 30, level completes
      {
        guideText.text = "Congratulations, level complete.\nPress \"0\" to go to next level";
      }
      else                                  // If level failed, press 1 to restart
      {
        guideText.text = "Alas, you lost.\nPress \"1\" to restart";
      }
    }
  }
   
    // If player won, check to next level otherwise repeat
  private void CheckNextLevel()
  {
    // if player won and press keypad 0
    if (Input.GetKeyDown(KeyCode.Alpha0) && playerWon)
    {
      levelLoader.active = true;
      loadNextLevel();
    }

    // if game over and press keypad 1
    else if (Input.GetKeyDown(KeyCode.Alpha1) && gameOver)
    {
      Scene scene = SceneManager.GetActiveScene();
      //print(scene.name[scene.name.Length-1]);
      int bar = scene.name[scene.name.Length - 1] - '0';
      SceneManager.LoadScene("Level " + bar);
    }
  }

    // Load next level
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
  }

    // If key is pressed
  private void CheckKeyPress()
  {

    if (gameOver)    // If game is over
    {
      return;
    }

    Scene scene = SceneManager.GetActiveScene();

    // gradually keep making the oil blob bigger and bigger
    increaseSpeed = 0.0004f + Random.Range(-blobbingSpeed, blobbingSpeed);
    transform.localScale += new Vector3(increaseSpeed, increaseSpeed, increaseSpeed);

    // random small movements
    movementSpeed = Random.Range(-blobbingSpeed, blobbingSpeed);
    transform.position += new Vector3(movementSpeed, movementSpeed, movementSpeed);

    if ((Input.GetKey(KeyCode.Alpha4) || Input.GetAxis("Mouse ScrollWheel") < 0f) && isSuckOilOn && progressBar.BarValue < 100)
    {
      // audio
      gun.Play();

      // progress bar 

      if (!(scene.name == "Level 5"))
      {
        suckedInInt += Time.deltaTime * 10;
        progressBar.BarValue = (int)suckedInInt;
      }
      else
      {

        suckedInInt += Time.deltaTime * 5;
        progressBar.BarValue = (int)suckedInInt;
      }
      // scoring
      scoreCollect += Time.deltaTime * 3;
      scoreText.text = "Score: " + (int)scoreCollect;

      if (!(scene.name == "Level 5") && scoreCollect >= 30)
      {
        playerWon = true;
        gameOver = true;
      }
      else if (scoreCollect >= 60)
      {
        playerWon = true;
        gameOver = true;
      }

      // gradually suck the oil blob making it 
      transform.localScale -= new Vector3(2 * decreaseSpeed, decreaseSpeed, decreaseSpeed);

      // if oil size reaches zero, destroy object
      if (transform.localScale.y <= 0.1f)
      {
        Destroy(gameObject);
        gun.Stop();
      }

    }
    else if (Input.GetKeyDown(KeyCode.Alpha4) && progressBar.BarValue == 100)
    {
      if (isSuckOilOn)
      {
        StartCoroutine(errorMessage());
      }

    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Nemo")
    {
      // When triggered, oil can be sucked into nemo
      isSuckOilOn = true;

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


  IEnumerator errorMessage()
  {
    //Print the time of when the function is first called.
    guideText.text = "The collector is full.\ngo near ship and press \"9\" to release it.";

    //yield on a new YieldInstruction that waits for 5 seconds.
    yield return new WaitForSeconds(5);

    guideText.text = "Go near oil and press \"4\" to collect it";
  }


}
