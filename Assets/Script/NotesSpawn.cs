using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSpawn : MonoBehaviour
{
    // List of spawn points
    public Transform[] spawnPoints;

    // List of prefabs to spawn
    public GameObject[] prefabs;

    // Control the randomness (seed)
    public int randomnessSeed = 0;

    // Rotation to apply to each spawned prefab
    public Vector3 spawnRotation;

    // Scale to apply to each spawned prefab
    public Vector3 spawnScale = Vector3.one;

    // Use this for initialization
    void Start()
    {
        // Initialize the random number generator with the provided seed
        Random.InitState(randomnessSeed);

        // Shuffle the spawn points array
        Shuffle(spawnPoints);

        // Randomly decide how many spawn points to use (between 0 and the total number of spawn points)
        int numSpawnPointsToUse = Random.Range(0, spawnPoints.Length + 1);

        // Ensure we don't use more spawn points than there are prefabs
        numSpawnPointsToUse = Mathf.Min(numSpawnPointsToUse, prefabs.Length);

        // Shuffle the prefabs array
        Shuffle(prefabs);

        // Iterate over the selected number of spawn points
        for (int i = 0; i < numSpawnPointsToUse; i++)
        {
            // Select a random prefab from the shuffled list
            GameObject prefabToSpawn = prefabs[i];

            // Instantiate the prefab at the spawn point's position
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPoints[i].position, Quaternion.Euler(spawnRotation));

            // Set the scale of the spawned prefab
            spawnedPrefab.transform.localScale = spawnScale;

            // Set the spawned prefab as a child of the current spawn point
            spawnedPrefab.transform.parent = spawnPoints[i];
        }
    }

    // Function to shuffle an array of Transforms
    void Shuffle(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Function to shuffle an array of GameObjects
    void Shuffle(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
