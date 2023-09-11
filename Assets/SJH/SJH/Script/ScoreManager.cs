using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEditor;


public class ScoreManager : MonoBehaviour
{
    enum SceneName
    {
        stage1,
        stage2,
        stage3
    };

    public static ScoreManager instance;

    [SerializeField]
    SceneName sceneName;


    //public float time = 0; // ��� �ð�
    public float monsterkill = 0;      // ���� ���� ����
    public float timescore = 0 ;        //�ð��� ���� ����
    public float monsterscore=0 ;    // ���� �ı� ����
    public float Bonus =0;           //���ʽ� ����, ���� �������, ����ų
    public float totalscore = 0 ;      //���� ����


    public GameObject Time; // ��� �ð�
    public GameObject Kill; // ���� ���� ����
    public GameObject TimeScore; //�ð��� ���� ����
    public GameObject KillScore; // ���� �ı� ����
    public GameObject BonusScore; //���ʽ� ����, ���� �������, ����ų
    public GameObject TotalScore; //���� ����
    
    public GameObject stageclear;
    public GameObject killtext;
    public GameObject timetext;
    public GameObject bonustext;
    public GameObject totaltext;

    void Start()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
      
    }
    public void UpdateScore()
    {

        StartCoroutine(Result());
        Kill.GetComponent<TextMeshProUGUI>().text = monsterkill.ToString();
        Time.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(GameManagerSJ.Instance.GameTime).ToString();
    }

    IEnumerator Result()
    {
        stageclear.SetActive(true);
        yield return new WaitForSeconds(3);
        killtext.SetActive(true);
        StartCoroutine(MonsterCount(monsterkill * 10, monsterscore));
        yield return new WaitForSeconds(3);
        timetext.SetActive(true);
        StartCoroutine(TimeCount(1000 - Mathf.FloorToInt(GameManagerSJ.Instance.GameTime), timescore));
        yield return new WaitForSeconds(3);
        bonustext.SetActive(true);
        StartCoroutine(BonusCount(Bonus + GameManagerSJ.Instance.player.Heart, Bonus));
        yield return new WaitForSeconds(3);
        totaltext.SetActive(true);

        StartCoroutine(TotalCount((timescore + monsterscore) * Bonus, totalscore));
    }

    IEnumerator MonsterCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            KillScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        monsterscore = target;
        KillScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator TimeCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            TimeScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        timescore = target;
        TimeScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator BonusCount(float target, float current)
    {
        float duration = 1f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            BonusScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        Bonus = target;
        BonusScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator TotalCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            TotalScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }


        if (sceneName == SceneName.stage1)
        {
            totalscore = target;
            PlayerPrefs.SetFloat("Totalscore1", totalscore);
        }
        else if (sceneName == SceneName.stage2)
        {
            totalscore = target;
            PlayerPrefs.SetFloat("Totalscore2", totalscore);
        }
        else if (sceneName == SceneName.stage3)
        {
            totalscore = target;
            PlayerPrefs.SetFloat("Totalscore3", totalscore);
        }


        TotalScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
}
