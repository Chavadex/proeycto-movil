using UnityEngine;
using TMPro; 

public class MainMenuView : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainContainer;
    [SerializeField] private GameObject settingsContainer;
    [SerializeField] private GameObject creditsContainer; 

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI totalCoinsText;

    public void ShowMainPanel()
    {
        mainContainer.SetActive(true);
        settingsContainer.SetActive(false);
        if (creditsContainer) creditsContainer.SetActive(false);
    }

    public void ShowSettings()
    {
        mainContainer.SetActive(false);
        settingsContainer.SetActive(true);
    }

    public void ShowCredits()
    {
        mainContainer.SetActive(false);
        if (creditsContainer) creditsContainer.SetActive(true);
    }

    public void UpdateCoinsText(int coins)
    {
        if (totalCoinsText != null)
            totalCoinsText.text = coins.ToString();
    }
}