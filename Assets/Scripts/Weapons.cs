using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public enum CurrentWeapon { None, Grapple, Rocket, Crystal }
    private CurrentWeapon activeWeapon = CurrentWeapon.Grapple;

    [Header("Weapon Switching")]
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] Transform firePoint;

    [Header("Grappling Settings")]
    [SerializeField] float horizontalPullSpeed = 10f;
    [SerializeField] float verticalPullSpeed = 10f;
    [SerializeField] float grappleReach = 20f;
    [SerializeField] LayerMask grappleLayer;
    [SerializeField] LineRenderer ropeRenderer;

    [Header("Audio")]
    AudioSource asc;
    [SerializeField] AudioClip grappleSound;
    [SerializeField] AudioClip rocketShootSound;
    bool grappleSoundPlayed = false;

    Vector2 targetPosition;
    bool isGrappling = false;
    Rigidbody2D rb;

    float defaultGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        asc = GetComponent<AudioSource>();

        defaultGravityScale = rb.gravityScale;

        if (ropeRenderer != null)
        {
            ropeRenderer.positionCount = 2;
            ropeRenderer.enabled = false;
        }

        ApplyWeaponEffects();
    }

    void Update()
    {
        bool inputDown = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J);
        bool inputUp = Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.J);
        bool inputHeld = Input.GetMouseButton(0) || Input.GetKey(KeyCode.J);

        if (activeWeapon == CurrentWeapon.Grapple)
        {
            if (inputDown) StartGrapple();
            if (inputUp) StopGrapple();

            if (isGrappling && Vector2.Distance(transform.position, targetPosition) >= grappleReach)
                StopGrapple();
        }
        else if (activeWeapon == CurrentWeapon.Rocket)
        {
            if (isGrappling) StopGrapple();

            if (inputDown) ShootRocket();
        }
        else if (activeWeapon == CurrentWeapon.Crystal)
        {
            if (isGrappling) StopGrapple();
        }
        else if (activeWeapon == CurrentWeapon.None)
        {
            if (isGrappling) StopGrapple();
        }
    }

    void FixedUpdate()
    {
        bool inputHeld = Input.GetMouseButton(0) || Input.GetKey(KeyCode.J);

        if (isGrappling && inputHeld && activeWeapon == CurrentWeapon.Grapple)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            float pullX = direction.x * horizontalPullSpeed * Time.fixedDeltaTime;
            float pullY = direction.y * verticalPullSpeed * Time.fixedDeltaTime;

            rb.linearVelocity += new Vector2(pullX, pullY);

            if (ropeRenderer != null)
                ropeRenderer.SetPosition(0, transform.position);
        }
    }

    public void SetWeaponBySlot(int slot)
    {
        switch (slot)
        {
            case 1:
                SetWeapon(CurrentWeapon.Grapple);
                Debug.Log("Equipped: Grappling Hook");
                break;

            case 2:
                SetWeapon(CurrentWeapon.Rocket);
                Debug.Log("Equipped: Rocket Launcher");
                break;

            case 3:
                SetWeapon(CurrentWeapon.Crystal);
                Debug.Log("Equipped: Crystal");
                break;

            case 0:
                DisableAllWeapons();
                Debug.Log("No weapon selected");
                break;
        }
    }

    public void DisableAllWeapons()
    {
        activeWeapon = CurrentWeapon.None;

        if (isGrappling)
            StopGrapple();

        rb.gravityScale = defaultGravityScale;
    }

    void SetWeapon(CurrentWeapon newWeapon)
    {
        if (activeWeapon == newWeapon) return;

        activeWeapon = newWeapon;

        if (isGrappling)
            StopGrapple();

        ApplyWeaponEffects();
    }

    void ApplyWeaponEffects()
    {
        if (activeWeapon == CurrentWeapon.Crystal)
            rb.gravityScale = 0f;
        else
            rb.gravityScale = defaultGravityScale;
    }

    void StartGrapple()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        bool originalQueriesStartInColliders = Physics2D.queriesStartInColliders;
        Physics2D.queriesStartInColliders = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleReach, grappleLayer);

        Physics2D.queriesStartInColliders = originalQueriesStartInColliders;

        if (hit.collider != null)
        {
            targetPosition = hit.point;
            isGrappling = true;

            Debug.DrawLine(transform.position, hit.point, Color.green, 1f);

            if (ropeRenderer != null)
            {
                ropeRenderer.enabled = true;
                ropeRenderer.SetPosition(0, transform.position);
                ropeRenderer.SetPosition(1, targetPosition);
            }

            if (!grappleSoundPlayed && asc != null && grappleSound != null)
            {
                asc.PlayOneShot(grappleSound);
                grappleSoundPlayed = true;
            }
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        grappleSoundPlayed = false;

        if (ropeRenderer != null)
            ropeRenderer.enabled = false;
    }

    void ShootRocket()
    {
        if (rocketPrefab == null || firePoint == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 direction = (mousePos - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Instantiate(rocketPrefab, firePoint.position, rotation);

        if (asc != null && rocketShootSound != null)
            asc.PlayOneShot(rocketShootSound);
    }
}