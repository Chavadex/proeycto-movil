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
        try
        {
            if (uiView != null)
            {
                uiView.ShowMainPanel();

                // Intentar actualizar las monedas de forma segura
                if (CoinManager.Instance != null)
                {
                    uiView.UpdateCoinsText(CoinManager.Instance.Coins);
                }
                else
                {
                    // Si CoinManager no existe, mostrar 0 como valor por defecto
                    uiView.UpdateCoinsText(0);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error en MainMenuPresenter.Start(): {e.Message}\n{e.StackTrace}");
            // Intentar mostrar el panel principal de todas formas
            if (uiView != null)
            {
                try
                {
                    uiView.ShowMainPanel();
                }
                catch (System.Exception e2)
                {
                    Debug.LogError($"Error crítico al mostrar MainPanel: {e2.Message}");
                }
            }
        }
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnCreditsPressed()
    {
        if (uiView != null)
            uiView.ShowCredits();
    }

    public void OnCreditsHide()
    {
        if (uiView != null)
            uiView.HideCredits();
    }

    public void OnBackToMainPressed()
    {
        if (uiView != null)
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
