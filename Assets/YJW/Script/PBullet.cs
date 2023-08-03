using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public float Attack = 1;
    public float enegy = 1;

    public GameObject effect;

    GameObject player;



    private void Awake()
    {
        player = GameObject.Find("Player");
    }



    void Update()
    {
        //�̻����� ���ʹ������� �����δ�.
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }


    //�ش��ڵ带 �����Ͻÿ�.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    // public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "BossArm")
        {
            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);

            Instantiate(effect, transform.position, Quaternion.identity);
            //�̻��� �����
            Destroy(gameObject);


        }
        else if (collision.tag == "Monster")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //���� �浹 �����
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Monster>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
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
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);



            //����Ʈ �����ϱ�
            // GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            // Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
        else if (collision.tag == "Boss2")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //���� �浹 �����
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<LastBoss>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);



            //����Ʈ �����ϱ�
            // GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            // Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
        else if(collision.tag =="BossHelper")
        {
            collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
            //collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);

            //�̻��� �����
            Destroy(gameObject);

        }
        else if (collision.tag == "BossHelper2")
        {
            //collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
            collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);

            //�̻��� �����
            Destroy(gameObject);

        }
    }




    //�����ɶ� ȣ��Ǵ� �Լ�
    private void OnDestroy()
    {
      
    }






}
