using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    int hp = 50;
    float speed = 2;
    float Delay = 1.5f;
    public Transform ms;
    public Transform ms2;
    public GameObject bullet;
    //������ ��������
    public GameObject Item = null;
    //Rigidbody2D rb;

    float dis = 0;
    Vector2 startPos;
    float curTime = 0;
    float endTime = 5;
    bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        //�ѹ� ȣ��
        Invoke("CreateBullet", 1f);
        StartCoroutine("startMove");
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;

        if(curTime >= endTime && !end)
        {
            //Debug.Log("?????");
            StopCoroutine("startMove");
            StartCoroutine("endMove");
            end = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void ItemDrop()
    {
        //������ ����
        Instantiate(Item, transform.position, Quaternion.identity);

    }

    public void Damage(int attack)
    {
        hp -= attack;

        if (hp <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
    }

    IEnumerator startMove()
    {
        Vector2 targetPos = startPos + Vector2.down * 3.5f;

        while (targetPos != (Vector2)transform.position)
        {
            //Debug.Log(startPos.position + ", " + transform.position);
            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime*2);
            //transform.Translate(Vector2.down * speed * Time.deltaTime);
            yield return null;
        }
        //Debug.Log("��");


    }

    IEnumerator endMove()
    {
        float x;
        float y = 1;
        int leftRight = Random.Range(0, 2);
        Debug.Log(leftRight);
        if (leftRight == 0)
        {
            x = -5;
        }
        else
        {
            x = 5;
        }
        int count = 0;
        while (true)
        {
            transform.Translate(new Vector2(x, y) * Time.deltaTime);
            count++;
            if(count == 10)
            {
                x = x * 0.9f;
                y = y * 1.1f;
                count = 0;
            }
            
            
            yield return null;
        }
    }
}
