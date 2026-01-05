using UnityEngine;
using UnityEngine.Purchasing;
    public class IAPExample : MonoBehaviour
    {
        public void OnOrderPending(PendingOrder order)
        {
            Debug.Log("OnOrderPending: " + order.Info.PurchasedProductInfo[0].productId);
            switch (order.Info.PurchasedProductInfo[0].productId)
            {
            case "iap_health_upgrade":
                //Grant the health upgrade to the player
                Debug.Log("Health upgrade granted!");
                break;
            case "iap_no_ads":
                //disable ads
                break;
            }
        }
    }

