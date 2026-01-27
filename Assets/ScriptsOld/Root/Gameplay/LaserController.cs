using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class LaserController : MonoBehaviour
{
    
    [Header("Variables del laser")]
    [SerializeField] float laserCooldown = 2f;
    [SerializeField] float laserDuration = 1f;
    [SerializeField] int lasersLeft = 3;
    [SerializeField] GameObject laserObject;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI lasersLeftText;
    [SerializeField] GameObject laserIsAvailableButton;
    [SerializeField] GameObject laserIsNOTAvailableButton;
    [SerializeField] GameObject adsButton;

    [Header("Booleanos")]
    [SerializeField] bool canActiveLaser = true;

    float cooldownTimer = 0f;

    void Start()
    {

        bool noAdsPurchased = PlayerPrefs.GetInt("no_ads", 0) == 1;

        if (noAdsPurchased)
        {
            adsButton.SetActive(false);
        }
        else
        {
            adsButton.SetActive(true);
        }



        UpdateLaserUI();
        laserObject.SetActive(false);
    }

    void Update()
    {
        CheckIfCanLaser();
        UpdateLaserUI();
    }

    public void TryActivateLaser()
    {
        if (!canActiveLaser) return;
        if (lasersLeft <= 0) return;

        lasersLeft--;
        UpdateLaserUI();

        StartCoroutine(LaserRoutine());
    }

    IEnumerator LaserRoutine()
    {
        canActiveLaser = false;

        laserObject.SetActive(true);
        yield return new WaitForSeconds(laserDuration);
        laserObject.SetActive(false);

        cooldownTimer = laserCooldown;
        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        canActiveLaser = true;
        UpdateLaserUI();
    }

    void CheckIfCanLaser()
    {
        if (canActiveLaser && lasersLeft > 0)
        {
            laserIsAvailableButton.SetActive(true);
            laserIsNOTAvailableButton.SetActive(false);
        }
        else
        {
            laserIsAvailableButton.SetActive(false);
            laserIsNOTAvailableButton.SetActive(true);
        }
    }

    void UpdateLaserUI()
    {
        lasersLeftText.text = lasersLeft.ToString();
    }

    public void addLaser()
    {
        lasersLeft += 10;
    }

    public void addOneLaser()
    {
        lasersLeft += 1;
    }
    public void addLaserDouble()
    {
        lasersLeft += 20;
    }

    public void addLaserAdsReward()
    {
        lasersLeft += 20;
    }

    public void disableAdsButton()
    {
        adsButton.SetActive(false);
    }
    
    public void OnOrderPending(PendingOrder order)
    {
        Debug.Log("OnOrderPending: " + order.Info.PurchasedProductInfo[0].productId);
        switch (order.Info.PurchasedProductInfo[0].productId)
        {
            
            case "iap_laser_upgrade":
                Debug.Log("Se compro 10 laser");
                addLaser();
                UpdateLaserUI();
                break;
            case "iap_laser2_upgrade":
                Debug.Log("Se compro 20 laser");
                Debug.Log("Compraste la 2");
                
                addLaserDouble();
                UpdateLaserUI();
                break;
            case "no_ads":
                Debug.Log("No mas ads");
                PlayerPrefs.SetInt("no_ads", 1);
                addLaserAdsReward();
                UpdateLaserUI();
                disableAdsButton();
                break;
            case "Ejemplo":
                Debug.Log("Compraste mil monedas");
                break;
        }
    }
}
