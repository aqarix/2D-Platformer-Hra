using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCoins playerCoins = collision.GetComponent<PlayerCoins>();

        if (playerCoins != null)
        {
            playerCoins.Add(coinValue);
            Destroy(gameObject);
        }
    }
}