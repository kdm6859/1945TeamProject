using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShapeShot : MonoBehaviour
{
    //�ʱ� �߽� : ȸ�� �Ǵ� ����
    [Range(0, 360), Tooltip("������ �� ȸ���� �� �� ����")]
    public float Rotation;

    [Range(3, 7), Tooltip("������ ����� ������� ������ ���ϴ� ��")] //->��~ĥ������ �׳��� �̻� �� �̻����� ���� ������ ����..
    public int Vertex = 3;

    [Range(1, 5), Tooltip("�� ���� �����Ͽ� �ձ� ����, ������ �������� ǥ�� ��")]
    public float Subdivision = 3;

    //���ǵ�
    public float Speed = 3; //speed

    //��Ÿ �����͵�
    private int _m;
    private float _a;
    private float _phi;
    private readonly List<float> _v = new List<float>();
    private readonly List<float> _xx = new List<float>();

    public GameObject Bullet;

    private void Awake()
    {
        //��� �����͸� �ʱ�ȭ �Ѵ�.
        ShapeInit();
        StartCoroutine(ShotReady());
    }

    private void Update()
    {

       
    }

    [ContextMenu("��� ���� �ʱ�ȭ")]
    //�ش� ���� ������ �̱� ���ؼ� initó���� �ؾ��Ѵ�.
    private void ShapeInit()
    {
        //��ҵ��� ��� ���� �� ������ �ʱ�ȭ �ϱ����� Clear�Ѵ�.
        _v.Clear();
        _xx.Clear();

        //������ �ʱ�ȭ
        _m = (int)Mathf.Floor(Subdivision / 2);
        _a = 2 * Mathf.Sin(Mathf.PI / Vertex);
        _phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;
        _v.Add(0);
        _xx.Add(0);

        for (int i = 1; i <= _m; i++)
        {
            //list.Insert(��ġ,���) -> �ش� ��ġ�� ���� ����ֽ��ϴ�.
            _v.Add(Mathf.Sqrt(Subdivision * Subdivision - 2 * _a * Mathf.Cos(_phi) * i * Subdivision + _a * _a * i * i));
        }

        for (int i = 1; i <= _m; i++)
        {
            _xx.Add(Mathf.Rad2Deg * (Mathf.Asin(_a * Mathf.Sin(_phi) * i / _v[i])));
        }
    }

    private void Shot()
    {
        //rot���� ������ ���� �ʵ��� ������ dir���� �����Ͽ���.
        float direction = Rotation;

        //������ �� ��ŭ ����
        for (int r = 0; r < Vertex; r++)
        {
            for (int i = 1; i <= _m; i++)
            {
                #region //1�� ����
                GameObject mon = GameObject.FindWithTag("Boss");
                //�Ѿ� ����
                GameObject idx1 = Instantiate(Bullet);

                //2���� ����
                Destroy(idx1, 8f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx1.transform.position = mon.transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                idx1.transform.rotation = Quaternion.Euler(0, 0, direction + _xx[i]);

                //������ �ӵ� ó���� ����� ����� ����.
                idx1.GetComponent<BossTraceBullet>().Speed = _v[i] * Speed / Subdivision;

                #endregion

                #region //2�� ����

                //�Ѿ� ����
                GameObject idx2 = Instantiate(Bullet);

                //2���� ����
                Destroy(idx2, 8f);

              

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx2.transform.position = mon.transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                idx2.transform.rotation = Quaternion.Euler(0, 0, direction - _xx[i]);

                //������ �ӵ� ó���� ����� ����� ����.
                idx2.GetComponent<BossTraceBullet>().Speed = _v[i] * Speed / Subdivision;

                #endregion

                #region //3�� ����

                //�Ѿ� ����
                GameObject idx3 = Instantiate(Bullet);

                //2���� ����
                Destroy(idx3, 8f);


                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx3.transform.position = mon.transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                idx3.transform.rotation = Quaternion.Euler(0, 0, direction);

                //������ �ӵ� ó���� ����� ����� ����.
                idx3.GetComponent<BossTraceBullet>().Speed = Speed;

                #endregion
                Vertex += 1;
                if(Vertex ==7)
                {
                    Vertex = 3;
                }


                //����� �ϼ��Ѵ�.
                direction += 360 / Vertex;
            }
        }
    }

    IEnumerator ShotReady()
    {
        yield return new WaitForSeconds(0.2f);
        Rotation += 17;
        Shot();
      //  Debug.Log("�Լ��� �۵�����");
        StartCoroutine(ShotReady());
    }    

}

