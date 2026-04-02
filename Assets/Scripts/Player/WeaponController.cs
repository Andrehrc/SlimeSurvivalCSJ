using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponController : MonoBehaviour
{
    [Serializable]
    public class WeaponSlot
    {
        public Weapon weapon;
        [HideInInspector] public float timer;

        [Header("Upgrades")]
        public float cooldowMult = 1f;
        public float damageMult = 1f;
    }

    public List<WeaponSlot> weapons = new List<WeaponSlot>();

    private void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            var w = weapons[i];
            if (w.weapon == null) continue;

            w.timer += Time.deltaTime;
            float finalCooldown = w.weapon.cooldown * w.cooldowMult;

            if (w.timer >= finalCooldown)
            {
                Attack(w);
                w.timer = 0f;
            }
        }
    }

    void Attack(WeaponSlot w)
    {
        int finalDamage = Mathf.RoundToInt(w.weapon.damage * w.damageMult);
        GameObject wp = Instantiate(w.weapon.attackPrefab, transform.position, Quaternion.identity);
        wp.GetComponent<AttackArea>().damageDealer.GetDamage(finalDamage);
    }

    public bool HasWeapon(Weapon weapon)
    {
        return GetSlot(weapon) != null;
    }

    public void UpgradeWeaponDamage(Weapon weapon, float percent)
    {
        WeaponSlot slot = GetSlot(weapon);

        if (slot == null)
            return;

        slot.damageMult *= (1f + percent);
    }

    public void UpgradeWeaponCooldown(Weapon weapon, float percent)
    {
        WeaponSlot slot = GetSlot(weapon);

        if (slot == null)
            return;

        slot.cooldowMult *= (1f - percent);
        slot.cooldowMult = Mathf.Max(0.1f, slot.cooldowMult);
    }

    WeaponSlot GetSlot(Weapon weapon)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].weapon == weapon)
            {
                return weapons[i];
            }
        }

        return null;
    }


}
