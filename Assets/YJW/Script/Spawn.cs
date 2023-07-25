using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float ss = -2;   //���� ���� x�� ó��
    public float es = 2;    //���� ���� x�� ��
    public float StartTime = 1;  //����
    public float StartTime2 = 3;  //����
    public float SpawnStop = 10; //���������� �ð�

    public float StartTime3 = 9.5f;  //����
    public GameObject monster;
    public GameObject monster2;
    public GameObject monster3;

    bool swi = true;
    bool swi2 = true;

    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);


        StartCoroutine("RandomSpawn2");
        Invoke("Stop2", SpawnStop);

        StartCoroutine("RandomSpawn3");
        Invoke("Stop3", SpawnStop);

    }

    void Stop() 
    {
        swi = false;
        StopCoroutine("RandomSpawn");


        //�ι�° ���� �ڷ�ƾ
        StartCoroutine("RandomSpawn2");

        //30�� �ڿ� 2��° ���ͽ����� ���߱�

        Invoke("Stop2", SpawnStop + 20);

        
    }



    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");
    }


    void Stop3()
    {
        swi = false;
        StopCoroutine("RandomSpawn");


    }


    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            //1�ʸ���
            yield return new WaitForSeconds(StartTime);
            //x�� ����
            float X = Random.Range(ss, es);
            //X�� ������ y�� �ڱ��ڽŰ�
            Vector2 r = new Vector2(X, transform.position.y);
            //���� ����
            Instantiate(monster, r, Quaternion.identity);
        }
    }


    //�ڷ�ƾ���� �����ϰ� �����ϱ�
    IEnumerator RandomSpawn3()
    {
        while(swi)
        {
            //1�ʸ���
            yield return new WaitForSeconds(StartTime3);
            //x�� ����
            
            //X�� ������ y�� �ڱ��ڽŰ�
            Vector2 r = new Vector2(transform.position.x, transform.position.y);
            //���� ����
            Instantiate(monster3,r,Quaternion.identity);
        }
    }
    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1�ʸ���
            yield return new WaitForSeconds(StartTime2);
            //x�� ����
            float X = Random.Range(ss, es);
            //X�� ������ y�� �ڱ��ڽŰ�
            Vector2 r = new Vector2(X, transform.position.y);
            //���� ����
            Instantiate(monster2, r, Quaternion.identity);
        }
    }



}
