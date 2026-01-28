using UnityEngine;
using TMPro; 

public class MainMenuView : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainContainer;
    [SerializeField] private GameObject creditsContainer; 

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI totalCoinsText;

    public void ShowMainPanel()
    {
        if (mainContainer != null)
            mainContainer.SetActive(true);
        if (creditsContainer != null) 
            creditsContainer.SetActive(false);
    }

    public void ShowCredits()
    {
        if (mainContainer != null)
            mainContainer.SetActive(false);
        if (creditsContainer != null) 
            creditsContainer.SetActive(true);
    }

    public void HideCredits()
    {
        if (mainContainer != null)
            mainContainer.SetActive(true);
        if (creditsContainer != null) 
            creditsContainer.SetActive(false);
    }

    public void UpdateCoinsText(int coins)
    {
        if (totalCoinsText != null)
            totalCoinsText.text = coins.ToString();
    }
}