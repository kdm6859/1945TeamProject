using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : MonoBehaviour
{
    //�ʱ� �߽� : ȸ�� �Ǵ� ����
    [Range(0, 360), Tooltip("������ �� ȸ���� �� �� ����")]
    public float Rotation;

    [Range(3, 7), Tooltip("������ ����� ������� ������ ���ϴ� ��")] //->��~ĥ������ �׳��� �̻� �� �̻����� ���� ������ ����..
    public int Vertex = 3;

    [Range(1, 5), Tooltip("�� ���� �����Ͽ� �ձ� ����, ������ �������� ǥ�� ��")]
    public float Subdivision = 3;
    public GameObject effect;

    //���ǵ�
    public float Speed = 3; //speed

    public float timer;

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
    }

    private void Start()
    {
      
    }

    private void Update()
    {
      
       
       if(timer >=2)
        {
            Destroy(gameObject);
        }
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

    private void OnDisable()
    {
       // Destroy(gameObject);
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

                //�Ѿ� ����
                GameObject idx1 = Instantiate(Bullet);

                //2���� ����
                Destroy(idx1, 2f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx1.transform.position = Vector2.zero;

                //������ ȸ�� ó���� ����� ����� ����.
                idx1.transform.rotation = Quaternion.Euler(0, 0, direction + _xx[i]);

                //������ �ӵ� ó���� ����� ����� ����.
                idx1.GetComponent<BossTraceBullet>().Speed = _v[i] * Speed / Subdivision;

                #endregion

                #region //2�� ����

                //�Ѿ� ����
                GameObject idx2 = Instantiate(Bullet);

                //2���� ����
                Destroy(idx2, 2f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx2.transform.position = Vector2.zero;

                //������ ȸ�� ó���� ����� ����� ����.
                idx2.transform.rotation = Quaternion.Euler(0, 0, direction - _xx[i]);

                //������ �ӵ� ó���� ����� ����� ����.
                idx2.GetComponent<BossTraceBullet>().Speed = _v[i] * Speed / Subdivision;

                #endregion

                #region //3�� ����

                //�Ѿ� ����
                GameObject idx3 = Instantiate(Bullet);

                //2���� ����
                Destroy(idx3, 2f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                idx3.transform.position = Vector2.zero;

                //������ ȸ�� ó���� ����� ����� ����.
                idx3.transform.rotation = Quaternion.Euler(0, 0, direction);

                //������ �ӵ� ó���� ����� ����� ����.
                idx3.GetComponent<BossTraceBullet>().Speed = Speed;

                #endregion


                Rotation += 13;
                //����� �ϼ��Ѵ�.
                direction += 360 / Vertex;
                timer += Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BossPattern"))
        {
            Debug.Log("�浹�Ͽ���");
            Destroy(collision.gameObject);
            InvokeRepeating("Shot", 0f, 0.5f);

        }
      
    }
}

