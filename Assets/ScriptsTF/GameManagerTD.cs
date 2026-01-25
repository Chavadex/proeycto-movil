using UnityEngine;
using UnityEngine.UI;

public class GameManagerTD : MonoBehaviour
{

    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseContainer;
    [SerializeField] GameObject DefeatADSContainer;
    [SerializeField] GameObject DefeatNOADSContainer;
    [SerializeField] GameObject ShopContainer;
    [SerializeField] GameObject BoxShopContainer;
    [SerializeField] GameObject CoinsShopContainer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        PauseContainer.SetActive(true);
    }

    public void DefeatedADS()
    {
        Time.timeScale = 0;
        DefeatADSContainer.SetActive(true);
    }

    public void DefeatedNOADS()
    {
        Time.timeScale = 0;
        DefeatNOADSContainer.SetActive(true);
    }

    public void OpenShop()
    {
        
        ShopContainer.SetActive(true);
    }

    public void OpenBoxShop()
    {
        BoxShopContainer.SetActive(true);
    }

    public void OpenCoinsShop()
    {
        CoinsShopContainer.SetActive(true);

    }

    public void BackFromCoinShop()
    {
        CoinsShopContainer.SetActive(false);

    }
    public void BackFromBOXShop()
    {
        BoxShopContainer.SetActive(false);

    }

    public void BackFromShop()
    {
        ShopContainer.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        PauseContainer.SetActive(false);
    }

    public void Retry()
    {
        //Recargar Escena
    }

    public void MainMenu()
    {
        //Menu principal
    }
}
