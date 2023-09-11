using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HelperBullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public float Attack = 1;

    public GameObject effect;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        //�̻����� ���ʹ������� �����δ�.
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("InterCepter") || collision.CompareTag("Elite")
            ||collision.CompareTag("Monster"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.tag == "BossArm")
        {
            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);
            Instantiate(effect, transform.position, Quaternion.identity);

            //GameObject go = 
            //�̻��� �����
            Destroy(gameObject);


        }
        else if (collision.tag == "Monster")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //���� �浹 �����
            //Destroy(collision.gameObject);
            if (collision.GetComponent<Monster_dm>() == null && 
                collision.GetComponent<Boss_dm>() == null && 
                collision.GetComponent<BossArm_dm>() == null &&
                collision.GetComponent<Octopus_dm>() == null)
                collision.gameObject.GetComponent<Monster>().Damage(Attack);
            Instantiate(effect, transform.position, Quaternion.identity);




            //����Ʈ �����ϱ�
            // GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            // Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
        else if (collision.tag == "Boss")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //���� �浹 �����
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Boss>().Damage(Attack);
            Instantiate(effect, transform.position, Quaternion.identity);



            //����Ʈ �����ϱ�
            //GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            // Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
        else if (collision.tag == "BossHelper")
        {
            collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
            Instantiate(effect, transform.position, Quaternion.identity);
            //�̻��� �����
            Destroy(gameObject);

        }
        else if (collision.tag == "BossHelper2")
        {
        
            collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
            Instantiate(effect, transform.position, Quaternion.identity);
            //�̻��� �����
            Destroy(gameObject);

        }
        else if (collision.tag == "Boss2")
        {
        
            collision.gameObject.GetComponent<LastBoss>().Damage(Attack);
            Instantiate(effect, transform.position, Quaternion.identity);


            Destroy(gameObject);

        }
    }
}
