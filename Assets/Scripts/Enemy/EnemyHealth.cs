using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;

    public void ReceiveDamage(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
        else
        {
            BroadcastMessage("BecomeProvoked");
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        if (!isDead)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }

        isDead = true;
    }
}
