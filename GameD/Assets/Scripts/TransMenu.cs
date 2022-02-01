using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button storyModeButton;
    void Start()
    {
        Button btn = storyModeButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Nemo Intro");
    }
}
