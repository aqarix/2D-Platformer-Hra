using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text strengthText;
    public TMP_Text agilityText;

    [Header("Stats")]
    public int strength = 1;
    public int agility = 1;

    void Start()
    {
        RefreshUI();
    }

    public void AddStrength()
    {
        strength++;
        RefreshUI();
    }

    public void AddAgility()
    {
        agility++;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (strengthText) strengthText.text = $"Strength: {strength}";
        if (agilityText) agilityText.text = $"Agility: {agility}";
    }
}
