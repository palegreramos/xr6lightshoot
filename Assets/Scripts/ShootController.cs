using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] GameObject Bullet;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private TextMeshProUGUI aviso;
    [SerializeField] private float shootDelay = 1f;
    private GameObject bullet;
    private bool canShoot=true;

    public void Disparar(InputAction.CallbackContext context)
    {
        if (context.performed && canShoot)
        {
            StartCoroutine(ShowRoutine("Disparo",shootDelay));
            canShoot = false;
            bullet = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);
            Destroy(bullet, shootDelay*2f);
        }
    }
    private IEnumerator ShowRoutine(string message, float duration)
    {
        aviso.SetText(message);
        aviso.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        canShoot = true;
        aviso.gameObject.SetActive(false);
    }
}

