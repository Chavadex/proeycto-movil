using UnityEngine;

using UnityEngine.AI;

using UnityEngine.EventSystems;



public class ClickToMove : MonoBehaviour

{
    [Header("Movement")]

    public float moveSpeed = 6f;

    private NavMeshAgent agent;

    private Camera mainCamera;
    private Animator anim;
    void Start()

    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(true);

        if (anim == null)
        Debug.LogError("No encontré Animator en hijos");

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

        if (anim != null)
        {
            float speed = agent.velocity.magnitude; // 0 = idle, >0 = running
            anim.SetFloat("Speed", speed);
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