using UnityEngine;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;           // Gun Button
    public Sprite spr;                  // Gun Sprite
    public GameObject nemo;             // Game Object Nemp
    private bool isShootPermission = true;  // Shooting functionality available or not
    public SpriteRenderer nemow;            // Nemo Sprite Renderer

    AudioSource gun;                    // Shoot sound

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();         // Gun button
        btn.onClick.AddListener(CreatePulse);                   // On clicking gun button, create pulse
        gun = GetComponent<AudioSource>();                      // Audio for shoot
        gun.volume = 0.5f;                                      // Volume for audio of shoot
        gun.Stop();                                             // Stop audio

    }

    // Update is called once per frame
    void Update()
    {
        // If shooting functionality is there, and key press is Space, create pulse and shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isShootPermission)
            {
                CreatePulse();
                gun.Play();
            }
        }

    }

    // Creating Pulse
    void CreatePulse()
    {
        GameObject obj;
        float scale = 4f;

        // Pulse Functionalit
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

        // Checking direction of pulse shoot
        if (nemow.flipX == false)
        {
            rigid.velocity = new Vector2(10f, 0f);
            box.offset = new Vector2(0.2f, 0f);
        }
        else
        {
            renderer.flipX = true;
            rigid.velocity = new Vector2(-10f, 0f);
            box.offset = new Vector2(-0.2f, 0f);
        }
    }

}
