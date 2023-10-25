using UnityEngine;

public class Dead : MonoBehaviour
{

    [SerializeField]
    private bool Cannon = false;

    private void Update()
    {
        if (Cannon) transform.Translate(Vector3.left * 7f * Time.deltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.instance.GameOver();
        }
        else if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
