using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject spawn;

    private bool pulando = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Horizontal"));
        float x = Input.GetAxis("Horizontal");
        if (x < 0)
        {
            //esquerda
            sr.flipX = true;
            animator.SetBool("correndo", true);
        }
        else if (x == 0) {
            //fica parado
            animator.SetBool("correndo", false);
        }
        else 
        {
            //direita
            sr.flipX = false;
            animator.SetBool("correndo", true);
        }
        transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime * playerSpeed);
        pulo();

    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Chao") {
            pulando = false;
            animator.SetBool("pulando", false);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            morre();
        }

        if (collision.gameObject.tag == "Morte")
        {
            morre();
        }

        if (collision.gameObject.tag == "Ganhemo")
        {
            GameController.GanhouJogo();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chao")
        {
            pulando = true;
        }
    }

    private void pulo() {
        if (Input.GetKeyDown(KeyCode.Space) && !pulando)
        {
            rb.AddForce(
                new Vector2(0f, jumpSpeed), ForceMode2D.Impulse
            );
            //pulando = true;
            animator.SetBool("pulando", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruta") {
            collision.gameObject.GetComponent<Animator>().SetBool("coletando", true);
            Destroy(collision.gameObject, 1f);
            GameController.setPontos(1);
        }
    }
    private void morre() {
        GameController.perdeVida();
        transform.position = spawn.transform.position;
    }
}
