using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    [Header("Attributes")]
    public int level = 1;
    public int xp = 0;
    public float xpGainMult = 1;
    public int xpToNextLevel = 1;

    [HideInInspector] public int totalXpEarned;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI levelText;

    [Header("UI Components")]
    public Image xpBarFillAmount;

    private void Start()
    {
        levelText.text = $"Lv. {level}";
    }

    public void AddXp(int amount)
    {
        amount = Mathf.RoundToInt(amount * xpGainMult);

        xp += amount;
        totalXpEarned += amount;

        int levelsGained = 0;

        while (xp >= xpToNextLevel)
        {
            xp -= xpToNextLevel;
            level++;
            levelsGained++;

            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.25f) + 5;
        }

        UpdateXpBar();

        if (levelsGained > 0)
        {
            UpgradeController.instance.Open();
        }
    }

    void UpdateXpBar()
    {
        xpBarFillAmount.fillAmount = (float)xp / (float)xpToNextLevel;
        levelText.text = $"Lv. {level}";
    }

    private void Update()
    {
        timer.text = GameManager.instance.GetElapsedTime();
    }
}
