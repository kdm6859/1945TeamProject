using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet_dm : MonoBehaviour
{
    //public GameObject target;
    public float speed = 3f;
    Vector2 vec2 = Vector2.down;
    //Vector2 dir;
    //Vector2 dirNo;

    // Start is called before the first frame update
    void Start()
    {
        ////�÷��̾� �±׷� ã��
        //target = GameObject.FindGameObjectWithTag("Player");
        ////A - B      �÷��̾� - �̻���
        //dir = target.transform.position - transform.position;
        ////���⺤�͸� ���ϱ� �������� 1�� ũ��� �����.
        //dirNo = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(dirNo * speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(
        //    Quaternion.LookRotation(new Vector3(1, 1, 0)).eulerAngles);
        
        transform.Translate(vec2 * speed * Time.deltaTime);
    }
    
    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ////�÷��̾� �����
            //Destroy(collision.gameObject);
            ////�̻��� �����
            //Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
