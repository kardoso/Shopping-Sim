using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    protected List<int> items = new List<int>();

    public void AddItem(int itemId)
    {
        items.Add(itemId);
    }

    public void RemoveItem(int itemId)
    {
        items.Remove(itemId);
    }

    public bool ItemExists(int itemId)
    {
        return items.Contains(itemId);
    }

    public List<int> GetAllItems()
    {
        return items;
    }
}
