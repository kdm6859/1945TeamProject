using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator ani; //�ִϸ����� ������ ����
    public float moveSpeed = 5;

    public GameObject playerChasePos;


    public GameObject[] bullet; //�̻��� 
    public GameObject lazer;
    public GameObject destroyEffect;

    public Transform pos = null; //�̻��� �߻�
    public Transform boompos = null;
    public Image energybar;
    public Image energyStack;

    // public GameObject Lazer = null; �ӽ÷� ����
    public Transform[] pos2 = null;
    public GameObject helper;


    public GameObject boom = null;

    public bool pBulletCheck = true;

    public int power = 0;

    public int stack = 0;
    //public float stackfill = 0;

    public int BoomStack = 2;





    public int powerStack = 0;

    public float energyValue;

    public float gazyStack;


    private void Awake()
    {
        gazyStack = 0;

    }

    void Start()
    {
        ani = GetComponent<Animator>();
        //GetComponent<RectTransform>().SetAsFirstSibling();
    }


    void Update()
    {
        PlayerControl();



    }

    private void PlayerControl()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");


        if (Input.GetAxis("Horizontal") >= 0.5f)
        {
            ani.SetBool("right", true);
        }
        else
        {
            ani.SetBool("right", false);
        }

        if (Input.GetAxis("Horizontal") <= -0.5f)
        {
            ani.SetBool("left", true);
        }
        else
        {
            ani.SetBool("left", false);
        }


        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            ani.SetBool("up", true);
        }
        else
        {
            ani.SetBool("up", false);
        }



        //���⼭ ó��
        if (Input.GetKeyUp(KeyCode.Space) && (gazyStack >= 100 && gazyStack < 300) && (stack >= 19 && stack < 47))
        {
            gazyStack -= 100;
            energybar.fillAmount = energybar.fillAmount - 0.19f;
            energyStack.fillAmount = 0f;
            stack = 0;
            Instantiate(helper, pos2[0].transform.position, Quaternion.identity);

            // Instantiate(Lazer, pos.transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && (gazyStack >= 300 && gazyStack < 700) && (stack >= 47 && stack < 100))
        {
            gazyStack -= 300;
            energybar.fillAmount = energybar.fillAmount - 0.47f;
            energyStack.fillAmount = 0f;
            stack = 0;
            Instantiate(helper, pos2[1].transform.position, Quaternion.identity);
            Instantiate(helper, pos2[2].transform.position, Quaternion.identity);
        }
        else if ((Input.GetKeyUp(KeyCode.Space) && gazyStack >= 700 && stack >= 100))
        {
            gazyStack = 0;
            energybar.fillAmount = 0f;
            energyStack.fillAmount = 0f;
            stack = 0;
            Instantiate(helper, pos2[3].transform.position, Quaternion.identity);
            Instantiate(helper, pos2[4].transform.position, Quaternion.identity);
            Instantiate(lazer, pos2[0].transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            stack = 0;
            energyStack.fillAmount = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Z) && BoomStack > 0)
        {
            BoomStack--;
            Instantiate(boom, boompos.transform.position, Quaternion.identity);
        }

        if (Input.GetKey(KeyCode.Space))
        {


            if (pBulletCheck)
            {
                pBulletCheck = false;
                if (power == 0)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);

                }
                else if (power == 1)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);

                }
                else if (power == 2)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);

                }
                else if (power == 3)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);

                }

                StartCoroutine(CoolTimeAtk());
            }


        }

        transform.Translate(moveX, moveY, 0);

        if (transform.position.x >= 2.5f)
        {
            transform.position = new Vector3(2.5f, transform.position.y, 0);

        }

        if (transform.position.x <= -2.5f)
        {
            transform.position = new Vector3(-2.5f, transform.position.y, 0);
        }

        if (transform.position.y >= 4)
        {
            transform.position = new Vector3(transform.position.x, 4.0f, 0);
        }

        if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }
    }

    IEnumerator CoolTimeAtk()
    {
        yield return new WaitForSeconds(0.04f);
        pBulletCheck = true;
        energyStack.fillAmount += 0.02f;
        stack += 2;

    }

    public void GazyPower(float energy)
    {


        gazyStack += energy;


        if (gazyStack <= 100)
        {
            //0.19�� 1�ܰ� Ǯ����
            energybar.fillAmount += 0.0019f;

        }
        if (gazyStack > 100 && gazyStack <= 300)
        {
            //0.48�� 2�ܰ� Ǯ���� // 0.28���� ä�� �� ����.
            energybar.fillAmount += 0.0014f;
            // powerStack = 1;

        }
        else if (gazyStack >= 300 && gazyStack < 700)
        {
            //1�� �ܰ� Ǯ���� // 0.52���� ä�� �� ����.
            energybar.fillAmount += 0.0013f;
            //  powerStack = 2;

        }
        else if (gazyStack == 700)
        {
            energybar.fillAmount = 1f;
            // powerStack = 3;
        }

        energyValue = energybar.fillAmount;
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            power += 1;



            if (power >= 3)
                power = 3;

            if (power == 3)
            {
                BoomStack++; // �Ŀ��� 3�̻��϶�, �� �̻��� ������ ������ �Ŀ���� ��ź�� ����.
            }


            //������ ���� ó��
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }

}
