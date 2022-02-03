using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // If key 0 is pressed, load next level
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Scene scene = SceneManager.GetActiveScene();
                int bar = scene.name[scene.name.Length - 1] - '0';
                if (bar < 5)
                    SceneManager.LoadScene("Level " + (bar + 1));
            }
    }
}
