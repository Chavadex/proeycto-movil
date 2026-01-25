using UnityEngine;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] TextMeshProUGUI currentCoinsTMP;

    private const string COIN_KEY = "PlayerCoins";
    private int coins;

    public int Coins => coins;

    private void Update()
    {
        currentCoinsTMP.text = coins.ToString();
    }

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadCoins();
    }

    // -----------------------------
    // PUBLIC METHODS
    // -----------------------------

    /// <summary>
    /// Añade monedas (enemigos, recompensas, shop, etc.)
    /// </summary>
    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        coins += amount;
        SaveCoins();

        Debug.Log($"Monedas añadidas: {amount} | Total: {coins}");
    }

    /// <summary>
    /// Gasta monedas si hay suficientes
    /// </summary>
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

        Debug.Log($"Monedas gastadas: {amount} | Total: {coins}");
        return true;
    }

    /// <summary>
    /// Resetea monedas (debug o nueva partida)
    /// </summary>
    public void ResetCoins()
    {
        coins = 0;
        SaveCoins();
    }

    // -----------------------------
    // SAVE / LOAD
    // -----------------------------

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COIN_KEY, coins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        coins = PlayerPrefs.GetInt(COIN_KEY, 0);
    }
}
