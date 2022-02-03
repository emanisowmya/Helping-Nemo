using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Net : MonoBehaviour
{

  [SerializeField]
  private GameObject net_stucked_fish;

  protected ProgressBar progressBar;

  protected Text guideText, scoreText;
  private TextMeshProUGUI textTimer;


  // Game won/lost vars
  private static float scoreCollect = 0;
  private bool playerWon = false, gameOver = false;

  [SerializeField]
  private NetStuckedFish net_again;

  [SerializeField]
  private MoveNemo nemo;

  public bool freeFromNet = false;

    public Animator transition;

    public GameObject levelLoader;

    void Awake()
  {
    progressBar = GameObject.Find("UI ProgressBar Animal Save").GetComponent<ProgressBar>();
    guideText = GameObject.Find("Instruction_text_Bg").GetComponent<Text>();
    scoreText = GameObject.Find("Score_text").GetComponent<Text>();
    textTimer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
  }

  // Start is called before the first frame update
  void Start()
  {
    scoreCollect = 0;
    progressBar.BarValue = 0;

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = net_stucked_fish.transform.position;
    //CheckGameOver();
    CheckKeyPress();
    CheckNextLevel();
    CheckGameOver();

    if(textTimer.text == "Time Increased"){
      scoreCollect = 0;
      progressBar.BarValue = 0;
      gameOver = false;
    }
  }

  private void CheckKeyPress()
  {

    if (gameOver)
    {
      return;
    }

    Scene scene = SceneManager.GetActiveScene();
    if (Input.GetKeyDown(KeyCode.Alpha3) && progressBar.BarValue < 100)
    {
      if (net_again.isNearStuckedFish)
      {
        transform.localScale = new Vector3(0, 0, 0);
        freeFromNet = true;
        net_again.net_stucked = false;

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
    if (net_again.net_stucked)
    {
      transform.localScale = new Vector3(0.3f, 0.3f, 1f);
      freeFromNet = false;
    }
  }

  private void CheckGameOver()
  {
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

    private void CheckNextLevel()
    {
        if (!gameOver)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && playerWon)
        {
            levelLoader.active = true;
            loadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && gameOver)
        {
            Scene scene = SceneManager.GetActiveScene();
            //print(scene.name[scene.name.Length-1]);
            int bar = scene.name[scene.name.Length - 1] - '0';
            SceneManager.LoadScene("Level " + bar);
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
    }
}
