using UnityEngine;

public class ShopWeapon : MonoBehaviour
{
    public PlayerCoins playerCoins;
    public WeaponSwitch weaponSwitch;
    public WeaponShopUI weaponShopUI;

    public int slot2Cost = 60;
    public int slot3Cost = 200;

    public void BuySlot2Weapon()
    {
        if (weaponSwitch.slot2Unlocked)
        {
            Debug.Log("Zbraò ve slotu 2 už je koupená");
            return;
        }

        if (playerCoins.Spend(slot2Cost))
        {
            weaponSwitch.UnlockSlot2();
            weaponShopUI.UnlockSlot2Icon();
            Debug.Log("Zbraò ve slotu 2 koupena");
        }
        else
        {
            Debug.Log("Nemáš dost coinù");
        }
    }

    public void BuySlot3Weapon()
    {
        if (weaponSwitch.slot3Unlocked)
        {
            Debug.Log("Zbraò ve slotu 3 už je koupená");
            return;
        }

        if (playerCoins.Spend(slot3Cost))
        {
            weaponSwitch.UnlockSlot3();
            weaponShopUI.UnlockSlot3Icon();
            Debug.Log("Zbraò ve slotu 3 koupena");
        }
        else
        {
            Debug.Log("Nemáš dost coinù");
        }
    }
}