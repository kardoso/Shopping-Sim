using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] protected List<int> items = new List<int>();
    [SerializeField] protected int coins = 5000;
    [SerializeField] protected TMP_Text coinText;

    void Start()
    {
        coinText.text = coins.ToString();
    }

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

    public void AddCoins(int amount)
    {
        coins += amount;
        coinText.text = coins.ToString();
    }

    // will return false if there's not enough coins to remove
    // or true if there is enough coins (and it will remove)
    public bool RemoveCoins(int amount)
    {
        if (coins - amount >= 0)
        {
            coins = coins - amount;
            coinText.text = coins.ToString();
            return true;
        }

        return false;
    }
}
