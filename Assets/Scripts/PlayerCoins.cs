using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    [Min(0)] public int coins = 0;

    public bool Spend(int amount)
    {
        if (amount <= 0) return true;
        if (coins < amount) return false;

        coins -= amount;
        return true;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;
        coins += amount;
    }
}
