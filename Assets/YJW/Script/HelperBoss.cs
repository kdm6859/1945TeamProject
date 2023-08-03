using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperBoss : MonoBehaviour
{
    public float HP = 150;
    public float Speed = 3;
    public float Delay = 2f;

    public GameObject bullet;
    public GameObject bullet2;
    GameObject ms;
   
    public GameObject effect;
    public GameObject Item = null;

    public float SpawnInterval;

    bool isArrive = false;

    bool isCool = false;

    public bool isHit = false;

    void Start()
    {
        ms = GameObject.Find("helperInspos1");
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ms.transform.position, Speed * Time.deltaTime);
        StartCoroutine(IsArrive());
        if (isArrive == true)
        {
            transform.Rotate(Vector3.forward * (Speed * 100 * Time.deltaTime));
          
            if(isCool == false) 
            {
                StartCoroutine(IsCoolTime());
                Shot();
            }
           
          
        }
    }

    public void Shot()
    {

        //360�� �ݺ�
        for (int i = 0; i < 360; i += 45)
        {
            //�Ѿ� ����
            GameObject temp = Instantiate(bullet, gameObject.transform.position, ms.transform.rotation);
            GameObject temp2 = Instantiate(bullet2, gameObject.transform.position, ms.transform.rotation);

            //2�ʸ��� ����
            Destroy(temp, 2f);
            Destroy(temp2, 2f);

            //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
            // temp.transform.position = Vector2.zero;

            //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
            temp.transform.rotation = transform.rotation;

            temp2.transform.rotation = Quaternion.Euler(0, 0, i);
            temp2.transform.rotation = transform.rotation;


        }
    }

    public void Damage(float attack)
    {
        HP -= attack;

        Debug.Log("������ �޾���");
        StartCoroutine(CoolHit());

        if (HP <= 0)
        {
            HP = 0;

            ScoreManager.instance.monsterkill++;
            Destroy(gameObject);

            Instantiate(Item, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            

            //    Destroy(effect, 0.5f);

        }
    }

    IEnumerator IsCoolTime()
    {
        isCool = true;
        yield return new WaitForSeconds(SpawnInterval);
        isCool = false;

    }

    IEnumerator IsArrive()
    {
        yield return new WaitForSeconds(1.5f);
        isArrive = true;
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
