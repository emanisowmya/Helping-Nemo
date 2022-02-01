using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Return))
        {
            TaskOnClick();
        }
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Level 1");
    }
}
