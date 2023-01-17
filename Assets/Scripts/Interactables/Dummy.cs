using UnityEngine;

public class Dummy : Interactable
{
    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (isCloseEnoughToPlayer)
        {
            Debug.Log("Interaction!!");
        }
    }
}
