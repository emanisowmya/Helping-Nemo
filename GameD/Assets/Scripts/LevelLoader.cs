using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Scene scene = SceneManager.GetActiveScene();
                //print(scene.name[scene.name.Length-1]);
                int bar = scene.name[scene.name.Length - 1] - '0';
                if (bar < 5)
                    SceneManager.LoadScene("Level " + (bar + 1));
            }
    }
}
