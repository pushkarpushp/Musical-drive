using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSpawn : MonoBehaviour
{
    // List of spawn points
    public List<Transform> spawnPoints;

    // List of prefabs to spawn
    public List<GameObject> prefabs;

    // Public variables to set rotation and scale of the spawned prefabs
    public Vector3 prefabRotation = Vector3.zero;
    public Vector3 prefabScale = Vector3.one;

    // Reference to the road transform
    public Transform roadTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure we do not spawn more prefabs than there are spawn points
        int numSpawnLocations = Mathf.Min(spawnPoints.Count, prefabs.Count);

        // Shuffle spawn points and pick the required number
        List<Transform> selectedSpawnPoints = GetRandomElements(spawnPoints, numSpawnLocations);

        // Shuffle prefabs and pick the required number
        List<GameObject> selectedPrefabs = GetRandomElements(prefabs, numSpawnLocations);

        // Spawn prefabs at selected spawn points
        for (int i = 0; i < numSpawnLocations; i++)
        {
            Vector3 spawnPosition = selectedSpawnPoints[i].position;
            Quaternion spawnRotation = Quaternion.Euler(prefabRotation);

            GameObject instance = Instantiate(selectedPrefabs[i], spawnPosition, spawnRotation);
            instance.transform.localScale = prefabScale;

            // Parent the prefab to the road transform
            instance.transform.SetParent(roadTransform);

            // Log the position and name of the instantiated object for debugging
            Debug.Log($"Spawned {instance.name} at {spawnPosition}");
        }

        gameObject.SetActive(false);
    }

    // Helper method to get a random subset of elements from a list
    private List<T> GetRandomElements<T>(List<T> list, int count)
    {
        // Create a copy of the list
        List<T> copyList = new List<T>(list);

        // Shuffle the copy
        for (int i = 0; i < copyList.Count; i++)
        {
            T temp = copyList[i];
            int randomIndex = Random.Range(i, copyList.Count);
            copyList[i] = copyList[randomIndex];
            copyList[randomIndex] = temp;
        }

        // Return the required number of elements
        return copyList.GetRange(0, Mathf.Min(count, copyList.Count));
    }
}
