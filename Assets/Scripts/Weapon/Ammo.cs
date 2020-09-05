using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int amount = 0;

    public int GetCurrentAmmo()
    {
        return amount;
    }

    public void ReduceCurrentAmmo()
    {
        amount--;
    }
}
