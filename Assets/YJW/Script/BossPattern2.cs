using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern2 : MonoBehaviour
{

    //�߻�� �Ѿ� ������Ʈ
    public GameObject Bullet;

    private void Update()
    {
      
    }

    public void Shot()
    {
        Debug.Log("2�� ������ �ߵ� �Ǿ���.");

        //360�� �ݺ�
        for (int i = 0; i < 360; i += 13)
        {
            //�Ѿ� ����
            GameObject temp = Instantiate(Bullet, gameObject.transform.parent.gameObject.transform.position, Quaternion.identity);

            //2�ʸ��� ����
            Destroy(temp, 2f);

            //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
           // temp.transform.position = Vector2.zero;

            //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }



}
