using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeflect : MonoBehaviour
{
    public GameObject deflector;
    public float deflectTime;
    public Image deflectTimerImage;
    public float deflectCooldownTime;
    private float coolDownTimer;
    private bool canDeflect;

    private void Start()
    {
        deflectTimerImage.fillAmount = 0;
        canDeflect = true;
        deflector.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canDeflect)
            {
                StartCoroutine("Deflect");
            }
        }

        DeflectCooldown();
    }

    void DeflectCooldown()
    {
        if ( canDeflect == false)
        {
            if (coolDownTimer <= 0)
            {
                canDeflect = true;
            }
            else
            {
                coolDownTimer -= Time.deltaTime;
            }

            deflectTimerImage.fillAmount = (coolDownTimer/deflectCooldownTime);
        }
    }

    IEnumerator Deflect ()
    {
        deflector.SetActive(true);
        coolDownTimer = deflectCooldownTime;
        canDeflect = false;
        yield return new WaitForSeconds(deflectTime);
        deflector.SetActive(false);
        
    }

}
