using UnityEngine;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour
{
    public void OnOrderPending(PendingOrder order)
    {
        Debug.Log("OnOrderPending: " + order.Info.PurchasedProductInfo[0].productId);
        switch (order.Info.PurchasedProductInfo[0].productId)
        {
            case "thousand_coins":
                Debug.Log("Compraste mil monedas");
                break;
            case "two_thousand_coins":
                Debug.Log("Compraste 2 mil monedas");
                break;
            case "three_thousand_coins":
                Debug.Log("Compraste 3 mil monedas");
                break;
        }
    }
}
