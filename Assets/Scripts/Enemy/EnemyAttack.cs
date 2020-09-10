using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] AudioClip[] attackZombieSounds = null;

    PlayerHealth target = null;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        target?.ReceiveDamage(damage);
    }

    public void PlayRandomAttackSound()
    {
        int audioClipIndex = Random.Range(0, attackZombieSounds.Length);
        GetComponent<AudioSource>().PlayOneShot(attackZombieSounds[audioClipIndex], 1f);
    }
}
