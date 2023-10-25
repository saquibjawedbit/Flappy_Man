using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private float JumpHeight = 3.6f;

    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioSource source;

    [SerializeField] private List<AudioClip> clip_;

    public chaser chas;

    [HideInInspector]
    public int Score = 0;
    private int jumpCount = 2;

    private bool gameStarted = false;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float attackRadius = 0.5f;
    public GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !gameStarted)
        {
            controller.SetActive(true);
            anim.SetTrigger("Run");
            gameStarted = true;
        }

        if(transform.position.x <= -4.8f)
        {
            chas.SetMove(true);
            anim.SetTrigger("reverse");
        }

    }

    public void PlayerJump()
    {
        if (jumpCount > 2) return;
        anim.SetTrigger("Jump");
        source.clip = jump;
        source.Play();
        rb.velocity = Vector2.up * JumpHeight * Time.deltaTime * 51;
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.IsTouchingLayers(3))
        {
            jumpCount = 0;
        }
    }

    public void Attack()
    {
        source.clip = clip_[Random.Range(0, clip_.Count)];
        source.Play();
        anim.SetTrigger("attack_" + Random.Range(1, 5));
        Collider2D coll = Physics2D.OverlapCircle(transform.position -offset, attackRadius, LayerMask.GetMask("Enemy"));
        if (coll == null) return;
        Enemy enmey = coll.transform.GetComponent<Enemy>();
        if (enmey != null) enmey.Dead();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position-offset, attackRadius);
    }

    public void CandyEffect()
    {
        StartCoroutine(SetNorm());
        JumpHeight *= 2;
    }

    IEnumerator SetNorm()
    {
        yield return new WaitForSeconds(10f);
        JumpHeight /= 2;

    }
}
