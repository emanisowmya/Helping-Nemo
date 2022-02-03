using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For moving camera
public class MoveCamera : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField]
  float xOffset;        // Horizontal off set

  [SerializeField]
  float yOffset;        // vertical off set

  [SerializeField]
  protected Transform trackingTarget;   // tracking target

  [SerializeField]
  protected float followSpeed;  // Follow Speed

  [SerializeField]
  protected bool isXLocked = false;     // locking x axis of camera

  [SerializeField]
  protected bool isYLocked = false;     // locking y axis of camera

  void Start()
  {

  }

  // Update is called once per frame=

  void Update()
  {
        // Positioning camera in horizontal and vertical axis
    float xTarget = trackingTarget.position.x + xOffset;
    float yTarget = trackingTarget.position.y + yOffset;


        // Camera following Nemo until its reaches end
    if (trackingTarget.position.x > -24.59974 && trackingTarget.position.x < 24.59974)
    {
      float xNew = transform.position.x;
      if (!isXLocked)
      {
        xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
      }

      float yNew = transform.position.y;
      if (!isYLocked)
      {
        yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);
      }

      transform.position = new Vector3(xNew, yNew, transform.position.z);
    }
  }
}
