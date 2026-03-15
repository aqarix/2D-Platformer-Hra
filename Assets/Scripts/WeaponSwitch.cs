using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("Assign 3 objects (weapons/tools/etc.)")]
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    [Header("Unlocked weapons")]
    public bool slot1Unlocked = true;
    public bool slot2Unlocked = false;
    public bool slot3Unlocked = false;

    [Header("References")]
    public PlayerWeapons playerWeapons;

    [Header("Optional")]
    public int startActive = 1; // 1..3, or 0 = none

    void Start()
    {
        if ((startActive == 0) || IsSlotUnlocked(startActive))
            SetActiveSlot(startActive);
        else
            SetActiveSlot(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TrySetSlot(1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            TrySetSlot(2);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            TrySetSlot(3);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetActiveSlot(0);
    }

    void TrySetSlot(int slot)
    {
        if (IsSlotUnlocked(slot))
        {
            SetActiveSlot(slot);
        }
        else
        {
            Debug.Log("Tahle zbraò ještì není koupená");
        }
    }

    bool IsSlotUnlocked(int slot)
    {
        switch (slot)
        {
            case 1: return slot1Unlocked;
            case 2: return slot2Unlocked;
            case 3: return slot3Unlocked;
            default: return false;
        }
    }

    public void UnlockSlot2()
    {
        slot2Unlocked = true;
    }

    public void UnlockSlot3()
    {
        slot3Unlocked = true;
    }

    public void SetActiveSlot(int slot)
    {
        if (slot1) slot1.SetActive(slot == 1);
        if (slot2) slot2.SetActive(slot == 2);
        if (slot3) slot3.SetActive(slot == 3);

        if (playerWeapons != null)
            playerWeapons.SetWeaponBySlot(slot);
    }
}