using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController instance;

    [Header("Data")]
    public List<UpgradeSO> upgrades = new List<UpgradeSO>();

    private WeaponController weaponController;
    private PlayerXP playerXP;
    private PlayerHealth playerHealth;

    [Header("UI")]
    public GameObject panel;

    public UpgradeSlot slot1;
    public UpgradeSlot slot2;
    public UpgradeSlot slot3;

    private void Awake()
    {
        instance = this;

        panel.SetActive(false);
    }

    private void Start()
    {
        weaponController = PlayerController.Instance.GetComponent<WeaponController>();
        playerXP = PlayerController.Instance.GetComponent<PlayerXP>();
        playerHealth = PlayerController.Instance.GetComponent<PlayerHealth>();
    }

    public void Open()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);

        var upgradeOptions = PickUpgrades();
        slot1.SetUpgradeInfo(upgradeOptions[0]);
        slot2.SetUpgradeInfo(upgradeOptions[1]);
        slot3.SetUpgradeInfo(upgradeOptions[2]);
    }

    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Apply(UpgradeSO upgrade)
    {
        if (upgrade == null)
            return;

        switch (upgrade.type)
        {
            case UpgradeType.Heal:
                playerHealth.Heal(upgrade.value);
                break;

            case UpgradeType.WeaponCooldown:
                weaponController.UpgradeWeaponCooldown(upgrade.targetWeapon, upgrade.value);
                break;

            case UpgradeType.WeaponDamage:
                weaponController.UpgradeWeaponDamage(upgrade.targetWeapon, upgrade.value);
                break;

            case UpgradeType.XpGain:
                playerXP.xpGainMult += upgrade.value;
                break;

            case UpgradeType.MaxHp:
                playerHealth.AddMaxHealth(upgrade.value);
                break;
        }
    }


    List<UpgradeSO> PickUpgrades()
    {
        List<UpgradeSO> pool = new List<UpgradeSO>();

        foreach (var item in upgrades)
        {
            if (item == null)
                continue;

            bool isWeaponUpgrade = item.type == UpgradeType.WeaponCooldown || item.type == UpgradeType.WeaponCooldown;

            if (isWeaponUpgrade)
                if (item.targetWeapon == null || !weaponController.HasWeapon(item.targetWeapon))
                    continue;

            pool.Add(item);
        }

        List<UpgradeSO> result = new List<UpgradeSO>();

        for (int i = 0; i < 3; i++)
        {
            if (pool.Count == 0)
            {
                result.Add(upgrades[0]); //fallback - Adiciona o primeiro item da lista para evitar bugs
            }

            int randomUpgrades = Random.Range(0, pool.Count);
            result.Add((UpgradeSO)pool[randomUpgrades]);
            pool.RemoveAt(randomUpgrades);
        }

        return result;
    }
}
