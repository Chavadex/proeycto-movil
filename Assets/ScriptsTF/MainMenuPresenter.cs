using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPresenter : MonoBehaviour
{
    [Header("View Reference")]
    [SerializeField] private MainMenuView uiView;

    [Header("Scene Configuration")]
    [SerializeField] private string gameSceneName = "GameScene";

    private void Start()
    {
        uiView.ShowMainPanel();

        if (CoinManager.Instance != null)
        {
            uiView.UpdateCoinsText(CoinManager.Instance.Coins);
        }
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnCreditsPressed()
    {
        uiView.ShowCredits();
    }

    public void OnCreditsHide()
    {
        uiView.HideCredits();
    }

    public void OnBackToMainPressed()
    {
        uiView.ShowMainPanel();
    }

    public void OnQuitPressed()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    public void OnResetDataPressed()
    {
        PlayerPrefs.DeleteAll();
        if (CoinManager.Instance != null) CoinManager.Instance.ResetCoins();
        Debug.Log("Datos borrados");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
