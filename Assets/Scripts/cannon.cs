using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{

    private Animator anim;
    [SerializeField]
    private float fireRate = 10f;

    private float NextTimetofire = 0f;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        NextTimetofire = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(NextTimetofire < 0)
        {
            //attack
            anim.SetTrigger("attack");
            GameObject obj = Instantiate(prefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
            Destroy(obj, 8f);
            NextTimetofire = fireRate;
        }
        else
        {
            NextTimetofire -= Time.deltaTime;
        }

    }
}
