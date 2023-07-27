using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rintercepter : MonoBehaviour
{
    //�� ���� ���� �տ��� ������ ��ũ��Ʈ �̸��� R

    public float Speed;

    public Transform Player;
    public Transform BossPos;
    public Transform PosA;
    public Transform PosB;
    public GameObject Effect;

    float time= 0;

    Vector3 myPos;
    Vector3 playerPos;


    void Start()
    {
        myPos = transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        PosA = GameObject.FindGameObjectWithTag("Elite").transform.Find("PosA");
        PosB = GameObject.FindGameObjectWithTag("Player").transform.Find("PosB");
    }
    void Update()
    {

        if (time > 1)
        {
            Destroy(gameObject);
        }
        Debug.Log(PosB.transform.localPosition);
        Debug.Log(PosA.transform.localPosition);

        Vector3 p1 = Vector3.Lerp(myPos, PosA.transform.position, time);
        Vector3 p2 = Vector3.Lerp(PosA.transform.position, PosB.transform.position, time);
        Vector3 p3 = Vector3.Lerp(PosB.transform.position, playerPos, time);

        Vector3 p4 = Vector3.Lerp(p1, p2, time);
        Vector3 p5 = Vector3.Lerp(p2, p3, time);

        transform.position = Vector3.Lerp(p4, p5, time);
        time += Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet") ||
             collision.gameObject.CompareTag("PlayerMissle"))
        {
            Destroy(gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
        }
    }
   
}