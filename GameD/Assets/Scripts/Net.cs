using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{

    [SerializeField]
    private GameObject net_stucked_fish;

    [SerializeField]
    private MoveNemo nemo;

    [SerializeField]
    private NetStuckedFish net_again;

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
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (nemo.isNearStuckedFish)
            {
                transform.localScale = new Vector3(0, 0, 0);
                freeFromNet = true;
            }
        }
        if (net_again.net_stucked)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            freeFromNet = false;
        }
    }
}
