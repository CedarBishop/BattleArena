using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Projectile basicBullet;
    public Transform gunTip;
    
    
    Camera mainCamera;
    
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,1000.0f))
        {
            Vector3 point = new Vector3(hit.point.x,transform.position.y, hit.point.z);
            transform.LookAt(point);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Projectile bullet = Instantiate(basicBullet, gunTip.position, transform.rotation);
            //bullet.Fire(transform.rotation);
        }
        
    }
}
