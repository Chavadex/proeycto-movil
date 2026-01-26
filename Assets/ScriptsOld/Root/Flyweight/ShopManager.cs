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
                _coinManager.AddCoins(1000);
                Debug.Log("Compraste mil monedas");
                break;
            case "two_thousand_coins":
                _coinManager.AddCoins(2000);
                Debug.Log("Compraste 2 mil monedas");
                break;
            case "three_thousand_coins":
                _coinManager.AddCoins(3000);
                Debug.Log("Compraste 3 mil monedas");
                break;
            case "four_thousand_coins":
                _coinManager.AddCoins(4000);
                Debug.Log("Compraste 4 mil monedas");
                break;
        }
    }
}
