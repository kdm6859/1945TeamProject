using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    //public float time = 0; // ��� �ð�
    public float monsterkill = 0; // ���� ���� ����
    public float timescore = 0; //�ð��� ���� ����
    public float monsterscore = 0; // ���� �ı� ����
    public float Bonus = 0; //���ʽ� ����, ���� �������, ����ų
    public float totalscore = 0; //���� ����


    public GameObject Time; // ��� �ð�
    public GameObject Kill; // ���� ���� ����
    public GameObject TimeScore; //�ð��� ���� ����
    public GameObject KillScore; // ���� �ı� ����
    public GameObject BonusScore; //���ʽ� ����, ���� �������, ����ų
    public GameObject TotalScore; //���� ����


    void Start()
    {
        if (instance == null)
            instance = this;

        //time = GameManagerSJ.Instance.GameTime;
    }


    public void UpdateScore()
    {
        timescore = 1000- GameManagerSJ.Instance.GameTime; //�ð� �������� �߰� ����
        monsterscore = monsterkill * 10;
        Bonus = Bonus + GameManagerSJ.Instance.player.Heart; //������Ƽ� �ö� ���ʽ������� �÷��̾� ���� ��� ���ϱ�
        totalscore = (timescore + monsterscore) * Bonus; //�ð�����, �������� ���ϰ� ���ʽ� ���ϱ�


        Kill.GetComponent<Text>().text = monsterkill.ToString();
        Time.GetComponent<Text>().text = GameManagerSJ.Instance.GameTime.ToString();
        TimeScore.GetComponent<Text>().text = timescore.ToString();
        KillScore.GetComponent<Text>().text = monsterscore.ToString();
        BonusScore.GetComponent<Text>().text = Bonus.ToString();
        TotalScore.GetComponent<Text>().text = totalscore.ToString();
    }
    void Update()
    {
        Debug.Log(timescore);
        
    }
}
