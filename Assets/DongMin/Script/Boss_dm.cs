using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class Boss_dm : MonoBehaviour
{
    public int hp = 1000;
    int maxHp;
    float speed = 0.35f;
    float Delay = 1.5f;
    public Transform ms;
    public GameObject bullet;
    float limitX = 0.3f;
    //float limitY = 4.47f;
    float x = 1;
    float y = 0;

    bool isLazer = false;
    public bool isBattle = false;
    bool isBodyActive = false;

    public int destroyArmCount = 0;


    [SerializeField]
    GameObject[] LazerWarningArea;
    [SerializeField]
    GameObject BossLazer;

    [SerializeField]
    GameObject bossHead;

    [SerializeField]
    GameObject octopus;
    [SerializeField]
    int octopusHp = 1000;
    int maxOctopusHp;
    [SerializeField]
    SpriteRenderer[] octopusSprite;
    [SerializeField]
    GameObject shadow1;
    [SerializeField]
    GameObject shadow2;
    [SerializeField]
    Image bossUI;
    [SerializeField]
    GameObject exprosion;

    [SerializeField]
    SpriteRenderer electricHead;
    [SerializeField]
    GameObject electricBall;
    [SerializeField]
    GameObject leftLeg;
    [SerializeField]
    GameObject rightLeg;

    [SerializeField]
    GameObject electricMiniBall;


    Coroutine corBossBullet;
    Coroutine AttackCoroutine;
    Coroutine corBossPattern;
    Coroutine corOctopusPattern;
    Coroutine corOctoSkill;
    Coroutine corElectricAttack;

    //[SerializeField]
    //GameObject FireWarningArea;
    //[SerializeField]
    //GameObject BossFire;

    // Start is called before the first frame update
    void Start()
    {
        CameraShake.instance.AudioPlay();

        transform.GetComponent<BoxCollider2D>().enabled = false;

        LazerWarningArea[0].SetActive(false);
        BossLazer.SetActive(false);

        bossUI = GameObject.Find("BossUI").GetComponent<Image>();

        maxHp = hp;
        maxOctopusHp = octopusHp;

        BossUI_dm.instance.StartSet(BossUI_dm.HP.body, maxHp);
        BossUI_dm.instance.StartSet(BossUI_dm.HP.octopus, maxOctopusHp);

        //����� ���� ����
        //corOctopusPattern = StartCoroutine(OctopusPattern());

        StartCoroutine(BossStart());


    }

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
                GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
                //�߻�ü �̵� ����(����)
                float angle = weightAngle + intervalAngle * i;
                //�߻�ü �̵� ���� (����)
                //Cos(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // Mathf.PI / 180.0f == Mathf.Deg2Rad
                //Sin(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                //�߻�ü �̵� ���� ����
                clone.GetComponent<MonsterBullet_dm>().Move(new Vector2(x, y));
            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate); //3�ʸ��� ���� �̻��� �߻�
        }
    }

    IEnumerator ElectricAttack()
    {
        float attackRate = 1;//�����ֱ�
        int count = 30;    //�߻�ü ���� ����
        float intervalAngle = 360 / count;  //�߻�ü ������ ����
        float weightAngle = 0; //���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)
        int maxAttackCount = 5;
        int attackCount = 0;

        //�� ���·� ����ϴ� �߻�ü ���� (count ���� ��ŭ)
        while (attackCount< maxAttackCount)
        {
            for (int i = 0; i < count; ++i)
            {
                //�߻�ü ����
                GameObject clone = Instantiate(electricMiniBall, electricBall.transform.position, Quaternion.identity);
                //�߻�ü �̵� ����(����)
                float angle = weightAngle + intervalAngle * i;
                //�߻�ü �̵� ���� (����)
                //Cos(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // Mathf.PI / 180.0f == Mathf.Deg2Rad
                //Sin(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                //�߻�ü �̵� ���� ����
                clone.GetComponent<MonsterBullet_dm>().Move(new Vector2(x, y));
            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            attackCount++;

            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate); //3�ʸ��� ���� �̻��� �߻�
        }

        leftLeg.SetActive(false);
        rightLeg.SetActive(false);
        electricBall.SetActive(false);
        leftLeg.SetActive(true);
        rightLeg.SetActive(true);
        CameraShake.instance.ShakeSwitchOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattle)
        {
            transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);

            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, -limitX, limitX),
                transform.position.y);

            if (transform.position.x >= limitX || transform.position.x <= -limitX)
            {
                x = -x;
            }
        }

        if (destroyArmCount >= 2 && !isBodyActive)
        {
            isBodyActive = true;

            BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.leftArm);
            BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.rightArm);

            BossUI_dm.instance.CorStartSliderSet(BossUI_dm.HP.body);

            transform.GetComponent<BoxCollider2D>().enabled = true;

            corBossPattern = StartCoroutine(BossPattern());
        }
    }

    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}

    IEnumerator BossStart()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * 11f, ForceMode2D.Impulse);

        float curTime = 0;

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime >= 0.1f)
            {
                curTime = 0;
                rb.velocity = rb.velocity * 0.8f;

                //if (rb.velocity.y >= -0.01f)
                if (transform.position.y <= 2.63)
                {
                    rb.velocity = Vector2.zero;
                    break;
                }
            }

            yield return null;
        }

        //���� ��Ÿ���� Hide�Լ� 1�� �� ����
        Invoke("Hide", 1);
        corBossBullet = StartCoroutine("BossBullet");

    }

    public void Damage(int attack)
    {
        hp -= attack;

        BossUI_dm.instance.Damage(BossUI_dm.HP.body, hp);

        if (hp <= 0)
        {
            //Destroy(gameObject);
            isBattle = false;
            transform.GetComponent<BoxCollider2D>().enabled = false;

            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
            }
            if (corBossPattern != null)
            {
                StopCoroutine(corBossPattern);
            }
            if (corBossBullet != null)
            {
                StopCoroutine(corBossBullet);
            }

            bossHead.SetActive(false);
            for (int i = 0; i < LazerWarningArea.Length; i++)
            {
                LazerWarningArea[i].SetActive(false);
            }
            BossLazer.SetActive(false);

            exprosion.SetActive(true);

            //CameraShake.instance.ShakeSwitchOff();

            //����� ���� ����
            corOctopusPattern = StartCoroutine(OctopusPattern());
        }
    }

    public void OctopusDamage(int attack)
    {
        octopusHp -= attack;

        BossUI_dm.instance.Damage(BossUI_dm.HP.octopus, octopusHp);

        if (octopusHp <= 0)
        {
            isBattle = false;
            octopus.GetComponent<CapsuleCollider2D>().enabled = false;

            exprosion.SetActive(true);

            StartCoroutine(OctopusDead());
        }
    }

    IEnumerator OctopusDead()
    {
        //float curTime = 0;
        CameraShake.instance.ShakeSwitchOff();

        StopCoroutine(corOctoSkill);
        StopCoroutine(corElectricAttack);


        yield return new WaitForSeconds(2f);
        exprosion.SetActive(false);

        while (true)
        {
            //���� ���� �������� �����
            transform.localScale = new Vector3(transform.localScale.x - (0.4f * Time.deltaTime), transform.localScale.y - (0.4f * Time.deltaTime), 1);

            if (transform.localScale.x <= 0)
            {
                transform.localScale = new Vector3(0, 0, 1);
                
                break;
            }

            yield return null;
        }
        CameraShake.instance.ShakeSwitchOff();

        ScoreManager.instance.Bonus++;
        BossUI_dm.instance.StageClear();

        gameObject.SetActive(false);
    }

    void Hide()
    {
        //���� �ؽ�Ʈ ��ü�̸� �˻��ؼ� ����
        if (GameObject.Find("BossWarning") != null)
        {
            GameObject.Find("BossWarning").SetActive(false);
        }

        isBattle = true;
    }

    IEnumerator BossBullet()
    {
        float weightAngle = 1;

        while (true)
        {
            ms.transform.Rotate(0, 0, weightAngle);

            for (int i = 0; i < 18; i++)
            {
                ms.transform.Rotate(0, 0, 20);
                Instantiate(bullet, ms.position, ms.transform.rotation);
            }

            //Instantiate(bullet, ms.position, Quaternion.identity);
            //Instantiate(bullet, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    public void corBossBulletStart()
    {
        corBossBullet = StartCoroutine("BossBullet");
    }

    public IEnumerator AttackWarning(GameObject[] attackArea, GameObject BossAttack)
    {
        SpriteRenderer[] attackAreaSprite = new SpriteRenderer[attackArea.Length];

        for (int i = 0; i < attackArea.Length; i++)
        {
            attackArea[i].SetActive(true);
            attackAreaSprite[i] = attackArea[i].GetComponent<SpriteRenderer>();
        }


        float curTime = 0;
        //int count = 10;
        //float wfSeconds = 1 / (float)count;
        //int curCount = 0;
        float maxColorA = 80 / 255.0f;
        float curColorA = 0;
        int blinkCount = 3;

        for (int i = 0; i < blinkCount; i++)
        {
            while (curTime < 1f)
            {
                curTime += Time.deltaTime;
                curColorA = maxColorA * curTime;
                for (int j = 0; j < attackAreaSprite.Length; j++)
                {
                    attackAreaSprite[j].color = new Color(attackAreaSprite[j].color.r,
                        attackAreaSprite[j].color.g, attackAreaSprite[j].color.b, curColorA);
                }

                yield return null;
            }
            curTime = 0;

            while (curTime < 1f)
            {
                curTime += Time.deltaTime;
                curColorA = maxColorA * (1.0f - curTime);
                for (int j = 0; j < attackAreaSprite.Length; j++)
                {
                    attackAreaSprite[j].color = new Color(attackAreaSprite[j].color.r,
                        attackAreaSprite[j].color.g, attackAreaSprite[j].color.b, curColorA);
                }

                yield return null;
            }
            curTime = 0;
        }

        //Debug.Log(Time.time);
        for (int i = 0; i < attackArea.Length; i++)
        {
            attackArea[i].SetActive(false);
        }

        StopCoroutine(corBossBullet);

        BossAttackOn(BossAttack);
    }


    void BossAttackOn(GameObject BossAttack)
    {
        BossAttack.SetActive(true);
    }

    IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            AttackCoroutine = StartCoroutine(AttackWarning(LazerWarningArea, BossLazer));

            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator OctopusPattern()
    {
        float shadow1Speed = 18f;
        float shadow2Speed = 13f;
        float curTime = 0;
        float color = 0;
        float downTime = 5f;
        float octStartScale = 8.5602f;
        bool warningTextOn = false;

        //CameraShake.instance.ShakeSwitchOff();

        yield return new WaitForSeconds(2f);

        exprosion.SetActive(false);

        yield return new WaitForSeconds(1.3f);

        while (true)
        {
            curTime += Time.deltaTime;

            shadow1.transform.Translate(Vector2.down * shadow1Speed * Time.deltaTime);

            if (curTime >= 2.5f)
            {
                break;
            }

            yield return null;
        }

        curTime = 0;
        while (true)
        {
            curTime += Time.deltaTime;

            shadow2.transform.Translate(Vector2.up * shadow2Speed * Time.deltaTime);

            if (curTime >= 2.5f)
            {
                break;
            }

            yield return null;
        }

        shadow1.SetActive(false);
        shadow2.SetActive(false);

        bossUI.color = Color.black;
        octopus.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        curTime = 0;
        while (true)
        {
            curTime += Time.deltaTime;
            color = curTime / downTime;

            for (int i = 0; i < octopusSprite.Length; i++)
            {   //���� ���������� ���� �����
                octopusSprite[i].color = new Color(color, color, color);
            }

            //UI�г� ���̵� �ƿ�
            bossUI.color = new Color(0, 0, 0, 1 - color);

            //���� ũ�� ���
            octopus.transform.localScale = new Vector3(octStartScale / (color * 10), octStartScale / (color * 10), 1);

            if (curTime >= downTime * 0.7f && !warningTextOn)
            {
                warningTextOn = true;
                BossUI_dm.instance.OctopusWarning();
                BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.body);

                BossUI_dm.instance.CorStartSliderSet(BossUI_dm.HP.octopus);
            }
            else if (curTime >= downTime)
            {
                octopus.GetComponent<CapsuleCollider2D>().enabled = true;

                yield return new WaitForSeconds(1f);

                isBattle = true;

                break;
            }


            yield return null;
        }

        yield return new WaitForSeconds(7f);
        while (true)
        {
            corOctoSkill = StartCoroutine(OctoSkill());

            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator OctoSkill()
    {
        float curTime = 0;
        float colorA = 0;
        float chargeTime = 3f;

        while (true)
        {
            curTime += Time.deltaTime;

            colorA = curTime / chargeTime;

            electricHead.color = new Color(electricHead.color.r, electricHead.color.g, electricHead.color.b, colorA);

            if (curTime >= chargeTime)
            {
                leftLeg.GetComponent<LegMove2_dm>().isSkill = true;
                rightLeg.GetComponent<LegMove2_dm>().isSkill = true;
                electricBall.SetActive(true);
                CameraShake.instance.ShakeSwitch();

                corElectricAttack = StartCoroutine(ElectricAttack());

                break;
            }

            yield return null;
        }
        yield return new WaitForSeconds(5f);
        
        electricHead.color = new Color(electricHead.color.r, electricHead.color.g, electricHead.color.b, 0);
    }

    private void OnDisable()
    {
        if (corBossBullet != null)
        {
            StopCoroutine(corBossBullet);
        }
        if (AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
        }
        if (corBossPattern != null)
        {
            StopCoroutine(corBossPattern);
        }

    }
}
