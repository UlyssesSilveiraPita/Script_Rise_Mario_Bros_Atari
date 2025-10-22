using UnityEngine;

public class Enemy_Ghost : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;

    [Header("Components")]
    [SerializeField] private Rigidbody2D ghost_rb;


    [Header("Variaveis")]
    [SerializeField] private float ghost_Speed;
    public bool isLookLeft;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (ghost_rb == null) { return; }
        ghost_rb.linearVelocityX = (isLookLeft == true) ? - ghost_Speed : ghost_Speed;

    }

    private void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            gameManager.restart();
            Destroy(this.gameObject);
            
        }
    }

}
