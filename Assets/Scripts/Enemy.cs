using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    private bool dead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10f);
    }
    // Update is called once per fram

    private void Update()
    {
        if (dead) return;
        transform.Translate(Vector3.right * -1.2f * Time.deltaTime);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (dead) return;
        if(collision.collider.IsTouchingLayers(3))
        {
            rb.velocity = Vector2.up * 5f * Time.deltaTime * 50;
        }
    }

    IEnumerator attack()
    {
        if (dead) yield return null;
        yield return new WaitForSeconds(0.25f);
        GameManager.instance.GameOver();
        this.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dead) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(attack());
        }
    }

    public void Dead()
    {
        print("Dead");
        dead = true;
        rb.isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Dead");
        Destroy(gameObject, .8f);
    }
}
