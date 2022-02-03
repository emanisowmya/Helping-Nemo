using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button storyModeButton;
    [SerializeField] private Button survivalModeButton;
    void Start()
    {
        //load story mode when clicked
        Button btnStory = storyModeButton.GetComponent<Button>();
        btnStory.onClick.AddListener(StoryModeOnClick);

        //load survival mode when clicked
        Button btnSurvival = survivalModeButton.GetComponent<Button>();
        btnSurvival.onClick.AddListener(SurvivalModeOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        // for story mode we can click button or press 1 or press Enter
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Return))
        {
            StoryModeOnClick();
        }
        // for survival mode we can click button or press 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SurvivalModeOnClick();
        }
    }

    // Loads the next Story scene of nemo intro
    void StoryModeOnClick()
    {
        SceneManager.LoadScene("Nemo Intro");
    }

    // Loads Survival mode
    void SurvivalModeOnClick()
    {
        SceneManager.LoadScene("SurvivalLevel");
    }

    // For desktop applications quits game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
