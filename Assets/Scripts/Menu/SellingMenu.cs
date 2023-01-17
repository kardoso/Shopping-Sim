using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingMenu : MonoBehaviour
{
    [SerializeField] protected Seller seller;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seller.FinishInteraction();
        }
    }
}
