using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
   private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text Win;
    public Text Lives;

    private int scoreValue = 0;
    private int livesValue = 3;

    private int move = 0;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private bool facingRight = true;
     private int gameover = 0;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        Lives.text = livesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetInteger("State", 0);
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        //add keys
        
         if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 3);
            }
             

             if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("State", 1);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State", 0);
            }

    
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            Lives.text = livesValue.ToString();
            collision.collider.gameObject.SetActive(false);

            if(livesValue == 0)
            {
                 Win.text = "You lose! Game by Nicole F ";

            }
        }

       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

         if (scoreValue == 4)
       {
        if(move == 0)
        {
        transform.position = new Vector3(-17.0f, 0.5f, 3.0f);
           move = 1;
           
        }
          
       }
        if (scoreValue == 8)
        {
       
        musicSource.clip = musicClipTwo;
        if (gameover == 0)
        {
        gameover = 1;
        musicSource.Play();
        musicSource.loop = false;
        Win.text = "You win! Game by Nicole F ";
        }
        

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Ground")
        {
          //  if (move == 0)
            //{
           //     anim.SetInteger("State", 0);
          // }
            
            
            if (Input.GetKey(KeyCode.W))
            {
                
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                 
            }
           
        }

        // if (collision.collider.tag != "Ground")
        // {
        //    anim.SetInteger("State", 3);
       // }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}