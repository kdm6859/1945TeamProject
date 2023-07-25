using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float ss = -2.3f; //���� ���� x�� ó��
    float es = 2.3f; //���� ���� y�� ó��
    public float delayTime = 2; //����
    public float spawnStop = 10; //���� ������ �ð�
    public GameObject monster;
    public GameObject monster2;
    public GameObject Boss;

    bool swi = true;
    bool swi2 = true;

    [SerializeField]
    GameObject textBossWarning; //���� ���� �ؽ�Ʈ ������Ʈ

    private void Awake()
    {
        //���� ���� �ؽ�Ʈ ��Ȱ��ȭ
        textBossWarning.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", spawnStop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");

        //�ι�° ���� �ڷ�ƾ
        StartCoroutine("RandomSpawn2");

        //30�ʵڿ� 2��° ���� ������ ���߱�
        Invoke("Stop2", spawnStop + 20);
    }

    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");

        //���� ����
        textBossWarning.SetActive(true);
        RandomSpawn3();
    }

    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            //1�ʸ���
            yield return new WaitForSeconds(delayTime);
            //x�� ����
            float x = Random.Range(ss, es);
            //x��:������ y��:�ڱ��ڽŰ�
            Vector2 r = new Vector2(x, transform.position.y);
            //���� ����
            Instantiate(monster, r, Quaternion.identity);
        }
    }

    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1�ʸ���
            yield return new WaitForSeconds(delayTime);
            //x�� ����
            float x = Random.Range(ss, es);
            //x��:������ y��:�ڱ��ڽŰ�
            Vector2 r = new Vector2(x, transform.position.y);
            //���� ����
            Instantiate(monster2, r, Quaternion.identity);
        }
    }
    void RandomSpawn3()
    {
        Instantiate(Boss, new Vector2(transform.position.x, transform.position.y - 1.8f), Quaternion.identity);
    }
}
