using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public PlayerController player;

    public TMP_Text speedText;
    public TMP_Text jumpText;

    void Update()
    {
        speedText.text = "Speed lvl: " + player.speedUpgradeLevel;
        jumpText.text = "Jump lvl: " + player.jumpUpgradeLevel;
    }
}