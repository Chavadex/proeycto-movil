using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private NavMeshAgent agent;
    private Camera mainCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        agent.speed = moveSpeed;
    }




    
    void Update()
    {
        // Actualiza la velocidad por si la cambias en runtime
        agent.speed = moveSpeed;

        if (Input.GetMouseButtonDown(0)) // Click o Touch
        {
            MoveToClickPosition();
        }
    }
    
    void MoveToClickPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
