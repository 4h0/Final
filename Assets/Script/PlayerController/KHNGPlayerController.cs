using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHNGPlayerController : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private Collider2D playerCollider2D;
    private Rigidbody2D playerRigidBody2D;
    private AudioSource soundSource;    
    private KHNGGameController gameControllerReference;

    private Vector2 movement;

    public bool canMove;
    public bool die;

    public float speed;

    private float moveHorizontal;
    private float moveVertical;

    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider2D = GetComponent<Collider2D>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        soundSource = GetComponent<AudioSource>();

        gameControllerReference = FindObjectOfType<KHNGGameController>();
    }

    private void Start()
    {
        die = false;
    }

    private void Update()
    {
        //movement logic
        if (die)
        {
            PlayerDie();
        }
    }

    private void FixedUpdate()
    {
        if (gameControllerReference.canMove)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            movement = new Vector2(moveHorizontal, moveVertical);

            playerRigidBody2D.velocity = movement * speed * Time.deltaTime;
        }
    }    

    private void PlayerDie()
    {
        gameControllerReference.canMove = false;
        gameControllerReference.endGame = true;

        playerSprite.enabled = false;
        playerCollider2D.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            die = true;
        }
        if(collision.gameObject.tag == "CheckPoint")
        {
            GameLoader.score += 3;
            soundSource.Play();

            Destroy(collision.gameObject);

            gameControllerReference.key--;
        }
    }
}
