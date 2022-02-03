using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    // Progess Bar
    public ProgressBar Pb;

    // Updating progress bar for each level
    void Update()
    {
        Pb.BarValue = 40;
    }
}
