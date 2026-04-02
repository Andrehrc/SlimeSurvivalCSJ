using TMPro;
using UnityEngine;

public class HitCanva : MonoBehaviour
{
    public TextMeshProUGUI hitText;

    public void ChangeValue(int damageValue)
    {
        hitText.text = damageValue.ToString();
    }
}
