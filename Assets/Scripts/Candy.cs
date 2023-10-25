using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    [SerializeField] private AudioSource source;
    [SerializeField] private Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController controller = collision.transform.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.CandyEffect();
                source.Play();
                anim.SetTrigger("player");
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
