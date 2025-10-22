using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;

    [Header("Components")]
    [SerializeField] private Rigidbody2D coin_rb;

    [Header("Variaveis")]
    [SerializeField] private float coin_Speed;
    public  bool isLookLeft;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (coin_rb == null) return;
        coin_rb.linearVelocityX = (isLookLeft == true) ? -coin_Speed : coin_Speed;
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Teleports teleport = col.gameObject.GetComponent<Teleports>();

        if ((teleport != null))
        {
            int index = teleport.targetIndexTeleports;
            if (index >= 0 && index < gameManager.teleSaida.Length)
            {
                transform.position = gameManager.teleSaida[index].position;
            }
        }

        if (col.gameObject.CompareTag("Player"))
        {
            gameManager.score_num += 100;
            Destroy(this.gameObject);
        }

    }

}
