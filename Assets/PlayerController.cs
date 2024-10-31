using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float maxJumpDistance = 8f;
    [SerializeField] Transform waterPrefab;

    private Rigidbody rb;
    private Camera playerCamera;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool hasJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving && !hasJumped)
        {
            MovePlayerToClickPosition();
        }

        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    void MovePlayerToClickPosition()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            if (Vector3.Distance(transform.position, hit.point) > maxJumpDistance)
            {
                return;
            }

            Cube cube = hit.transform.parent.GetComponent<Cube>();
            if (cube != null && cube.cubeType == Cube.Cubes.water)
            {
                Debug.Log("Nemùžeš skákat na vodu!");
                return;
            }

            targetPosition = hit.point;
            isMoving = true;
            hasJumped = true;
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        if (hasJumped)
        {
            rb.velocity = new Vector3(direction.x * moveSpeed, jumpHeight, direction.z * moveSpeed);
            hasJumped = false;
        }
        else
        {
            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                             new Vector3(targetPosition.x, 0, targetPosition.z)) < 0.1f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            isMoving = false;
        }
    }
}
