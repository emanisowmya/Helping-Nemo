using UnityEngine;
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
        btn.onClick.AddListener(CreatePulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isShootPermission)
            {
                CreatePulse();
            }
        }

    }

    void CreatePulse()
    {
        GameObject obj;
        float scale = 4f;

        obj = new GameObject("pulse1");
        Rigidbody2D rigid = obj.AddComponent<Rigidbody2D>();
        BoxCollider2D box = obj.AddComponent<BoxCollider2D>();
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = spr;
        rigid.gravityScale = 0;
        renderer.sortingOrder = 3;
        obj.transform.localScale = new Vector2(scale, scale);
        Vector2 pos = nemo.transform.position;
        obj.transform.position = pos;
        rigid.freezeRotation = true;

        box.size = new Vector2(0.2f, 0.2f);

        if (nemow.flipX == false)
        {
            //rigid.AddForce(Vector2.right * 50);
            rigid.velocity = new Vector2(10f, 0f);
            box.offset = new Vector2(0.2f, 0f);
        }
        else
        {
            renderer.flipX = true;
            //rigid.AddForce(Vector2.left * 50);
            rigid.velocity = new Vector2(-10f, 0f);
            box.offset = new Vector2(-0.2f, 0f);
        }
    }

}
