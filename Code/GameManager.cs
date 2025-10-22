using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private Coin coin;
    [SerializeField] private Enemy_Ghost enemy_Ghost;
    [SerializeField] private Enemy_Turtle enemy_Turtle;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI score_txt;
    [SerializeField] private Transform playerStart;
    public AudioSource audioController;
    public Transform[] coinsTeleports;
    public Transform[] ghostTeleports;
    public Transform[] turtleTeleports;
    public Transform[] teleSaida;
    public GameObject coinPrefab; 
    public GameObject ghostPrefab;
    public GameObject turtlePrefab;


    [Header("Variaveis")]
    [SerializeField] private Vector3 player_StartPosition;
    public AudioClip dead_Fx;
    public AudioClip coin_Fx;
    public AudioClip kick_Fx;
    public AudioClip jump_Fx;
    public AudioClip running_Fx;


    public int score_num;


    void Start()
    {

        StartCoroutine(IEteleportCoins());
        StartCoroutine(IEteleportGhost());
        StartCoroutine(IEteleportTurtle());
        restart();
    }

    void Update()
    {
        scorePoints();
    }

    void scorePoints()
    {
        score_txt.text = score_num.ToString(); 
    }

    IEnumerator IEteleportCoins()
    {
        while (true)
        {
            //Sorteia uma posição aleatória do array
            int randomIndex = Random.Range(0, coinsTeleports.Length);
            Transform spawnPoint = coinsTeleports[randomIndex];

            //Instancia a moeda
            GameObject coinInstance = Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);

            //Verifica se é o lado esquerdo
            Coin coinScript = coinInstance.GetComponent<Coin>();
            if (coinScript != null)
            {
                // Se estiver à esquerda do meio do mapa, ela olha para a esquerda
                coinScript.isLookLeft = spawnPoint.position.x < 0 ? false : true;
            }
            

            //Espera 3 segundos antes de gerar outra moeda
            yield return new WaitForSeconds(10f);

        }

    }
    IEnumerator IEteleportGhost()
    {
        while (true)
        {

            int ramdomIndex = Random.Range(0, ghostTeleports.Length);
            Transform spawnGhost = ghostTeleports[ramdomIndex];

            GameObject ghostInstance = Instantiate(ghostPrefab, spawnGhost.position, Quaternion.identity);

            Enemy_Ghost ghostScript = ghostInstance.GetComponent<Enemy_Ghost>();
            if (ghostScript != null)
            {
                ghostScript.isLookLeft = spawnGhost.position.x < 0 ? false : true;
            }

            yield return new WaitForSeconds(20f);
        }
    }

    IEnumerator IEteleportTurtle()
    {
        while (true)
        { 
            int ramdomIndex = Random.Range(0, turtleTeleports.Length);
            Transform spawnTurtle = turtleTeleports[ramdomIndex];

            GameObject turtleInstance = Instantiate(turtlePrefab, spawnTurtle.position, Quaternion.identity);

            Enemy_Turtle turtleScript = turtleInstance.GetComponent<Enemy_Turtle>();
            if(turtleScript != null)
            {
                turtleScript.isLookLeft = spawnTurtle.position.x < 0 ? false : true;    
            }

            yield return new WaitForSeconds(15f);
        }

    }

    public void restart()
    {
       player.transform.position = new Vector3(playerStart.transform.position.x, playerStart.transform.position.y);
    }
}
