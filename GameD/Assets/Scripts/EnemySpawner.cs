using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField]
  private GameObject[] Garbages;

  private GameObject spawnedGarbage;

  [SerializeField]
  private Transform leftPos, rightPos;
  private Vector3 randomDistance;

  private int randomIndex, randomSide;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(SpawnGarbages());
  }

  IEnumerator SpawnGarbages()
  {
    while (true)
    {
      yield return new WaitForSeconds(Random.Range(2, 6));

      randomIndex = Random.Range(0, Garbages.Length);
      randomSide = Random.Range(0, 2);
      randomDistance = new Vector3(Random.Range(0, 35), Random.Range(0, 5), 0f);

      spawnedGarbage = Instantiate(Garbages[randomIndex]);

      if (randomSide == 0)
      {
        spawnedGarbage.transform.position = leftPos.position + randomDistance;
      }
      else
      {
        spawnedGarbage.transform.position = rightPos.position - randomDistance;
      }

    }
  }
}
