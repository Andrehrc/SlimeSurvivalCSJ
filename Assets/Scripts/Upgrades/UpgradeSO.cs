using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "Upgrades/New Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public Sprite icon;
    public string title;
    [TextArea] public string description;

    public UpgradeType type;

    public float value;
    public Weapon targetWeapon;
}

public enum UpgradeType
{
    WeaponDamage,
    WeaponCooldown,
    XpGain,
    MaxHp,
    Heal
}
