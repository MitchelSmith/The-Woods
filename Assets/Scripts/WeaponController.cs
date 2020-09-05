﻿using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("First Person Camera")] [SerializeField] Camera FPCamera = null;
    [Tooltip("Meters")] [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] GameObject hitEffect = null;

    [Header("Weapon Animation Parameters")]
    [SerializeField] string animationName = null;
    [SerializeField] float animationSpeed = 1f;
    [SerializeField] Transform casingParent = null;
    [SerializeField] GameObject casingPrefab = null;
    [SerializeField] Transform barrelLocation = null;
    [SerializeField] Transform casingExitLocation = null;
    [SerializeField] ParticleSystem muzzleFlash = null;

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)) { return; }

        if (Input.GetButtonDown("Fire1"))
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
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
        casing.transform.parent = casingParent;

        Rigidbody casingRigidBody = casing.GetComponent<Rigidbody>();

        casingRigidBody.isKinematic = false;
        casingRigidBody.useGravity = true;
        casingRigidBody.AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * Random.Range(0.35f, 0.45f) - casingExitLocation.up * Random.Range(0.55f, 0.65f)), 1f);
        casingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);

        Destroy(casing, 10f);
    }
}
