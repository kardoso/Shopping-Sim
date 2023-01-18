using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellingMenu : MonoBehaviour
{
    [SerializeField] protected Seller seller;
    [SerializeField] GameObject firstSelect;
    GameObject lastSelect;

    void Start()
    {
        lastSelect = firstSelect;
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void CloseStore()
    {
        StartCoroutine(DelayToCloseStore());
    }

    // This prevents store to open right after closed
    IEnumerator DelayToCloseStore() {
        yield return new WaitForEndOfFrame();
        seller.FinishInteraction();
    }
}
