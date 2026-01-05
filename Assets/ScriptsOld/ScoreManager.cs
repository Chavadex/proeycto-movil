using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI defeatedTMP;
    [SerializeField]  private Image[] lifesImage;
    [SerializeField] static private int currentScore;
    [SerializeField] NewAdView _newAdView;

    public static int lifesLeft;

    private void Start()
    {
        _newAdView = FindFirstObjectByType<NewAdView>();
        lifesLeft = lifesImage.Length;
        UpdateLifesUI();
        defeatedTMP.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (lifesLeft <= 0)
        {
            defeated();
        }

        scoreText.text = "Score: " + currentScore;

    }
     public void LoseLife()
    {

        lifesLeft--;
        UpdateLifesUI();

    }

     private void UpdateLifesUI()
    {
        for (int i = 0; i < lifesImage.Length; i++)
        {
            lifesImage[i].enabled = i < lifesLeft;
        }
    }

     public void addScore()
    {
        currentScore += 10;
    }

    private void defeated()
    {
        defeatedTMP.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}

