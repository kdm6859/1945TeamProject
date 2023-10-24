using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    public float HP = 2000;

    public GameObject effect; //�� ��ü�� �ı� �ɶ����� ȣ��� ����Ʈ
    public GameObject lastBoss; //�� ��ü�� ������ �ı��ɶ� ȣ��� ������Ʈ
    public GameObject self_destruct;
    public GameObject lazer;

    public GameObject HitedEffect;


    int speed = 2; //�� ������Ʈ�� ���� �ӵ� ����
    Vector3 pos; //������ġ

    float delta = 1.0f; // ��(��)�� �̵������� (x)�ִ밪

    public float timer = 0;


    //�¾Ҵ����� ���� üũ �ϴ� ����

    string bossMode = "mode1"; //������ 1������ mode�� ��Ʈ������ �����Ͽ���.  






    public GameObject mb;
    public GameObject mb2;
    public Transform tr;
    public Transform tr2;
    public Transform lazerZone;
    //���� ��� ���Ͽ� ���� ��ġ�� �ű⿡ ���� ź ������Ʈ


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;
    //���� ���Ͽ� ���� ������Ʈ�� ����
    GameObject BossP4;

    

    
    public GameObject[] SummonsMon;
   
    public int breackStack = 0;

    public int summonStack = 0;

    public int destruckCount = 0;

    public int StartPaze2Conut = 0;

    public bool CoolAtk = false;

    public bool isHit = false;



    private void Awake()
    {
       
        //transform.Find("BPpos3") .gameObject.SetActive(false);
        BossP4 = transform.GetChild(7).gameObject;
        //BossP4.gameObject.SetActive(false);
        BossP3.gameObject.SetActive(false);

    }

    void Start()
    {
        pos = transform.position;
        //���� ��Ÿ���� Hide�Լ� 1�ʵ� ����
        //Invoke("Hide", 1);

        BossUI_dm.instance.StartSet_ver2();

    }



    void Update()
    {
        timer += Time.deltaTime;
        if (HP <= 4000 && HP > 3000)
        {

            if (summonStack == 0)
            {

                Summons();

            }

            if(timer % 10 == 0)
            {
                summonStack = 0;
                timer = 0;
            }
        }
        else if (HP <= 3000 && HP > 2000)
        {
            //������ �ٸ� ���� ������.
            //�ѹ��� �ߵ��ǰ� �ϴ°� ������.
            if (destruckCount == 0) 
            {
                Instantiate(self_destruct, transform.position, Quaternion.identity);
                destruckCount++;
            }
            if(timer % 10 == 0)
            {
                destruckCount = 0;
                timer = 0;
            }
            
        }
        else if(HP > 1000 && HP <= 2000)
        {

            BossP4.gameObject.SetActive(false);
            BossP3.gameObject.SetActive(true);
            if (StartPaze2Conut == 0)
            {
                Start2Paze();
                Instantiate(lazer, lazerZone.transform.position, Quaternion.identity);
                StartPaze2Conut++;
                timer = 0;
                
              
            }


            if (timer % 10 > 9.97f)
            {
                Instantiate(lazer, lazerZone.transform.position, Quaternion.identity);
            }

        }
        else if(HP>0 && HP<=1000)
        {
            if (CoolAtk == false)
            {
                StartCoroutine(Think());
                StartCoroutine(CoolTime());
            }

        }


    }


    void LateUpdate()
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

        BossUI_dm.instance.Damage(BossUI_dm.HP.body, HP);

       // Debug.Log("������ �޾���");
        StartCoroutine(CoolHit());

        if (HP <= 0)
        {
            HP = 0;

            ScoreManager.instance.Bonus++;
            ScoreManager.instance.monsterkill++;


            Instantiate(lastBoss, transform.position, Quaternion.identity);
            BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.body);
            BossUI_dm.instance.CorStartSliderSet(BossUI_dm.HP.octopus);

            Instantiate(effect, transform.position, Quaternion.identity);

            Destroy(gameObject);
            //    Destroy(effect, 0.5f);

        }
    }



    private void Start2Paze()
    {
        StartCoroutine(BossMissle()); //�ڷ�ƾ ���� �Լ� ����
        StartCoroutine(CircleFire()); //�ڷ�ƾ ���� �Լ� ����
    }

   /* void Hide()
    {
        //���� �ؽ�Ʈ ��ü�̸� �˻��ؼ� ����
        GameObject.Find("BossWarning").SetActive(false);
        //GameObject.Find("TextBossWarning").SetActive(false);
    }*/

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
        yield return new WaitForSeconds(0.5f);

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






    }

    IEnumerator Bosspattern1()
    {

        BossP1.GetComponent<BossPattern1>().Shot();
        yield return new WaitForSeconds(1f);
        // StartCoroutine(Think());
    }

    IEnumerator Bosspattern2()
    {

        BossP2.GetComponent<BossPattern2>().Shot();
        yield return new WaitForSeconds(1f);
        //StartCoroutine(Think());
    }



    IEnumerator CoolTime()
    {
        CoolAtk = true;
        yield return new WaitForSeconds(0.5f);
        CoolAtk = false;
    }


    void Summons()
    {
        //Debug.Log("��ȯ�Ǿ���");
        for (int i = 0; i < 4; i++)
        {
            Instantiate(SummonsMon[i], transform.position, Quaternion.identity);
        }
        summonStack++;
       
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
