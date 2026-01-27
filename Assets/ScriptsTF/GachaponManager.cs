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

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI rewardText;

    [Header("Gacha Costs")]
    [SerializeField] private int gachaCost = 50;

    [Header("PowerUp Rewards Settings")]
    [SerializeField] private int powerUpQuantity = 3; 
    [SerializeField] private int maxInventoryLimit = 10;
    [SerializeField] private int compensationCoins = 150; 

    [Header("Consolation Prize Settings")]
    [SerializeField] private int coinsPrizeAmount = 200;

    [Header("Rewards Config")]
    [SerializeField] private List<GachaReward> rewards = new List<GachaReward>();

    [Header("Spin Settings")]
    [SerializeField] private float spinSpeed = 0.05f;
    [SerializeField] private float spinDuration = 2f;

    private bool isSpinning = false;

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

        ProcessReward(finalReward.rewardName);

        isSpinning = false;
    }

    private void ProcessReward(string rewardName)
    {
        if (rewardName == "Monedas")
        {
            CoinManager.Instance.AddCoins(coinsPrizeAmount);
            rewardText.text = $"¡GANASTE!\n+{coinsPrizeAmount} Monedas";
            return; 
        }

        int currentAmount = PowerUpManager.Instance.GetPowerUpCount(rewardName);

        if (currentAmount >= maxInventoryLimit)
        {
            CoinManager.Instance.AddCoins(compensationCoins);
            rewardText.text = $"{rewardName} (LLENO)\n+{compensationCoins} Monedas";
        }
        else
        {
            PowerUpManager.Instance.AddPowerUp(rewardName, powerUpQuantity);
            rewardText.text = $"¡GANASTE!\n+{powerUpQuantity} {rewardName}";
        }
    }

    private GachaReward GetRandomRewardByProbability()
    {
        float totalProbability = 0f;
        foreach (var reward in rewards) totalProbability += reward.probability;

        float randomValue = Random.Range(0f, totalProbability);
        float current = 0f;

        foreach (var reward in rewards)
        {
            current += reward.probability;
            if (randomValue <= current) return reward;
        }

        return rewards[0];
    }
}