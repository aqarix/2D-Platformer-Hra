using UnityEngine;

public class WeaponShopUI : MonoBehaviour
{
    public GameObject slot2Icon;
    public GameObject slot3Icon;

    public void UnlockSlot2Icon()
    {
        if (slot2Icon != null)
            slot2Icon.SetActive(true);
    }

    public void UnlockSlot3Icon()
    {
        if (slot3Icon != null)
            slot3Icon.SetActive(true);
    }
}