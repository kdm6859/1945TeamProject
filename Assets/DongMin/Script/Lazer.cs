using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField]
    Animator lazerBodyAnim;
    [SerializeField]
    Animator lazerHeadAnim;
    [SerializeField]
    GameObject exprosionPrefab;
    public Transform playerFireTransform = null;
    public bool isLazer = true;
    int attack = 10;
    float curTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerFireTransform.position;

        if (!isLazer)
        {
            lazerBodyAnim.SetTrigger("End");
            lazerHeadAnim.SetTrigger("End");
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {

            curTime += Time.deltaTime;

            if (curTime >= 0.1f)
            {
                //Debug.Log("ƽ�ߵ���");
                curTime = 0;
                if (collision.GetComponent<Monster>() != null)
                    collision.GetComponent<Monster>().Damage(attack);
                else if(collision.GetComponent<Boss>() != null)
                    collision.GetComponent<Boss>().Damage(attack);
                else if (collision.GetComponent<BossArm>() != null)
                    collision.GetComponent<BossArm>().Damage(attack);
                
                    
            }

        }
    }
}
