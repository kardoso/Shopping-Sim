using UnityEngine;

public class Seller : Interactable
{
    [SerializeField] protected SellingMenu sellingMenu;

    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (isCloseEnoughToPlayer && !isShowingDialog)
        {
            isShowingDialog = true;
            player.DisableMovement();
            sellingMenu.gameObject.SetActive(true);
        }
    }

    public void FinishInteraction()
    {
        sellingMenu.gameObject.SetActive(false);
        isShowingDialog = false;
        player.EnableMovement();
    }
}
