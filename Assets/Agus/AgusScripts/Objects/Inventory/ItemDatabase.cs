using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [Header("All Available Items")]
    [SerializeField] private List<InventoryItemData> allItems;

    private Dictionary<string, InventoryItemData> itemLookup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // opcional si querés mantenerlo entre escenas

        BuildLookup();
    }

    private void BuildLookup()
    {
        itemLookup = new Dictionary<string, InventoryItemData>();

        foreach (var item in allItems)
        {
            if (item != null && !string.IsNullOrEmpty(item.itemID))
            {
                if (!itemLookup.ContainsKey(item.itemID))
                {
                    itemLookup.Add(item.itemID, item);
                }
                else
                {
                    Debug.LogWarning($"Duplicate item ID found in database: {item.itemID}");
                }
            }
        }
    }

    public InventoryItemData GetItemById(string id)
    {
        if (itemLookup.TryGetValue(id, out var item))
        {
            return item;
        }

        Debug.LogWarning($"Item ID not found: {id}");
        return null;
    }

    public List<InventoryItemData> GetAllItems()
    {
        return allItems;
    }
}
