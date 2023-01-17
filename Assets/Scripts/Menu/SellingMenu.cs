using System.Collections;
using UnityEngine;

public class SellingMenu : MonoBehaviour
{
    [SerializeField] protected Seller seller;

    [SerializeField] GameObject firstItem;

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
