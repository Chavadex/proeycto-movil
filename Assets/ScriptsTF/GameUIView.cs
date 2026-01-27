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
        pausePanel.SetActive(isActive);
        pauseContainer.SetActive(isActive);
    }

    public void ShowDefeatAds()
    {
        pausePanel.SetActive(true);
        defeatADSContainer.SetActive(true);
        defeatNOADSContainer.SetActive(false);
    }

    public void ShowDefeatNoAds()
    {
        pausePanel.SetActive(true);
        defeatNOADSContainer.SetActive(true);
        defeatADSContainer.SetActive(false);
    }

    public void ShowShop(bool isOpen)
    {
        shopContainer.SetActive(isOpen);
    }

    public void ShowBoxShop(bool isOpen)
    {
        boxShopContainer.SetActive(isOpen);
    }

    public void ShowCoinsShop(bool isOpen)
    {
        coinsShopContainer.SetActive(isOpen);
    }

    public void HideAllPopups()
    {
        pauseContainer.SetActive(false);
        defeatADSContainer.SetActive(false);
        defeatNOADSContainer.SetActive(false);
        shopContainer.SetActive(false);
        boxShopContainer.SetActive(false);
        coinsShopContainer.SetActive(false);
        pausePanel.SetActive(false);
    }
}