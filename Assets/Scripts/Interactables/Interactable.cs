using UnityEngine;
using System.Collections.Generic;

public abstract class Interactable : MonoBehaviour
{
    protected float checkPlayerRadius = 1.5f;
    [SerializeField] GameObject interactionIndicator;
    protected PlayerController player;
    protected bool isCloseEnoughToPlayer = false;
    protected bool isShowingDialog = false;

    protected virtual void Update() {
        CheckForPlayer();
    }

    public abstract void Interact();

    protected void CheckForPlayer()
    {
        var playerObjects = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        var _player = playerObjects.Find(delegate (PlayerController p)
        {
            return (p.transform.position - this.transform.position)
                .magnitude <= checkPlayerRadius;
        });
        if (_player != null) {
            isCloseEnoughToPlayer = true;
            if (!isShowingDialog) {
                interactionIndicator.SetActive(true);
            } else {
                interactionIndicator.SetActive(false);
            }
        } else {
            isCloseEnoughToPlayer = false;
            interactionIndicator.SetActive(false);
        }

        player = _player;
    }
}
