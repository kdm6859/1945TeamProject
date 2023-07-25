using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4.0f;
    [SerializeField]
    GameObject exprosionPrefab;
    [SerializeField]
    GameObject itemPrefab;
    int attack = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            GameObject exprosion = Instantiate(exprosionPrefab, transform.position, Quaternion.identity);
            Destroy(exprosion, 0.4f);
            //������ ����
            //Instantiate(itemPrefab, collision.transform.position, Quaternion.identity);
            //collision.GetComponent<Monster>().ItemDrop();

            //���� �浹 �����
            //Destroy(collision.gameObject);
            collision.GetComponent<Monster>().Damage(attack);
            //�̻��� �����
            Destroy(gameObject);

        }
    }
}
