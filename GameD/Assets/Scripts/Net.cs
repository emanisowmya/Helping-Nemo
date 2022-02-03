using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Net : MonoBehaviour
{

  [SerializeField]
  private GameObject net_stucked_fish;      // Game Object Turtle

  protected ProgressBar progressBar;        // Progress Bar

    // Guide Text, Score Text, Text Timer
  protected TextMeshProUGUI guideText, scoreText;
  private TextMeshProUGUI textTimer;


  // Game won/lost vars
  private static float scoreCollect = 0;
  private bool playerWon = false, gameOver = false;

  [SerializeField]
  private NetStuckedFish net_again;     // If Turtle got stuck again

  [SerializeField]
  private MoveNemo nemo;                // Nemo

  public bool freeFromNet = false;      // If Turtle is free

  public Animator transition;           // Transition before level starts

  public GameObject levelLoader;        // Next level loader

    AudioSource knife;                  // Audio for net cutter

    void Awake()
    {
        progressBar = GameObject.Find("UI ProgressBar Animal Save").GetComponent<ProgressBar>();    // Progress Bar
        guideText = GameObject.Find("InstructionTextBg").GetComponent<TextMeshProUGUI>();           // Guide Text
        scoreText = GameObject.Find("ScoreText_animal").GetComponent<TextMeshProUGUI>();            // Score Text
        textTimer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();                       // Text Timer
    }

  // Start is called before the first frame update
  void Start()
  {
    scoreCollect = 0;                           // Score
    progressBar.BarValue = 0;                   // Progree Bar
    knife = GetComponent<AudioSource>();        // Audio for net cutter
    knife.volume = 0.5f;                        // Volume for audio
    knife.Stop();                               // Stopping audio
  }

  // Update is called once per frame
  void Update()
  {
        // Updating position of net according to turtle
    transform.position = net_stucked_fish.transform.position;
    CheckKeyPress();    // Check if key is pressed
    CheckNextLevel();   // Load next level
    CheckGameOver();    // Check whether game is over or not

    // For survival mode, if time increased
    if (textTimer.text == "Time Increased")
    {
      scoreCollect = 0;
      progressBar.BarValue = 0;
      gameOver = false;
    }
  }

    // If key is pressed
    private void CheckKeyPress()
    {

        if (gameOver)
        {
            return;
        }

        // If keypad 3 is pressed, play audio for net cutter
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            knife.Play();
        }

        Scene scene = SceneManager.GetActiveScene();
        // Bar value is less than 100 and key 3 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3) && progressBar.BarValue < 100)
        {
            // If nemo is near turtle
            if (net_again.isNearStuckedFish)
            {
                transform.localScale = new Vector3(0, 0, 0);
                freeFromNet = true;
                net_again.net_stucked = false;

                // Progress Bar according to level
                if (!(scene.name == "Level 5"))
                {
                    progressBar.BarValue += 10;
                }
                else
                {
                    progressBar.BarValue += 5;
                }

                scoreCollect += 1;
                scoreText.text = "Score: " + scoreCollect;

                // Score According to level
                if (!(scene.name == "Level 5") && scoreCollect >= 10)
                {
                    playerWon = true;
                    gameOver = true;
                }
                else if (scoreCollect >= 20)
                {
                    playerWon = true;
                    gameOver = true;
                }

            }
        }

        // If turtle get stucked again
        if (net_again.net_stucked)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            freeFromNet = false;
        }
    }

    // Check game over
    private void CheckGameOver()
    {
        // If game is over
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 2" && (textTimer.text == "Game Over!" || gameOver))
        {
            gameOver = true;


            if (scoreText.text == "Score: 10")
            {
                guideText.text = "Congratulations, level complete.\nPress \"0\" to go to next level";
            }
            else
            {
                guideText.text = "Alas, you lost.\nPress \"1\" to restart";
            }
        }
    }

    // Load next
    private void CheckNextLevel()
    {
        if (!gameOver)
        {
            return;
        }
        // If player Won and pressed 0, go to next level
        if (Input.GetKeyDown(KeyCode.Alpha0) && playerWon)
        {
            levelLoader.active = true;
            loadNextLevel();
        }
        // If player lost and pressed 1, play same level again
        else if (Input.GetKeyDown(KeyCode.Alpha1) && gameOver)
        {
            Scene scene = SceneManager.GetActiveScene();
            //print(scene.name[scene.name.Length-1]);
            int bar = scene.name[scene.name.Length - 1] - '0';
            SceneManager.LoadScene("Level " + bar);
        }
    }

    // Load next level after player is won
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
}