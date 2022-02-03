using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
  [SerializeField] private float timeRemaining; // total time of level
  private bool isTimerRunning = false;

  // TextMeshPro object for better font customisation
  private TextMeshProUGUI textTimer;

    public Button yourButton;

    public GameObject clear;
    // Start is called before the first frame update
    void Start()
  {
    // Initates the timer automatically
    textTimer = GetComponent<TextMeshProUGUI>();

    Button btn = yourButton.GetComponent<Button>();
    btn.onClick.AddListener(TaskOnClick);
  }

  // Update is called once per frame
  void Update()
  {
    if (textTimer.text == "Time Increased")
    {
      isTimerRunning = false;
      StartCoroutine(IncreaseTime(0.1f));
    }
    // Check if timer is still running
    if (isTimerRunning)
    {
      // if time is left, continue to decrement it
      if (timeRemaining > 0)
      {
        timeRemaining -= Time.deltaTime;
        DisplayTime(timeRemaining);
      }
      // time over, game over, reset to 0
      else
      {
        isTimerRunning = false;
        timeRemaining = 0;
        textTimer.text = string.Format("Game Over!");
      }

    }
    }

    void TaskOnClick()
    {
        clear.active = false;
        isTimerRunning = true;

    }

    private void DisplayTime(float timeToDisplay)
  {
    timeToDisplay += 1;

    // convert to mins and secs for display
    float minutes = Mathf.FloorToInt(timeToDisplay / 60);
    float seconds = Mathf.FloorToInt(timeToDisplay % 60);

    textTimer.text = string.Format("Time : {0:00}:{1:00}", minutes, seconds);
  }

  IEnumerator IncreaseTime(float newTime)
  {
    yield return new WaitForSeconds(1);

    timeRemaining += newTime;
    isTimerRunning = true;
    DisplayTime(timeRemaining);
  }
}
