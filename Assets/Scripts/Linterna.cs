using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Linterna : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light touchLight;
    [SerializeField] private Light dLight;
    [SerializeField] private float toggleDelay = 0.3f;
    private bool canToggle = true;
    private void Start()
    {
        dLight.enabled = false;
    }
    public void ToggleLinterna(InputAction.CallbackContext context)
    {
        if (context.performed && canToggle)
        {
            touchLight.enabled = !touchLight.enabled;
            StartCoroutine(ToggleCountdown());
        }
    }

    public void ToggleSol(InputAction.CallbackContext context)
    {
        if (context.performed && canToggle)
        {
            dLight.enabled = !dLight.enabled;
            StartCoroutine(ToggleCountdown());
        }
    }
    private IEnumerator ToggleCountdown()
    {
        canToggle = false;
        yield return new WaitForSeconds(toggleDelay);
        canToggle = true;
    }
}
