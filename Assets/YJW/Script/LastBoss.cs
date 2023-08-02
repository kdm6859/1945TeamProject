using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class LastBoss : MonoBehaviour
{
    public float hp = 1500;

    public GameObject playerDied;
    GameObject target;

    //�Ѿ��� ������ Target���� ���ư� ����
  //  public GameObject Target;
    public GameObject bossPos;

    public GameObject helper1;
    public GameObject helper2;

    public GameObject targetbullet;

    public GameObject effect;


    float rightMax = 1.0f; //�·� �̵������� (x)�ִ밪

    float leftMax = -1.0f; //��� �̵������� (x)�ִ밪

    float currentPosition; //���� ��ġ(x) ����

    float direction = 3.0f; //�̵��ӵ�+����

    bool isDownAttack = false;
    bool isfell = false;

    bool isShot = false;

    bool isLastShot = false;

    public bool isHit = false;


    float leftSpeed = 2;
    float downSpeed = 4;

    int SpawnCount = 0;

    //Vector3 pos; //������ġ

    float delta = 1.0f; // ��(��)�� �̵������� (x)�ִ밪


    [SerializeField] public GameObject missile;
    [SerializeField] public GameObject targetPos;

    [SerializeField] public float spd;
    [SerializeField] public int shot = 12;



    void Start()
    {
        currentPosition = transform.position.x;
        target = GameObject.FindGameObjectWithTag("Player");
        bossPos = GameObject.Find("BossRollBackPos");
        //pos = transform.position;
       // BossHelperSpawn();

       
    }

   
    void Update()
    {

        Debug.Log(hp);

        BossMoving();

        if (hp <= 1000 && hp > 600)
        {
            if (isShot == false)
            {
                StartCoroutine(CreateMissile());
            }
        }
        else if (hp <= 600 && hp > 300)
        {
            //������ �÷��̾��� x���� ���� ���밪���� -�ؼ� 0.1 ������ ���̰� ���ٸ�
            if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(target.transform.position.x)) <= 0.1)
            {
                isDownAttack = true;

            }



            if (isDownAttack && !isfell)
            {

                BossStop();

                transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, downSpeed * Time.deltaTime);

                if (transform.position.y < -3)
                {
                    isDownAttack = false;
                    //Debug.Log("y�� ���� �۾�����.");
                    isfell = true;
                }



            }

            if (!isDownAttack && isfell)
            {


                transform.position =
                Vector3.MoveTowards(transform.position, bossPos.transform.position, downSpeed * Time.deltaTime);
                if (transform.position.y == bossPos.transform.position.y)
                {

                    isfell = false;
                    // isDownAttack = true;

                }




            }
            else if (isDownAttack && isfell)
            {
                transform.position =
               Vector3.MoveTowards(transform.position, bossPos.transform.position, downSpeed * Time.deltaTime);
                if (transform.position.y == bossPos.transform.position.y)
                {

                    isfell = false;
                    // isDownAttack = true;

                }

            }
        }
        else if(hp>100 &&hp <= 300)
        {
            transform.position =
            Vector3.MoveTowards(transform.position, bossPos.transform.position, downSpeed * Time.deltaTime);
            if (SpawnCount == 0)
            {
                BossHelperSpawn();
                SpawnCount++;
            }
           
            if(helper1 == null && helper2 == null && SpawnCount == 1 && hp > 100)
            {
                BossHelperSpawn();
            }
        }
        else if(hp <= 100)
        {
            //�߾� ���� ������ ��.

            StartCoroutine(Shot());
            if (hp <=0)
            {
                hp = 0;
              
            }
        }



        Debug.Log("isDdwonAttack :" + isDownAttack);
        Debug.Log("isfell :" + isDownAttack);


    }

    void BossHelperSpawn()
    {
       Instantiate(helper1, transform.position, Quaternion.identity);
       Instantiate(helper2, transform.position, Quaternion.identity);
    }



    private void BossMoving()
    {
       
        currentPosition += Time.deltaTime * direction;

        if (currentPosition >= rightMax)
        {
            direction *= -1;
            currentPosition = rightMax;
            //���� ��ġ(x)�� ��� �̵������� (x)�ִ밪���� ũ�ų� ���ٸ�
            //�̵��ӵ�+���⿡ -1�� ���� ������ ���ְ� ������ġ�� ��� �̵������� (x)�ִ밪���� ����
        }
        else if (currentPosition <= leftMax)
        {
            direction *= -1;
            currentPosition = leftMax;
            //���� ��ġ(x)�� �·� �̵������� (x)�ִ밪���� ũ�ų� ���ٸ�
            //�̵��ӵ�+���⿡ -1�� ���� ������ ���ְ� ������ġ�� �·� �̵������� (x)�ִ밪���� ����
        }
        transform.position = new Vector3(currentPosition, transform.position.y, 0);
    }

    void BossStop()
    {
       // direction = 0;
        currentPosition = target.transform.position.x;
    }

    public void Damage(float attack)
    {
        hp -= attack;

        Debug.Log("������ �޾���");
        StartCoroutine(CoolHit());

        if (hp <= 0)
        {
            hp = 0;

            Destroy(gameObject);


            Instantiate(effect, transform.position, Quaternion.identity);

            //    Destroy(effect, 0.5f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(collision.gameObject);
            Instantiate(playerDied, collision.transform.position, Quaternion.identity);
            Debug.Log("�÷��̾ �¾���");
        }
    }

    IEnumerator CreateMissile()
    {
       
        int _shot = shot;
        while (_shot > 0)
        {   
            _shot--;
            GameObject bullet = Instantiate(missile, transform.position,Quaternion.identity);
            bullet.GetComponent<BeazierBullet>().master = gameObject;
            bullet.GetComponent<BeazierBullet>().enemy = target;
            isShot = true;
            yield return new WaitForSeconds(0.1f);
            isShot = false;
        }
        yield return null;

       
    }


    IEnumerator Shot()
    {
        Debug.Log("1�������� �ߵ��Ǿ���");
        //Target�������� �߻�� ������Ʈ ����
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {

            yield return new WaitForSeconds(0.5f);
            //�Ѿ� ����
            GameObject temp = Instantiate(targetbullet, transform.position, Quaternion.identity);

            //2���� ����
            Destroy(temp, 2f);

            //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
            //temp.transform.position = Vector2.zero;

            //?���Ŀ� Target���� ���ư� ������Ʈ ����
            bullets.Add(temp.transform);

            //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        yield return new WaitForSeconds(0.4f);
        //�Ѿ��� Target �������� �̵���Ų��.
        StartCoroutine(BulletToTarget(bullets));
    }

    private IEnumerator BulletToTarget(IList<Transform> objects)
    {
        //0.5�� �Ŀ� ����
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < objects.Count; i++)
        {

            //���� �Ѿ��� ��ġ���� �÷����� ��ġ�� ���Ͱ��� �y���Ͽ� ������ ����
            Vector3 targetDirection = target.transform.position - objects[i].position;

            //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            //Target �������� �̵�
            objects[i].rotation = Quaternion.Euler(0, 0, angle);


        }

        //������ ����
        objects.Clear();
    }

    IEnumerator CoolHit()
    {
        var hit = transform.GetComponent<SpriteRenderer>();
        isHit = true;
        hit.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hit.color = Color.white;
        isHit = false;
    }



}
