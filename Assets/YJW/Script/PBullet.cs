using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public int Attack = 1;

    //public GameObject effect;

  
   
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

            //�̻��� �����
            Destroy(gameObject);


        }

        if (collision.tag =="Monster")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //���� �浹 �����
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Monster>().Damage(Attack);


            //����Ʈ �����ϱ�
            //GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            // Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);

           
        }
    }




    //�����ɶ� ȣ��Ǵ� �Լ�
    private void OnDestroy()
    {
      
    }






}
