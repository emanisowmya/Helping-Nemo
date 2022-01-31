using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected Button yourButton;

    [SerializeField]
    protected GameObject instruction;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        instruction.active = false;
    }
}
