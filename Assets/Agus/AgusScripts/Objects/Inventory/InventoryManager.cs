using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory State")]
    [SerializeField] private List<string> ownedItemIds = new List<string>();
    private int currentIndex = 0;

    public delegate void OnItemChanged(InventoryItemData newItem);
    public event OnItemChanged ItemChanged;

    public Action<string> OnItemUsedExternally;

    public InventoryItemData CurrentItem
    {
        get
        {
            if (!HasItems()) return null;
            string id = ownedItemIds[currentIndex];
            return ItemDatabase.Instance.GetItemById(id);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddItem(string itemId)
    {
        if (!ownedItemIds.Contains(itemId))
        {
            ownedItemIds.Add(itemId);
            currentIndex = ownedItemIds.Count - 1;
            ItemChanged?.Invoke(CurrentItem);
            Debug.Log($"[Inventory] Collected item: {itemId}");
        }
    }

    public void NextItem()
    {
        if (!HasItems()) return;

        currentIndex = (currentIndex + 1) % ownedItemIds.Count;
        ItemChanged?.Invoke(CurrentItem);
    }

    public void PreviousItem()
    {
        if (!HasItems()) return;

        currentIndex = (currentIndex - 1 + ownedItemIds.Count) % ownedItemIds.Count;
        ItemChanged?.Invoke(CurrentItem);
    }

    public bool HasItems()
    {
        return ownedItemIds.Count > 0;
    }

    public bool HasItem(string itemId)
    {
        return ownedItemIds.Contains(itemId);
    }

    public List<string> GetAllOwnedItemIds()
    {
        return ownedItemIds;
    }

    public void RemoveItem(string itemId)
    {
        var item = ownedItemIds.Find(i => i == itemId);
        if (item != null)
        {
            ownedItemIds.Remove(item);
            currentIndex = Mathf.Clamp(currentIndex, 0, ownedItemIds.Count - 1);
            ItemChanged?.Invoke(CurrentItem);
        }
    }

    public void UseCurrentItem()
    {
        Debug.Log("TRIED TO USE THE ITEM");
        if (CurrentItem == null || !CurrentItem.isUsable) return;

        Debug.Log($"[Inventory] Used item: {CurrentItem.displayName}");

        OnItemUsedExternally?.Invoke(CurrentItem.itemID);
    }

    public int GetCurrentIndex() => currentIndex;
}