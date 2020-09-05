using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void ReceiveDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            DeathHandler deathHandler = GetComponent<DeathHandler>();
            deathHandler.HandleDeath();
        }
    }
}
