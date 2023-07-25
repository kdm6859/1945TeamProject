using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp = 1000;
    float speed = 1; 
    float Delay = 1.5f;
    public Transform ms;
    public GameObject bullet;
    float limitX = 1.4f;
    //float limitY = 4.47f;
    float x = 1;
    float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        //���� ��Ÿ���� Hide�Լ� 1�� �� ����
        Invoke("Hide", 1);

        //StartCoroutine("BossBullet");
        StartCoroutine("CircleFire");

        ////�ѹ� ȣ��
        //Invoke("CreateBullet", 0.1f);
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
                clone.GetComponent<MonsterBullet>().Move(new Vector2(x, y));
            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate); //3�ʸ��� ���� �̻��� �߻�
        }
    }

    // Update is called once per frame
    void Update()
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Damage(int attack)
    {
        //hp -= attack;

        //if (hp <= 0)
        //{
        //    ItemDrop();
        //    Destroy(gameObject);
        //}
    }

    void Hide()
    {
        //���� �ؽ�Ʈ ��ü�̸� �˻��ؼ� ����
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator BossBullet()
    {
        while (true)
        {
            for(int i = 0; i < 36; i++)
            {
                ms.transform.Rotate(0, 0, 10);
                Instantiate(bullet, ms.position, ms.transform.rotation);
            }

            //Instantiate(bullet, ms.position, Quaternion.identity);
            //Instantiate(bullet, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
