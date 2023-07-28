using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConstrolJ : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public int HP = 1;
    private Animator animator;
    public GameObject[] bullet; //PBulletJ
    public Transform bulletSpawnPoint; // �Ѿ��� ������ ��ġ (spawn ���� ������Ʈ)
    public int power = 0;
    public float minX = -2.4f;
    public float maxX = 2.4f;
    public float minY = -4.7f;
    public float maxY = 4.5f;
    public float gValue = 0;
    public Transform pos = null;
    public GameObject OneJ;
    public Image Gage;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");

        float deltaX = MoveX * moveSpeed * Time.deltaTime;
        float deltaY = MoveY * moveSpeed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        transform.position = new Vector3(newX, newY, 0f);

        Vector3 movement = new Vector3(MoveX, MoveY, 0f) * moveSpeed;
        transform.Translate(movement * Time.deltaTime);


        // �ִϸ����� ���� Ʈ���� �Ķ���Ϳ� ���� �����Ͽ� �ִϸ��̼� ����
        animator.SetFloat("MoveX", MoveX);
        animator.SetFloat("MoveY", MoveY);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet[power], bulletSpawnPoint.position, Quaternion.identity);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            gValue += 0.01f;

            Gage.fillAmount = gValue;

            if (gValue >= 1)
            {

                //������ ������
                GameObject go = Instantiate(OneJ, pos.position, Quaternion.identity);

                Destroy(go, 3);
                gValue = 0;

            }

        }
        else
        {
            gValue -= 0.005f;

            if (gValue <= 0)
                gValue = 0;

            Gage.fillAmount = gValue;
        }
    }
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }


}