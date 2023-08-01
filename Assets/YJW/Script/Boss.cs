using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.PlayerSettings;

public class Boss : MonoBehaviour
{
    public float HP = 2000;

    public GameObject effect;

    int flag = 1;
    int speed = 2;
    Vector3 pos; //������ġ

    float delta = 1.0f; // ��(��)�� �̵������� (x)�ִ밪


    public bool isHit = false;

   string bossMode = "mode1"; //������ 1������ mode�� ��Ʈ������ �����Ͽ���.  

        

    


    public GameObject mb;
    public GameObject mb2;
    public Transform tr;
    public Transform tr2;


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;


    private void LateUpdate()
    {
       
        //2������ �̵����� ����.
        Vector3 v = pos;

        v.x += delta * Mathf.Sin(Time.time * speed);

        // �¿� �̵��� �ִ�ġ �� ���� ó���� �̷��� ���ٿ� ���ְ� �ϳ׿�.

        transform.position = v;
    }

    public void Damage(float attack)
    {
        HP -= attack;

        Debug.Log("������ �޾���");
        StartCoroutine(CoolHit());

        if (HP <= 0)
        {
            HP = 0;

            Destroy(gameObject);


            Instantiate(effect, transform.position, Quaternion.identity);

            //    Destroy(effect, 0.5f);

        }
    }


    private void Awake()
    {
        StartCoroutine(Think());
       //transform.Find("BPpos3") .gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        //���� ��Ÿ���� Hide�Լ� 1�ʵ� ����
        Invoke("Hide", 1);
      
            Start2Paze();
        
    }

    private void Start2Paze()
    {
        StartCoroutine(BossMissle()); //�ڷ�ƾ ���� �Լ� ����
        StartCoroutine(CircleFire()); //�ڷ�ƾ ���� �Լ� ����
    }

    void Hide()
    {
        //���� �ؽ�Ʈ ��ü�̸� �˻��ؼ� ����
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator BossMissle()
    {
        while (true)
        {
            //�̻��� �ΰ� 
            Instantiate(mb, tr.position, Quaternion.identity);
            Instantiate(mb, tr2.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    //���������� �̻��� �߻�
    IEnumerator CircleFire()
    {
        float attackRate = 3;//�����ֱ�
        int count = 30;    //�߻�ü ���� ����
        float intervalAngle = 360 / count;  //�߻�ü ������ ����
        float weightAngle = 0; //���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)


        //�� ���·� ����ϴ� �߻�ü ���� (count ���� ��ŭ)
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                //�߻�ü ����
                GameObject clone = Instantiate(mb2, transform.position, Quaternion.identity);
                //�߻�ü �̵� ����(����)
                float angle = weightAngle + intervalAngle * i;
                //�߻�ü �̵� ���� (����)
                //Cos(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //Sin(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                //�߻�ü �̵� ���� ����
                //clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                Debug.Log("�߻�Ǿ���");
            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate); //3�ʸ��� ���� �̻��� �߻�

        }
    }

    IEnumerator Think() //���� �ڷ�ƾ �Լ��� 2���������� ���� ����.
    {
        yield return new WaitForSeconds(0.1f);

        int rendAction = Random.Range(0, 5);
        switch (rendAction)
        {
            case 0:

            case 1:
                StartCoroutine(Bosspattern1());

                break;

            case 2:

            case 3:

            case 4:
                StartCoroutine(Bosspattern2());

                break;
           



        }

        IEnumerator Bosspattern1()
        {
           
            BossP1.GetComponent<BossPattern1>().Shot();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Think());
        }

        IEnumerator Bosspattern2()
        {
            
            BossP2.GetComponent<BossPattern2>().Shot();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Think());
        }

      





    }

    IEnumerator CoolHit()
    {
        var hit = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        isHit = true;
        hit.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hit.color = Color.white;
        isHit = false;
    }
    
}
