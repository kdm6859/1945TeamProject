using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBressJ : MonoBehaviour
{
    public GameObject effect;
    Transform tr;
    int Attack = 100;
    public int HP = 1;

    void Start()
    {
        //tr = GameObject.Find("BossJ").GetComponent<BossJ>().tr;
    }

    
    void Update()
    {
        //transform.position = tr.position;
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerJ")
        {
            //�÷��̾� �����
            Destroy(collision.gameObject);
            //�̻��� �����
            Destroy(gameObject);
        }
        if (collision.tag == "OneJ")
        {
            //�÷��̾� �����
            Destroy(collision.gameObject);
            //�̻��� �����
            Destroy(gameObject);
        }

    }
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            //ItemDrop();
            Destroy(gameObject);
        }
    }
}
