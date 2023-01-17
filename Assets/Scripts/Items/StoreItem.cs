using UnityEngine;
using TMPro;

public class StoreItem : MonoBehaviour
{
    [SerializeField] int id = 1;
    [SerializeField] int price = 200;
    [SerializeField] TMP_Text priceText;
    PlayerInventory playerInventory;
    bool isOnInventory = false;
    [SerializeField] GameObject itemOnInventoryOptions;
    [SerializeField] GameObject itemNotOnInventoryOptions;
    private GameObject eventSystemGO;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
        playerInventory = FindObjectOfType<PlayerInventory>();
        eventSystemGO = GameObject.Find("EventSystem");
    }

    public void BuyItem()
    {
        var enoughCoins = playerInventory.RemoveCoins(price);
        if (enoughCoins)
        {
            playerInventory.AddItem(id);
            isOnInventory = true;
        }
        UpdateItemStatus();
    }

    public void SellItem()
    {
        if (playerInventory.ItemExists(id))
        {
            isOnInventory = false;
            playerInventory.AddCoins(price);
            playerInventory.RemoveItem(id);
        }
        UpdateItemStatus();
    }

    public void WearItem()
    {
        Debug.Log("Use");
    }

    void UpdateItemStatus()
    {
        if (isOnInventory)
        {
            itemNotOnInventoryOptions.SetActive(false);
            itemOnInventoryOptions.SetActive(true);
            var selectOption = itemOnInventoryOptions.transform.GetChild(1).gameObject;
            eventSystemGO.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(selectOption);
        }
        else if (!isOnInventory)
        {
            itemOnInventoryOptions.SetActive(false);
            itemNotOnInventoryOptions.SetActive(true);
            var selectOption = itemNotOnInventoryOptions.transform.GetChild(1).gameObject;
            eventSystemGO.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(selectOption);
        }
    }
}
