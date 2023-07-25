using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MBullet : MonoBehaviour
{
    public GameObject target;
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;




    // Start is called before the first frame update
    void Start()
    {
        
        //�÷��̾� �±׷� ã��
        target = GameObject.FindGameObjectWithTag("Player");
        //A - B   �÷��̾� - �̻���    
        dir = target.transform.position - transform.position;
        //���⺤�͸� ���ϱ� �������� 1��ũ��� �����.
        dirNo = dir.normalized;
        

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(dirNo * Speed * Time.deltaTime);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //�÷��̾� �����
           // Destroy(collision.gameObject);
            //�̻��� �����
           // Destroy(gameObject);
        }
    }



}
