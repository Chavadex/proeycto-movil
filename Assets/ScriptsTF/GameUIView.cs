using UnityEngine;

public class GameUIView : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel; 
    [SerializeField] private GameObject pauseContainer;

    [Header("Defeat Screens")]
    [SerializeField] private GameObject defeatADSContainer;
    [SerializeField] private GameObject defeatNOADSContainer;

    [Header("Shops")]
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject boxShopContainer;
    [SerializeField] private GameObject coinsShopContainer;

    public void TogglePauseUI(bool isActive)
    {
        if (pausePanel != null)
            pausePanel.SetActive(isActive);
        if (pauseContainer != null)
            pauseContainer.SetActive(isActive);
    }

    public void ShowDefeatAds()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
        if (defeatADSContainer != null)
            defeatADSContainer.SetActive(true);
        if (defeatNOADSContainer != null)
            defeatNOADSContainer.SetActive(false);
    }

    public void ShowDefeatNoAds()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
        if (defeatNOADSContainer != null)
            defeatNOADSContainer.SetActive(true);
        if (defeatADSContainer != null)
            defeatADSContainer.SetActive(false);
    }

    public void ShowShop(bool isOpen)
    {
        if (shopContainer != null)
            shopContainer.SetActive(isOpen);
    }

    public void ShowBoxShop(bool isOpen)
    {
        if (boxShopContainer != null)
            boxShopContainer.SetActive(isOpen);
    }

    public void ShowCoinsShop(bool isOpen)
    {
        if (coinsShopContainer != null)
            coinsShopContainer.SetActive(isOpen);
    }

    public void HideAllPopups()
    {
        if (pauseContainer != null)
            pauseContainer.SetActive(false);
        if (defeatADSContainer != null)
            defeatADSContainer.SetActive(false);
        if (defeatNOADSContainer != null)
            defeatNOADSContainer.SetActive(false);
        if (shopContainer != null)
            shopContainer.SetActive(false);
        if (boxShopContainer != null)
            boxShopContainer.SetActive(false);
        if (coinsShopContainer != null)
            coinsShopContainer.SetActive(false);
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
}