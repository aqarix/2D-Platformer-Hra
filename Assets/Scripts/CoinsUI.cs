using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    public PlayerCoins playerCoins;
    public TMP_Text coinsText;

    void Update()
    {
        coinsText.text = "" + playerCoins.coins;
    }
}