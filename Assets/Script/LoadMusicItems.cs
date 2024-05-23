using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadMusicItems : MonoBehaviour
{
    public MusicPlayer musicPlayer;
    public GameObject togglePrefab;

    private List<Types.Item> currentItems = new List<Types.Item>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (musicPlayer.items.Count == 0) return;

        if (currentItems.SequenceEqual(musicPlayer.items)) return;

        // check if just required to add more items
        if (currentItems.Count < musicPlayer.items.Count)
        {
            List<Types.Item> newItems = musicPlayer.items.Skip(currentItems.Count).ToList();

            InstantiateAndAddMusicItems(newItems, currentItems.Count);
            // check if playlist is changed & required to remove all items and add new ones
        }
        else
        {
            RemoveAllMusicItems();
            InstantiateAndAddMusicItems(musicPlayer.items, 0);
        }

        currentItems = new List<Types.Item>(musicPlayer.items);
    }

    void RemoveAllMusicItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // use this method to load more music items, pass on items to render and indices as per the original List
    void InstantiateAndAddMusicItems(List<Types.Item> items, int initialOriginalIndex)
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject toggle = Instantiate(togglePrefab, transform);

            if (toggle.TryGetComponent<RenderToggleItem>(out var renderToggleItem))
            {
                renderToggleItem.musicPlayer = musicPlayer;
                renderToggleItem.item = items[i];
                renderToggleItem.index = initialOriginalIndex + i;
            }
            else
            {
                Debug.Log("RenderToggleItem is null");
            }
        }
    }
}
