using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] TextMeshProUGUI currentCoinsTMP;

    private const string COIN_KEY = "PlayerCoins";
    private int coins;

    public int Coins => coins;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadCoins();
        }
        else
        {

            Instance.currentCoinsTMP = currentCoinsTMP;

            Instance.UpdateUI();

            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        coins += amount;
        SaveCoins();
        UpdateUI(); 
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= 0) return false;

        if (coins < amount)
        {
            Debug.Log("No hay suficientes monedas");
            return false;
        }

        coins -= amount;
        SaveCoins();
        UpdateUI();

        return true;
    }

    public void ResetCoins()
    {
        coins = 0;
        SaveCoins();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentCoinsTMP != null)
        {
            currentCoinsTMP.text = coins.ToString();
        }
    }

    private void SaveCoins()
    {
        if (GameRepository.Instance != null)
            GameRepository.Instance.SaveCoins(coins);
    }

    private void LoadCoins()
    {
        if (GameRepository.Instance != null)
            coins = GameRepository.Instance.LoadCoins();

        UpdateUI();
    }
}