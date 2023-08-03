using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing2 : MonoBehaviour
{
    public GameObject effect;
    public GameObject destroyEfffect;
    // public GameObject target;
    public float Speed = 3f;
    // Vector2 dir;
    //  Vector2 dirNo;




    // Start is called before the first frame update
    void Start()
    {
        /*
        //�÷��̾� �±׷� ã��
        target = GameObject.FindGameObjectWithTag("Player");
        //A - B   �÷��̾� - �̻���    
        dir = target.transform.position - transform.position;
        //���⺤�͸� ���ϱ� �������� 1��ũ��� �����.
        dirNo = dir.normalized;
        */

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(destroyEfffect, collision.transform.position, Quaternion.identity);
        }
        else if (collision.CompareTag("BombAir"))
        {
            //Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Boom"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Lazer"))
        {

            Destroy(gameObject);
        }
    }
}
