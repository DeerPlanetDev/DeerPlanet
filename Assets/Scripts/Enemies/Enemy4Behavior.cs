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
    [SerializeField] bool damagesPlayer = true;


    Animator animator;
    bool signIsActive = false;
    Vector2 playerDirection = new Vector2(0, 0);
    Vector3 movingDir;
    int goingToPointNumber = 0;
    int numOfpatrolPoints;

    //En caso de realizar daño necesita
    //[SerializeField] AudioClip damageSfx; //Sonido ------------------------ AGREGAR SONIDO AL UNIR RAMA DE ENEMIGOS 1 Y 2
    [SerializeField] int damage = -30; //Daño realizado
    [SerializeField] int notScore = -1; //Puntaje a restar



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


    //Reducir la vida del jugador y su puntaje cuando entre en contacto con el enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(damagesPlayer == true)
        {
            //Comparamaos si entraron en contacto
            if (other.gameObject.CompareTag("Player"))
            {
                //Hacemos que suene el sonido de daño
                //other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx); ------------------ QUITAR COMENTARIO AL UNIR RAMA
                //Modiicamos la salud del jugador
                PlayerHealth.instance.ModifyHP(damage);
                //Modificamos el puntaje del jugador
                LevelManager.instance.IncreaseScore(notScore, 0); //Solo modificaremos el puntaje (primer parametro)
            }
        }
    }

}
