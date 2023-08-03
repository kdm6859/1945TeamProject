using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern1 : MonoBehaviour
{
    //�Ѿ��� ������ Target���� ���ư� ����
    public GameObject Target;

    //�߻�� �Ѿ� ������Ʈ
    public GameObject Bullet;

  

    private void Start()
    {
       

     

    }

    private void Update()
    {
        Target = GameObject.FindWithTag("Player");
    }

    public void Shot()
    {
        Debug.Log("1�������� �ߵ��Ǿ���");
        //Target�������� �߻�� ������Ʈ ����
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            //�Ѿ� ����
            GameObject temp = Instantiate(Bullet, transform.position, Quaternion.identity);

            //2���� ����
            Destroy(temp, 2f);

            //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
           //temp.transform.position = Vector2.zero;

            //?���Ŀ� Target���� ���ư� ������Ʈ ����
            bullets.Add(temp.transform);

            //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

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
            Vector3 targetDirection = Target.transform.position - objects[i].position;

            //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            //Target �������� �̵�
            objects[i].rotation = Quaternion.Euler(0, 0, angle);


        }

        //������ ����
        objects.Clear();
    }
}

