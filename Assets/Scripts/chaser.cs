using UnityEngine;

public class chaser : MonoBehaviour
{

    private Animator anim;
    private Transform player;

    [SerializeField]
    private float range = 1.8f;

    private AudioSource source;

    private bool Move = false;
    private bool start = false;
    private Vector3 initialPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        source = GetComponent<AudioSource>();
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !start)
        {
            anim.SetTrigger("attack");
            source.Play();
            anim.SetTrigger("Run");
            start = true;
        }
        if(Move)
        {
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
            if(Vector2.Distance(player.position, transform.position) <= range)
            {
                anim.SetTrigger("attack");
                {
                    RaycastHit2D coll = Physics2D.CircleCast(transform.position, 1f, Vector2.right);
                    if (coll.collider.tag == "Player")
                    {
                        source.Play();
                        print("Game Over");
                        GameManager.instance.GameOver();
                        anim.SetTrigger("idle");
                        this.enabled = false;
                    }
                }
            }
        }


    }

    public void SetMove(bool move)
    {
        Move = true;
    }

    public void ResetEnemy()
    {
        Move = false;
        transform.localPosition = initialPos;
        player.position = new Vector3(-3.85f, 2.2f, 0);
        Camera.main.transform.localPosition = new Vector3(-0.331f, 0.138f, -10);
    }
}
