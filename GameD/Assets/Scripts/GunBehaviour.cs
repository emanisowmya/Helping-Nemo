using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{
  // Start is called before the first frame update
  public Button yourButton;
  public Sprite spr;
  public GameObject nemo;
  private bool isShootPermission = true;
  public SpriteRenderer nemow;
  void Start()
  {
    Button btn = yourButton.GetComponent<Button>();
    btn.onClick.AddListener(TaskOnClick);

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (isShootPermission)
      {
        TaskOnClick();
      }
    }

  }

  void TaskOnClick()
  {
    GameObject obj;
    float scale = 4f;

    obj = new GameObject("pulse1");
    Rigidbody rigid = obj.AddComponent<Rigidbody>();
    BoxCollider box = obj.AddComponent<BoxCollider>();
    SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();

    renderer.sprite = spr;
    rigid.useGravity = false;
    renderer.sortingOrder = 4;
    obj.transform.localScale = new Vector2(scale, scale);
    Vector2 pos = nemo.transform.position;
    obj.transform.position = pos;

    if (nemow.flipX == false)
      rigid.AddForce(Vector2.right * 50);
    else
    {
      renderer.flipX = true;
      rigid.AddForce(Vector2.left * 50);
    }

    /*
    Vector2 pos = nemo.transform.position;
    Vector2 position = obj.transform.position;


    int speed = 10;
    while(position.x < 9.09)
    {
        position.x +=  speed * Time.deltaTime;
        obj.transform.position = position;
    }
    */
    //print(spr);
    //print("yes");
  }
}
