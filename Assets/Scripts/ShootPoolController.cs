using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class ShootPoolController : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] GameObject Bullet;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private TextMeshProUGUI availables;
    //private GameObject bullet;
    private ObjectPool<GameObject> bulletPool;
 

    private void Awake()
    {
        bulletPool = new ObjectPool<GameObject>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            false,
            10,
            50
        );
    }

    private void Start()
    {
        availables.SetText($"Balas disponibles: {bulletPool.CountActive}");
    }
    private GameObject CreateBullet()
    {
        return Instantiate(Bullet);
    }

    private void OnGetBullet(GameObject bullet)
    {
        bullet.SetActive(true);

    }

    private void OnReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void OnDestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }
    public void Disparar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet = bulletPool.Get();

            bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);

            StartCoroutine(ReleaseBulletAfterTime(bullet, 3f));
        }
    }

    private IEnumerator ReleaseBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        bulletPool.Release(bullet);
    }
}

