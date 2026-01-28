using UnityEngine;

using UnityEngine.AI;

using UnityEngine.EventSystems;



public class ClickToMove : MonoBehaviour

{
    [Header("Movement")]

    public float moveSpeed = 6f;

    private NavMeshAgent agent;

    private Camera mainCamera;
    void Start()

    {
        agent = GetComponent<NavMeshAgent>();

        mainCamera = Camera.main;

        agent.speed = moveSpeed;

        agent.acceleration = 60f;

        agent.angularSpeed = 720f;

        agent.autoBraking = true;

    }



    void Update()

    {
        agent.speed = moveSpeed;

        if (Input.GetMouseButtonDown(0))

        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

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