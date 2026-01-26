using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerTD : MonoBehaviour
{

    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseContainer;
    [SerializeField] GameObject DefeatADSContainer;
    [SerializeField] GameObject DefeatNOADSContainer;
    [SerializeField] GameObject ShopContainer;
    [SerializeField] GameObject BoxShopContainer;
    [SerializeField] GameObject CoinsShopContainer;

    [SerializeField] bool hasDead;
    WaveManager _waveManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _waveManager = FindFirstObjectByType<WaveManager>();
        hasDead = false;
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
        
        PausePanel.SetActive(true);
        DefeatADSContainer.SetActive(true);
        hasDead = true;
    }

    public void DefeatedNOADS()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
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
        DefeatADSContainer.SetActive(false);
        DefeatNOADSContainer.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _waveManager.ResetWavesFromStart();
    }

    public void MainMenu()
    {
        //Menu principal
    }

    public void checkIfFirstDead()
    {
        if (!hasDead)
        {
            DefeatedADS();
        }
        else
        {
            DefeatedNOADS();
        }
    }
}
