using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJ : MonoBehaviour
{
    public float ss = -2;   //���� ���� x�� ó��
    public float es = 2;    //���� ���� x�� ��
    public float StartTime = 1;  //����
    public float SpawnStop = 40; //���������� �ð�
    public GameObject monster;
    public GameObject monster2;
    public GameObject BossJ;


    bool swi = true;
    bool swi2 = true;

    [SerializeField]
    GameObject Intro; //�ؽ�Ʈ ������Ʈ
    public GameObject BossWarning;
    //public GameObject GameClear;

    private void Awake()
    {
        BossWarning.SetActive(false);
        //GameClear.SetActive(false);
        //StartCoroutine(ShowIntroAndHide());
    }

    /*IEnumerator ShowIntroAndHide()
    {
        //Intro.SetActive(true); // Intro�� ���̰� ����

        // 5�� ���
        //yield return new WaitForSeconds(3);

        //Intro.SetActive(false); // Intro�� ����� ����
    }*/


    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);
    }

    void Stop()
    {
        swi = false;

        StopCoroutine("RandomSpawn");

        //�ι�° ���� �ڷ�ƾ
        StartCoroutine("RandomSpawn2");

        //30�ʵڿ� 2��° ���ͽ����� ���߱�
        Invoke("Stop2", SpawnStop + 40);



    }
    void WarningAct()
    {
        BossWarning.SetActive(false);
    }
    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");

        Vector3 pos = new Vector3(0, 3.0f, 0);

        BossWarning.SetActive(true);

        //��������
        Instantiate(BossJ, pos, Quaternion.identity);
        Invoke("WarningAct", 1.5f);




    }
    //�ڷ�ƾ���� �����ϰ� �����ϱ�
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
    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1�ʸ���
            yield return new WaitForSeconds(StartTime + 1);
            //x�� ����
            float X = Random.Range(ss, es);
            //X�� ������ y�� �ڱ��ڽŰ�
            Vector2 r = new Vector2(X, transform.position.y);
            //����2 ����
            Instantiate(monster2, r, Quaternion.identity);
        }
    }
}
