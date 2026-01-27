using UnityEngine;

public class GameRepository : MonoBehaviour
{
    public static GameRepository Instance;

    private const string COIN_KEY = "PlayerCoins";
    private const string WAVE_KEY = "CURRENT_WAVE";
    private const string PREFIX_POWERUP = "PWR_";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCoins(int amount)
    {
        PlayerPrefs.SetInt(COIN_KEY, amount);
        PlayerPrefs.Save();
    }

    public int LoadCoins()
    {
        return PlayerPrefs.GetInt(COIN_KEY, 0);
    }

    public void SaveWave(int waveIndex)
    {
        PlayerPrefs.SetInt(WAVE_KEY, waveIndex);
        PlayerPrefs.Save();
    }

    public int LoadWave()
    {
        return PlayerPrefs.GetInt(WAVE_KEY, 0);
    }

    public void SavePowerUpCount(string type, int count)
    {
        PlayerPrefs.SetInt(PREFIX_POWERUP + type, count);
        PlayerPrefs.Save();
    }

    public int LoadPowerUpCount(string type, int defaultVal)
    {
        return PlayerPrefs.GetInt(PREFIX_POWERUP + type, defaultVal);
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}