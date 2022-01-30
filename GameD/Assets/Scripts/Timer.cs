using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining; // total time of level
    private bool isTimerRunning = false;

    // TextMeshPro object for better font customisation
    private TextMeshProUGUI textTimer;


    // Start is called before the first frame update
    void Start()
    {
        // Initates the timer automatically
        isTimerRunning = true;
        textTimer = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        // convert to mins and secs for display
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textTimer.text = string.Format("Time : {0:00}:{1:00}", minutes, seconds);
    }
}
