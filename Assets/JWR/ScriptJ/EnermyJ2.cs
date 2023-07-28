using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyJ2 : MonoBehaviour
{
    public int HP = 30;
    public float Speed = 3;
    public float Delay = 1f;
    public Transform mis1;
    public GameObject bullet;
    //������ ��������
    //public GameObject Item = null;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateBullet", Delay);
    }

    void CreateBullet()
    {
        Instantiate(bullet, mis1.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        //�Ʒ��������� ��������
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }



    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    public void ItemDrop()
    {
        //������ ����
        //Instantiate(Item, transform.position, Quaternion.identity);
    }


    //�̻��Ͽ� ���� ������ �Դ� �Լ�
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            //ItemDrop();
            Destroy(gameObject);
        }
    }
}
