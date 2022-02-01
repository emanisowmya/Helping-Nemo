using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Net : MonoBehaviour
{

    [SerializeField]
    private GameObject net_stucked_fish;

   /*[SerializeField]
    private ProgressBar Pb;

    [SerializeField]
    private Text scoreText;

    private int scoreCollect = 0;*/

    [SerializeField]
    private NetStuckedFish net_again;

    [SerializeField]
    private MoveNemo nemo;

    public bool freeFromNet = false;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = net_stucked_fish.transform.position;
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (net_again.isNearStuckedFish)
            {
                transform.localScale = new Vector3(0, 0, 0);
                freeFromNet = true;
                net_again.net_stucked = false;
                nemo.scoreCollect += 1;
                nemo.scoreText.text = "Score: " + nemo.scoreCollect;
                if (nemo.scoreCollect >= 20)
                {
                    nemo.playerWon = true;
                    nemo.gameOver = true;
                }
            }
        }
        if (net_again.net_stucked)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            freeFromNet = false;
        }
    }
}
