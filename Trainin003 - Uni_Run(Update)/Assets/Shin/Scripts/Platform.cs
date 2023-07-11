using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // ���� ������Ʈ�� => �迭�� �����Ѵ�.
    public GameObject[] obstacles;

    // �÷��̾ ���������� ��Ҵ��� ������ bool stepped ����.
    private bool stepped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ������Ʈ�� Ȱ��ȭ �ɶ����� ����Ǵ� �޼��� OnEnable()
    private void OnEnable()
    {
        // ������ �������� ���·� �ʱ�ȭ.
        stepped = false;

        // ������ ������ŭ �ݺ����� ������ => �ش� ������Ʈ������ ������ ������ 3�� ( Left, Mid, Right ).
        for(int i = 0; i<obstacles.Length; i++)
        {
            // �� ������ 1/3 Ȯ���� ��ȯ�ȴ� ( 0, 1, 2 �� 0�� ���� �������ÿ��� ).
            if(Random.Range(0,3) == 0)
            {
                // 0���� ���� ������ ������Ʈ Ȱ��ȭ.
                obstacles[i].SetActive(true);
            }
            else
            {
                // �� ���� ���� ���� ������ ������Ʈ�� ��Ȱ��ȭ.
                obstacles[i].SetActive(false);
            }
        }
    }

    // ���𰡰� ���ǿ� ��´ٸ�
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾ ���ǿ� ������鼭 ���ǿ� ���� �ʾҴ� ���¶��.
        if (collision.collider.tag == "Player" && !stepped)
        {
            // ���ǿ� ���� ���·� �ʱ�ȭ.
            stepped=true;

            // ���ӸŴ����� AddScore() �޼��� ȣ��� 1�� �߰�.
            GameManager.Instance.AddScore(1);
        }     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
