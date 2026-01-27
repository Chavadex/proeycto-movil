using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (_mainCamera != null)
        {
            transform.rotation = _mainCamera.transform.rotation;

            // Opción B: Si prefieres que siempre esté rígido en una sola dirección (ej. siempre plano)
           // transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Descomenta esta y comenta la de arriba si prefieres esto
        }
    }
}
