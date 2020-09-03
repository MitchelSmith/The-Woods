using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;

    [Header("Weapon Animation Parameters")]
    [SerializeField] Transform parent;
    [SerializeField] GameObject casingPrefab;
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] Transform barrelLocation;
    [SerializeField] Transform casingExitLocation;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        StartAnimation();

        RaycastHit hit;
        Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit);
        print(hit.transform.name);
    }

    private void StartAnimation()
    {
        GetComponent<Animator>().speed = 2f;
        GetComponent<Animator>().SetTrigger("Fire");
    }

    /// <summary>
    /// Triggered by firing animation
    /// </summary>
    void Shoot()
    {
        GameObject muzzleFlash;
        muzzleFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
        muzzleFlash.transform.parent = parent;
    }

    /// <summary>
    /// Triggered by firing animation
    /// </summary>
    void CasingRelease()
    {
        GameObject casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        casing.transform.parent = parent;

        Rigidbody casingRigidBody = casing.GetComponent<Rigidbody>();

        casingRigidBody.isKinematic = false;
        casingRigidBody.useGravity = true;
        casingRigidBody.AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);

        Destroy(casing, 10f);
    }
}
