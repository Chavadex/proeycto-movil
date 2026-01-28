using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTD : MonoBehaviour
{
    [Header("Presentation")]
    [SerializeField] private GameUIView uiView;

    private GameViewModel _viewModel;

    private WaveManager _waveManager;

    void Start()
    {
        _waveManager = FindFirstObjectByType<WaveManager>();

        _viewModel = new GameViewModel();

        Time.timeScale = 1;

        if (uiView != null) uiView.HideAllPopups();
    }

    public void PauseGame()
    {
        _viewModel.SetPauseState(true);

        Time.timeScale = 0;

        if (uiView != null)
            uiView.TogglePauseUI(true);
    }

    public void Resume()
    {
        _viewModel.SetPauseState(false);

        Time.timeScale = 1;

        if (uiView != null)
        {
            uiView.TogglePauseUI(false);
            uiView.HideAllPopups();
        }
    }

    public void CheckIfFirstDead()
    {
        if (!_viewModel.HasDead)
        {
            DefeatedADS();
        }
        else
        {
            DefeatedNOADS();
        }
    }

    private void DefeatedADS()
    {
        _viewModel.SetDeadState(true);

        if (uiView != null)
            uiView.ShowDefeatAds();
    }

    private void DefeatedNOADS()
    {
        Time.timeScale = 0;

        if (uiView != null)
            uiView.ShowDefeatNoAds();
    }


    public void OpenShop() { if (uiView != null) uiView.ShowShop(true); }
    public void BackFromShop() { if (uiView != null) uiView.ShowShop(false); }

    public void OpenBoxShop() { if (uiView != null) uiView.ShowBoxShop(true); }
    public void BackFromBOXShop() { if (uiView != null) uiView.ShowBoxShop(false); }

    public void OpenCoinsShop() { if (uiView != null) uiView.ShowCoinsShop(true); }
    public void BackFromCoinShop() { if (uiView != null) uiView.ShowCoinsShop(false); }


    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (_waveManager != null) _waveManager.ResetWavesFromStart();
    }

    public void MainMenu()
    {
         SceneManager.LoadScene("MainMenu");
    }
}
