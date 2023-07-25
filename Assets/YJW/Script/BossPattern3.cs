using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern3 : MonoBehaviour
{
    //ȸ���Ǵ� ���ǵ��̴�.
    public float TurnSpeed;

    //�߻�� �Ѿ� ������Ʈ�̴�.
    public GameObject Bullet;

    public float SpawnInterval = 0.5f;
    private float _spawnTimer;

    private void Update()
    {
        Shot();
    }

    public void Shot()
    {
        //�⺻ ȸ��
        transform.Rotate(Vector3.forward * (TurnSpeed * 100 * Time.deltaTime));

        //���� ���� ó��
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer < SpawnInterval) return;

        //�ʱ�ȭ
        _spawnTimer = 0f;

        //�Ѿ� ����
        GameObject temp = Instantiate(Bullet);

        //2���� �ڵ� ����
        Destroy(temp, 2f);

        //�Ѿ� ���� ��ġ�� ���� �Ա��� �Ѵ�.
        temp.transform.position = transform.position;

        //�Ѿ��� ������ ������Ʈ�� �������� �Ѵ�.
        //->�ش� ������Ʈ�� ������Ʈ�� 360�� ȸ���ϰ� �����Ƿ�, Rotation�� ������ ��.
        temp.transform.rotation = transform.rotation;
    }
}