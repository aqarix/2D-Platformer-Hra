using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Optional UI")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI messageText;

    private PlayerCoins playerCoins;

    private void Awake()
    {
        // Start closed
        gameObject.SetActive(false);
        if (messageText) messageText.text = "";
    }

    public void Open(PlayerCoins coins)
    {
        playerCoins = coins;
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (messageText) messageText.text = "";
        playerCoins = null;
    }

    public void BuyItem(int cost)
    {
        if (playerCoins == null) return;

        bool ok = playerCoins.Spend(cost);
        if (ok)
        {
            if (messageText) messageText.text = $"Bought item for {cost}!";
            // TODO: give item here (inventory, powerup, etc.)
        }
        else
        {
            if (messageText) messageText.text = $"Not enough coins ({cost} needed).";
        }

        Refresh();
    }

    private void Refresh()
    {
        if (coinsText && playerCoins != null)
            coinsText.text = $"Coins: {playerCoins.coins}";
    }
}
