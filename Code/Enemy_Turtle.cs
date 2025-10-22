using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Turtle : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    public Player player { get; set; }

    [Header("Components")]
    [SerializeField] private Rigidbody2D enemy_Turtle_rb;
    [SerializeField] private Animator enemy_Turtle_Anim;

    [Header("Variaveis")]
    [SerializeField] private float turtle_Speed;
    public bool isVulnerable;
    public bool isLookLeft;


    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (enemy_Turtle_rb == null) { return; }

        enemy_Turtle_rb.linearVelocityX = (isLookLeft == true) ? -turtle_Speed : turtle_Speed;
        if(isLookLeft == true)
        {
            this.transform.localScale = new Vector3(-1,1,1);
        }

    }

    

    private void OnCollisionEnter2D(Collision2D col)
    {
        Teleports teleportTurtle = col.gameObject.GetComponent<Teleports>();

        if ((teleportTurtle != null))
        {
            int index = teleportTurtle.targetIndexTeleports;
            if(index >= 0 && index < gameManager.teleSaida.Length)
            {
                transform.position = gameManager.teleSaida[index].position;
            }
        }

        if (col.gameObject.CompareTag("Player") && !isVulnerable)
        {
            gameManager.restart();
            gameManager.audioController.PlayOneShot(gameManager.dead_Fx);
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("Player") && isVulnerable)
        {
            gameManager.score_num += 500;
            gameManager.audioController.PlayOneShot(gameManager.kick_Fx);
            player.player_Anim.SetTrigger("Kick");
            Destroy(this.gameObject);

        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlataformDeform"))
        {
            turtle_Speed = 0;
            isVulnerable = true;
            enemy_Turtle_Anim.SetTrigger("Dead");
        }
    }

}
