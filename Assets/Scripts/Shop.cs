using UnityEngine;

public class Shop : MonoBehaviour
{
    public PlayerController player;
    public PlayerCoins playerCoins;

    public int speedCost = 20;
    public int jumpCost = 20;

    public void BuySpeed()
    {
        if (playerCoins.Spend(speedCost))
        {
            player.UpgradeSpeed(1f);
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void BuyJump()
    {
        if (playerCoins.Spend(jumpCost))
        {
            player.UpgradeJump(2f);
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
}