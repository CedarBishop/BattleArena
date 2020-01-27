using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    private PlayerInput playerInput;

    public Projectile basicBullet;
    public Grenade basicGrenade;
    public Transform gunTip;

    public float shootDelayTime;
    public float grenadeDelayTime;
    private bool canShoot;
    private bool canThrowGrenade;

    public Image GrenadeRefillImage;
    public Image bulletRefillImage;

    private float shootTimer;
    private float grenadeTimer;
    
    Camera mainCamera;
    Vector3 point;

    public float grenadeThrowDistance = 10;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
        canShoot = true;
        canThrowGrenade = true;

        bulletRefillImage.fillAmount = 0;
        GrenadeRefillImage.fillAmount = 0;
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard & Mouse")
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(point);
            }
        }





        ShootCountdown();
        GrenadeCountdown();
    }

    Vector3 CalculateGrenadeVelocity(Vector3 target, Vector3 origin, float time)
    {

        Vector3 distance = target - origin;
        Vector3 distanceXZ = new Vector3(distance.x, 0, distance.z);

        float lateralMagnitude = distanceXZ.magnitude;

        float lateralVelocity = lateralMagnitude / time;
        float yVelocity = distance.y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;


        Vector3 result = distanceXZ.normalized;
        result *= lateralVelocity;
        result.y = yVelocity;
        return result;

    }

   void ShootCountdown ()
   {
        if (canShoot == false)
        {
            if (shootTimer <= 0)
            {
                canShoot = true;
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }


            bulletRefillImage.fillAmount = (shootTimer /shootDelayTime);
        }
   }

    void GrenadeCountdown ()
    {
        if (canThrowGrenade == false)
        {
            if (grenadeTimer <= 0)
            {
                canThrowGrenade = true;
            }
            else
            {
                grenadeTimer -= Time.deltaTime;
            }

            GrenadeRefillImage.fillAmount = (grenadeTimer / grenadeDelayTime);
        }
    }

    void OnFire ()
    {
        if (canShoot)
        {
            Projectile bullet = Instantiate(basicBullet, gunTip.position, transform.rotation);
            shootTimer = shootDelayTime;
            canShoot = false;
        }
    }

    void OnGrenade ()
    {
        if (canThrowGrenade)
        {
            Grenade grenade = Instantiate(basicGrenade, gunTip.position, Quaternion.identity);
            grenade.GetComponent<Rigidbody>().velocity = CalculateGrenadeVelocity(point, gunTip.position, 1.0f);
            grenadeTimer = grenadeDelayTime;
            canThrowGrenade = false;
        }
    }

    void OnLook (InputValue value)
    {
        if (playerInput.currentControlScheme != "Keyboard & Mouse")
        {
            Vector2 target = value.Get<Vector2>() * grenadeThrowDistance;
            point = transform.position + new Vector3(target.x, 0, target.y);
            transform.LookAt(point);
        }        
    }
}
