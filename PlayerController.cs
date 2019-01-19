using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anim;
    public BoxCollider2D boxCol;
    public CircleCollider2D circeCol;


    public float movementSpeed;
    public Text scoreText;
    int score;

    float horizontalMovementSpeed;
    bool isJumping = false;
    bool isHurt = false;
    bool isCrouch = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();

        isJumping = false;
        isHurt = false;
        score = 0;
   
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;

        anim.SetFloat("speed", Mathf.Abs(horizontalMovementSpeed));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(Input.GetKey(KeyCode.C))
        {
            isCrouch = true;
            anim.SetBool("isCrouch", true);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            isCrouch = false;
            anim.SetBool("isCrouch", false);
        }
    }

    public void OnLanding()
    {
        isJumping = false;
        anim.SetBool("isJumping", false);

    }

    /*public void OnCrouch()
    {
        isCrouch = true;
        anim.SetBool("isCrouch", true);
    }*/

    void OnTriggerEnter2D(Collider2D col)

    {   if (col.gameObject.tag == "Star")
        {
            score++;
            Destroy(col.gameObject);
        }
        else if(col.gameObject.tag == "Enemy")
        {
             if (isJumping)
            {
                Destroy(col.gameObject);
                score++;
            }
                    
                
            else
            {
                anim.SetBool("isHurt",true);
                boxCol.enabled = false; 
                circeCol.enabled = false; 


                 }
            if (boxCol.enabled == false)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime,false, isJumping);
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
            if(other.gameObject.CompareTag("Platform"))
        {   
            transform.SetParent(other.transform);
        }

           
        
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            transform.SetParent(null);
    }
    
}
