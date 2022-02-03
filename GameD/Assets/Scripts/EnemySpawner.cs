using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField]
  private GameObject[] Garbages;    // Game Object Garbage List

  private GameObject spawnedGarbage;    // Game Object Garbage

  [SerializeField]
  private Transform leftPos, rightPos;      // End position for garbage to spawn
  private Vector3 randomDistance;           // Random distance from end 

  private int randomIndex, randomSide;      // Random Side

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(SpawnGarbages());    // Spawning Garbages
  }

  IEnumerator SpawnGarbages()
  {
        // Until Game ends, spawn garbages
    while (true)
    {
      yield return new WaitForSeconds(Random.Range(2, 6));      // Spawn Garbages at some time interval

        // Random position of garbage spawning
      randomIndex = Random.Range(0, Garbages.Length);
      randomSide = Random.Range(0, 2);
      randomDistance = new Vector3(Random.Range(0, 35), Random.Range(0, 5), 0f);

      spawnedGarbage = Instantiate(Garbages[randomIndex]);

    // Random spawning of garbage according to side
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
