using UnityEngine;

public class Seller : Interactable
{
    [SerializeField] protected SellingMenu sellingMenu;

    SpriteRenderer sprite;
    Transform playerTransform;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    protected override void Update()
    {
        base.Update();
        if (playerTransform.position.y > transform.position.y)
        {
            sprite.sortingOrder = 1;
        }
        else
        {
            sprite.sortingOrder = -1;
        }
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
