using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    void Start()
    {
        // Get next button component
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        // Proceed if 1 pressed or Enter key pressed
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Return))
        {
            TaskOnClick();
        }
    }

    // Load story mode level 1
    void TaskOnClick()
    {
        SceneManager.LoadScene("Level 1");
    }
}
