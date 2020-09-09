using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots = null;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType = default;
        public int ammoAmount = 0;
        public int magazineSize = 0;
        public int roundsInMagazine = 0;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public int GetCurrentRoundsInMagazine(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).roundsInMagazine;
    }

    public int GetMaxRoundsInMagazine(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).magazineSize;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void ReduceCurrentRoundsInMagazine(AmmoType ammoType) 
    {
        GetAmmoSlot(ammoType).roundsInMagazine--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    public void ReloadWeapon(AmmoType ammoType)
    {
        if (GetAmmoSlot(ammoType).ammoAmount >= 7)
        {
            GetAmmoSlot(ammoType).roundsInMagazine = 7;
        }
        else
        {
            GetAmmoSlot(ammoType).roundsInMagazine = GetAmmoSlot(ammoType).ammoAmount;
        }
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }
}
