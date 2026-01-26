using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GachaponManager : MonoBehaviour
{
    [System.Serializable]
   

    public class GachaReward
    {
        public string rewardName;
        [Range(0f, 100f)] public float probability;
    }

    private const string GACHA_KEY_PREFIX = "GACHA_REWARD_";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI rewardText;

    [Header("Gacha Cost")]
    [SerializeField] private int gachaCost = 50;

    [Header("Rewards")]
    [SerializeField] private List<GachaReward> rewards = new List<GachaReward>();

    [Header("Spin Settings")]
    [SerializeField] private float spinSpeed = 0.05f;
    [SerializeField] private float spinDuration = 2f;

    private bool isSpinning = false;

    // =========================
    // BUTTON CALL
    // ========================
    //
    // =

    private void Start()
    {
        ResetGachaponData();
    }
    public void StartGachapon()
    {
        if (isSpinning) return;

        if (CoinManager.Instance.Coins < gachaCost)
        {
            rewardText.text = "NO TIENES MONEDAS";
            return;
        }

        CoinManager.Instance.SpendCoins(gachaCost);
        StartCoroutine(SpinRoutine());
    }

    // =========================
    // SPIN
    // =========================
    private IEnumerator SpinRoutine()
    {
        isSpinning = true;

        float timer = 0f;

        while (timer < spinDuration)
        {
            rewardText.text = rewards[Random.Range(0, rewards.Count)].rewardName;

            timer += spinSpeed;
            yield return new WaitForSecondsRealtime(spinSpeed);
        }

        GachaReward finalReward = GetRandomRewardByProbability();
        rewardText.text = finalReward.rewardName;

        ExecuteReward(finalReward.rewardName);



        isSpinning = false;
    }


    // =========================
    // PROBABILITY LOGIC
    // =========================
    private GachaReward GetRandomRewardByProbability()
    {
        float totalProbability = 0f;

        foreach (var reward in rewards)
            totalProbability += reward.probability;

        float randomValue = Random.Range(0f, totalProbability);
        float current = 0f;

        foreach (var reward in rewards)
        {
            current += reward.probability;
            if (randomValue <= current)
                return reward;
        }

        return rewards[0];
    }

    // =========================
    // SAVE SYSTEM
    // =========================
    private void RegisterReward(string rewardName)
    {
        string key = GACHA_KEY_PREFIX + rewardName;
        int count = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, count + 1);
        PlayerPrefs.Save();
    }

    public bool HasRewardAppeared(string rewardName)
    {
        return PlayerPrefs.GetInt(GACHA_KEY_PREFIX + rewardName, 0) > 0;
    }

    public void ResetGachaponData()
    {
        foreach (var reward in rewards)
        {
            PlayerPrefs.DeleteKey(GACHA_KEY_PREFIX + reward.rewardName);
        }

        PlayerPrefs.Save();
        Debug.Log("Gachapon reseteado");
    }

    // =========================
    // REWARD EXECUTION
    // =========================
    private void ExecuteReward(string reward)
    {
        if (HasRewardAppeared(reward))
        {
            Reward_Repetida();
            return;
        }

        switch (reward)
        {
            case "Monedas":
                Reward_Monedas();
                break;

            case "Vida":
                Reward_Vida();
                break;

            case "Laser":
                Reward_Laser();
                break;

            default:
                Debug.Log("Recompensa sin función: " + reward);
                break;
        }

        RegisterReward(reward);
    }


    // =========================
    // REWARD FUNCTIONS
    // =========================
    private void Reward_Monedas()
    {
        Debug.Log("Gacha: Monedas");
        CoinManager.Instance.AddCoins(100);
    }

    private void Reward_Vida()
    {
        Debug.Log("Gacha: Vida");
    }

    private void Reward_Laser()
    {
        Debug.Log("Gacha: Laser");
    }

    private void Reward_Repetida()
    {
        CoinManager _coinManager = FindFirstObjectByType<CoinManager>();
        _coinManager.AddCoins(500);
        Debug.Log("Gacha: Repetida");
        // aquí tú decides qué hacer
    }
}
