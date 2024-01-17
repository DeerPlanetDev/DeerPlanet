using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] InputActionReference movement;
    [SerializeField] LayerMask ColliderLayer;
    [SerializeField] float timeToMove = 0.2f;
    private Vector3 origPos, targetPos;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isMoving = false;


    void Awake()
    {
        gameObject.GetComponent<AudioSource>().volume = GameSettings.sfxVolume;
    }


    // Start is called before the first frame update
    void Start()
    {
        //align to tile 
        float nearestMultipleX = Mathf.FloorToInt(transform.position.x) + 0.5f;
        float nearestMultipleY = Mathf.FloorToInt(transform.position.y) + 0.5f;
        Vector3 newPosition = new Vector3(nearestMultipleX, nearestMultipleY, transform.position.z);
        transform.position = newPosition;




        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject eventSystem = GameObject.Find("EventSystem");
        GameObject inputUI = GameObject.Find("PlayerInputUI");


        if (eventSystem != null)
            eventSystem.transform.SetParent(null);
        if (inputUI != null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
                inputUI.transform.SetParent(canvas.transform);
        }




    }



    // Update is called once per frame
    void Update()
    {
        moveInput = movement.action.ReadValue<Vector2>();

        if (moveInput.magnitude != 0 && moveInput.magnitude == 1 && !isMoving)
        {
            StartCoroutine(MovePlayer(moveInput));
        }
    }


    private IEnumerator MovePlayer(Vector3 direction)
    {
        void Stop()
        {
            isMoving = false;
            animator.SetFloat("x_dir", 0);
            animator.SetFloat("y_dir", 0);
        }

        if (direction.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;


        animator.SetFloat("x_dir", Mathf.Abs(direction.x));
        animator.SetFloat("y_dir", direction.y);


        isMoving = true;
        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;

        if (Physics2D.OverlapCircle(targetPos, 0.2f, ColliderLayer))
        {
            Stop();
            yield break;
        }

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        Stop();

    }





}

