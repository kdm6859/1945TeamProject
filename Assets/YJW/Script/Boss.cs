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

    public bool isHit = false;

   

    


    public GameObject mb;
    public GameObject mb2;
    public Transform tr;
    public Transform tr2;


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;


    private void LateUpdate()
    {
        if (transform.position.x >= 0.75f)
            flag *= -1;
        if (transform.position.x <= -0.75f)
            flag *= -1;

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
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

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //���� ��Ÿ���� Hide�Լ� 1�ʵ� ����
        Invoke("Hide", 1);
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
            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate); //3�ʸ��� ���� �̻��� �߻�

        }
    }

    IEnumerator Think() //���� �ڷ�ƾ �Լ���
    {
        yield return new WaitForSeconds(0.1f);

        int rendAction = Random.Range(0, 3);
        switch (rendAction)
        {
            case 0:

            case 1:
                StartCoroutine(Bosspattern1());


                break;

            case 2:

            case 3:
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
