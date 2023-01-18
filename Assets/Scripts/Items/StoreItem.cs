using UnityEngine;
using TMPro;

public class StoreItem : MonoBehaviour
{
    [SerializeField] int id = 1;
    [SerializeField] int price = 200;
    [SerializeField] TMP_Text priceText;
    PlayerInventory playerInventory;
    PlayerController playerController;
    bool isOnInventory = false;
    [SerializeField] GameObject itemOnInventoryOptions;
    [SerializeField] GameObject itemNotOnInventoryOptions;
    private GameObject eventSystemGO;

    [SerializeField] StoreItemType itemType;
    [SerializeField] RuntimeAnimatorController itemAnimator;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerController = FindObjectOfType<PlayerController>();
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
            CheckIfItemIsBeingUsedAndChangeToDefault();
        }
        UpdateItemStatus();
    }

    public void CheckIfItemIsBeingUsedAndChangeToDefault()
    {
        switch (itemType)
        {
            case StoreItemType.Head:
                if (playerController.CurrentHeadItemId == id) playerController.ChangeHead();
                return;

            case StoreItemType.Torso:
                if (playerController.CurrentTorsoItemId == id) playerController.ChangeTorso();
                return;

            case StoreItemType.Legs:
                if (playerController.CurrentLegsItemId == id) playerController.ChangeLegs();
                return;
        }
    }

    public void WearItem()
    {
        switch (itemType)
        {
            case StoreItemType.Head:
                playerController.ChangeHead(id, itemAnimator);
                return;

            case StoreItemType.Torso:
                playerController.ChangeTorso(id, itemAnimator);
                return;

            case StoreItemType.Legs:
                playerController.ChangeLegs(id, itemAnimator);
                return;
        }
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

public enum StoreItemType
{
    Head = 1,
    Torso = 2,
    Legs = 3,
};
