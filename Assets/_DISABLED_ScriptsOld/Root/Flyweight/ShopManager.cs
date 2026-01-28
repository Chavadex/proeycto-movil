using UnityEngine;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour
{
    CoinManager _coinManager;

    private void Awake()
    {
        _coinManager = FindFirstObjectByType<CoinManager>();
    }

    public void OnOrderPending(PendingOrder order)
    {
        Debug.Log("OnOrderPending: " + order.Info.PurchasedProductInfo[0].productId);
        switch (order.Info.PurchasedProductInfo[0].productId)
        {
            case "thousand_coins":
                _coinManager.AddCoins(50);
                Debug.Log("Compraste 50 monedas");
                break;
            case "two_thousand_coins":
                _coinManager.AddCoins(100);
                Debug.Log("Compraste 100 monedas");
                break;
            case "three_thousand_coins":
                _coinManager.AddCoins(200);
                Debug.Log("Compraste 200 monedas");
                break;
            case "four_thousand_coins":
                _coinManager.AddCoins(500);
                Debug.Log("Compraste 50 monedas");
                break;
        }
    }
}
