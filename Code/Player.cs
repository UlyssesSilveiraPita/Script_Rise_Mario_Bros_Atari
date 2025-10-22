using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Enemy_Turtle enemy_Turtle {  get; set; }   

    [Header("Componentes")]
    [SerializeField] private Rigidbody2D player_rb;
    [SerializeField] private GameObject plataformDeform_Prefab;
    [SerializeField] private Transform pointPlataformDeform;
    [SerializeField] private AudioSource playerAudioSource;
    public Animator player_Anim;


    [Header("Variaveis")]
    [SerializeField] private int player_speed;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isLookLeft;
    [SerializeField] private bool isJump;
    [SerializeField] private bool isKick;


    void Start()
    {

    }

    void Update()
    {
        AnimatorManager();
        InputManager();

    }

    //--------------------------- Movimentacao Player ---------------------------//
    void InputManager()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        isWalking = horizontal != 0;
        player_rb.linearVelocity = new Vector2(horizontal * player_speed, player_rb.linearVelocity.y);
        isWalking = (horizontal != 0)? true : false;

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            playerJump();
        }

        if(horizontal > 0 && isLookLeft == true)
        {
            flip();
        }
        else if(horizontal < 0 && isLookLeft == false)
        {
            flip();
        }


    }

    void playerJump()
    {
        isJump = true;
        player_rb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
        playerAudioSource.PlayOneShot(gameManager.jump_Fx);
    }
    void flip()
    {
        isLookLeft = !isLookLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //------------------------------- Controles -------------------------------//

    void AnimatorManager()
    {
        player_Anim.SetBool("isWalk", isWalking);
        player_Anim.SetBool("isJump", isJump);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        switch(col.gameObject.tag)
        {
            case "Ground":
                isJump = false;
                break;

            case "Coin":
               // player_Anim.SetTrigger("Kick");
               playerAudioSource.PlayOneShot(gameManager.coin_Fx);
                break;

                               
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Ground"))
        {
            StartCoroutine(IEplataformDeforme());
        }
    }

    IEnumerator IEplataformDeforme()
    {

        GameObject plataformInstatiate = Instantiate(plataformDeform_Prefab, pointPlataformDeform.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.4f);
        Destroy(plataformInstatiate);
    }

    void running()
    {
        playerAudioSource.volume = 0.2f;
        playerAudioSource.PlayOneShot(gameManager.running_Fx);
    }

}
