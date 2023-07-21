using UnityEngine;


public class Enemy4Behavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform partolPoints;
    [SerializeField] GameObject signPrefab;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask colliderLayer;

    [Header("Settings")]
    [SerializeField] float speed = 1.0f;
    [SerializeField] int detectionRange = 2;
    [SerializeField] float signDuration = 5f;


    Animator animator;
    bool signIsActive = false;
    Vector2 playerDirection = new Vector2(0, 0);
    Vector3 movingDir;
    int goingToPointNumber = 0;
    int numOfpatrolPoints;



    private void Start()
    {
        transform.position = partolPoints.GetChild(0).position;
        animator = GetComponent<Animator>();
        numOfpatrolPoints = partolPoints.childCount;

        foreach (Transform patrolPoint in partolPoints)
            AlignPointToGrid(patrolPoint);
    }


    private void Update()
    {
        if (Vector2.Distance(transform.position, partolPoints.transform.GetChild(goingToPointNumber).position) > 0)
            transform.position = Vector2.MoveTowards(transform.position, partolPoints.transform.GetChild(goingToPointNumber).position, Time.deltaTime * speed);
        else
            goingToPointNumber = (goingToPointNumber == numOfpatrolPoints - 1) ? 0 : (goingToPointNumber + 1);


        movingDir = Vector3.Normalize((partolPoints.transform.GetChild(goingToPointNumber).position - transform.position));
        animator.SetFloat("Vertical", movingDir.y);
        animator.SetFloat("Horizontal", movingDir.x);
    }

    void PlaceSign()
    {
        if (!signIsActive)
        {
            Vector3 signPos = transform.position + new Vector3(playerDirection.x, playerDirection.y, 0f);
            if (Physics2D.OverlapCircle(signPos, 0.1f, playerLayer + colliderLayer) == null)
            {
                signIsActive = true;
                GameObject sign = Instantiate(signPrefab, signPos, Quaternion.identity);
                AlignPointToGrid(sign.transform);
                Destroy(sign, signDuration);
                Invoke("ResetSign", signDuration);
            }
        }
    }

    void ResetSign()
    {
        signIsActive = false;
    }

    void FixedUpdate()
    {

        foreach (Vector2 dir in new Vector2[] { Vector2.down, Vector2.up, Vector2.left, Vector2.right })
        {
            if (Physics2D.Raycast(transform.position, dir, detectionRange, playerLayer).collider != null)
            {
                playerDirection = dir;
                break;
            }
            else
                playerDirection = Vector2.zero;
        }

        if (playerDirection.magnitude != 0)
            PlaceSign();

    }


    void AlignPointToGrid(Transform point)
    {
        float nearestMultipleX = Mathf.FloorToInt(point.position.x) + 0.5f;
        float nearestMultipleY = Mathf.FloorToInt(point.position.y) + 0.5f;
        Vector3 newPosition = new Vector3(nearestMultipleX, nearestMultipleY, point.position.z);
        point.position = newPosition;
    }

}
