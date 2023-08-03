using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossJ : MonoBehaviour
{
    int flag = 1;
    int speed = 2;
    public int HP = 100;

    public GameObject bress;
    public Transform tr = null;
    //public Transform tr2;
    public float ZigzagDistance = 1.4f;
    public float Speedx = 30;
    public float Speedy = 10;
    public float Delay = 3;


    public TextMeshProUGUI GameClear;






    void Start()
    {
        StartCoroutine(BossBressJ());

        GameClear = GameObject.Find("GameClear").GetComponent<TextMeshProUGUI>();

        // �ʱ⿡�� GameClear ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        GameClear.enabled = false;

        //���� ��Ÿ���� Hide�Լ� 1�ʵ� ����
        //Invoke("Hide", 1);
        StartCoroutine(BossBressJ());


    }
    void Update()
    {
        
        if (transform.position.x >= ZigzagDistance)
            flag *= -1;
        if (transform.position.x <= -ZigzagDistance)
            flag *= -1;

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
        
        //Invoke("BossBressJ", 3);
    }

    void Hide()
    {
        //���� �ؽ�Ʈ ��ü�̸� �˻��ؼ� ����
        //GameObject.Find("TextBossWarning").SetActive(false);
    }

    //IEnumerator BossBress()
    IEnumerator BossBressJ()
    {

        bool isActive = false;
        while (true)
        {

            // 3�� ���
            yield return new WaitForSeconds(2f);


            bress.SetActive(isActive);
            isActive = !isActive;

            // ���� ���� üũ
            //bool isActive = GameObject.Find("BossBressJ").activeSelf;

            // ���� ����
            //GameObject.Find("BossBressJ").SetActive(!isActive);


        }
    }
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            //ItemDrop();
            //      GameClear.gameObject.SetActive(true);

            
            GameClear.enabled = true;
            Destroy(gameObject);
            

        }
    }

    void OnDestroy()
    {
        ScoreManagerJw.instance.UpdateScore();
    }


}
