using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBulletJ : MonoBehaviour
{
    public float Speed = 4.0f;
    public int Attack = 10;
    public GameObject effect;



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


        if (collision.tag == "Enermy")
        {
            collision.gameObject.GetComponent<EnermyJ1>().Damage(Attack);

            //����Ʈ �����ϱ�
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
        if (collision.tag == "Enermy2")
        {
            collision.gameObject.GetComponent<EnermyJ2>().Damage(Attack);

            //����Ʈ �����ϱ�
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
        if (collision.tag == "BossJ")
        {
            collision.gameObject.GetComponent<BossJ>().Damage(Attack);

            //����Ʈ �����ϱ�
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�ʵڿ� �����
            Destroy(go, 1);


            //�̻��� �����
            Destroy(gameObject);


        }
    }




    //�����ɶ� ȣ��Ǵ� �Լ�
    private void OnDestroy()
    {

    }






}
