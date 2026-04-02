using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;

    private UpgradeSO current;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetUpgradeInfo(UpgradeSO upgrade)
    {
        current = upgrade;

        icon.sprite = upgrade.icon;
        title.text = upgrade.title;
        desc.text = upgrade.description;
    }

    void OnClick()
    {
        if (current == null)
            return;

        UpgradeController.instance.Apply(current);
        UpgradeController.instance.Close();
    }
}
