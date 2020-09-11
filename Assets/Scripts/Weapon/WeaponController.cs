using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("First Person Camera")] [SerializeField] Camera FPCamera = null;

    [Header("Weapon Parameters")]
    [Tooltip("Meters")] [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] Ammo ammoSlot = null;
    [SerializeField] AmmoType ammoType = default;

    [Header("Animation Parameters")]
    [SerializeField] string shootingAnimationName = null;
    [SerializeField] float animationSpeed = 1f;
    [SerializeField] Transform destroyParent = null;
    [SerializeField] GameObject casingPrefab = null;
    [SerializeField] Transform barrelLocation = null;
    [SerializeField] Transform casingExitLocation = null;
    [SerializeField] ParticleSystem muzzleFlash = null;

    [Header("Audio")]
    [SerializeField] AudioSource generalAudio = null;
    [SerializeField] AudioSource reloadAudio = null;
    [SerializeField] AudioClip pistolShot = null;
    [SerializeField] AudioClip dryFire = null;

    private Animator animator;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Do not shoot if animation is still playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(shootingAnimationName) || reloadAudio.isPlaying) { return; }

        bool hasAmmo = ammoSlot.GetCurrentAmmo(ammoType) > 0;

        if (Input.GetButtonDown("Fire1"))
        {
            if (hasAmmo && ammoSlot.GetCurrentRoundsInMagazine(ammoType) > 0)
            {
                ShootWeapon();
            }
            else
            {
                animator.speed = 2f;
                animator.SetTrigger("DryFire");
            }
        }
        else if (Input.GetButtonDown("Reload"))
        {
            bool magazineRoundsEqualsTotalRounds = ammoSlot.GetCurrentRoundsInMagazine(ammoType) == ammoSlot.GetCurrentAmmo(ammoType);
            bool roundsInMagazineLessThanMax = ammoSlot.GetCurrentRoundsInMagazine(ammoType) < ammoSlot.GetMaxRoundsInMagazine(ammoType);

            if (hasAmmo && !magazineRoundsEqualsTotalRounds && roundsInMagazineLessThanMax)
            {
                ReloadWeapon();
            }
        }
    }

    private void ShootWeapon()
    {
        // TODO: Fix gun shot audio being cut off by holstering
        generalAudio.PlayOneShot(pistolShot);
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo(ammoType);
        ammoSlot.ReduceCurrentRoundsInMagazine(ammoType);
    }

    private void ReloadWeapon()
    {
        animator.SetTrigger("Reload");
        reloadAudio.Play();
        ammoSlot.ReloadWeapon(ammoType);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            target?.ReceiveDamage(damage);
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
        animator.speed = animationSpeed;
        animator.SetTrigger("Fire");
    }

    /// <summary>
    /// Triggered by firing animation
    /// </summary>
    void CasingRelease()
    {
        GameObject casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        casing.transform.parent = destroyParent;

        Rigidbody casingRigidBody = casing.GetComponent<Rigidbody>();

        casingRigidBody.isKinematic = false;
        casingRigidBody.useGravity = true;
        casingRigidBody.AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * Random.Range(0.35f, 0.45f) - casingExitLocation.up * Random.Range(0.55f, 0.65f)), 1f);
        casingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);

        Destroy(casing, 10f);
    }

    /// <summary>
    /// Trigger by dry fire animation
    /// </summary>
    void DryFireWeapon()
    {
        generalAudio.PlayOneShot(dryFire);
    }
}
