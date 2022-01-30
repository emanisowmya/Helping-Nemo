using UnityEngine;

namespace VolumetricLines
{
	public class VolumetricLineStripBehavior : MonoBehaviour 
	{
		// Used to compute the average value of all the Vector3's components:
		static readonly Vector3 Average = new Vector3(1f/3f, 1f/3f, 1f/3f);
        private SpriteRenderer _renderer;
        private int speed = 5;
        public GameObject nemo;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = nemo.transform.position;
            if (Input.GetKey(KeyCode.Space))
            {
                transform.localScale += new Vector3(0, Time.deltaTime * speed * 0.1f, 0);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            }
        }
    }
}