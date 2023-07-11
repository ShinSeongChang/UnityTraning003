using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // �÷��� �������� �Ҵ��� ���ӿ�����Ʈ ����.
    public GameObject platformPrefabs;
    public GameObject coinPrefabs;
    public GameObject meatPrefabs;

    // �÷����� ���� ����.
    public int count = 3;
    public int coinCount = 5;

    // �÷����� �����ֱ⸦ ���� �� ���� Spawn = Min ~ Max .
    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn;

    // �÷����� ��ǥ ��ġ ���ѽ�ų �� ���� 
    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 20f;

    // �̸� �����صξ��� ���ǵ�
    private GameObject[] platforms;

    // ���� ����� ������ �ε��� ��.
    private int currentIndex = 0;

    // �ʹݿ� ����� ������ ���ܵ� ��ġ.
    private Vector2 poolPosition = new Vector2(0, -25);
    private Vector2 coinPosition = new Vector2(6, 3);
    private Vector2 meatPosition;


    // ������ ��ġ ���� �ð�.
    private float lastSpawnTime;
    public bool meat_Spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
        // ���۽� ������ ������ ���� => �ش� ������Ʈ������ 3���� �Ҵ�.
        platforms = new GameObject[count];

        // �÷����� �������� ��ŭ (count) �������� �ν��Ͻ�ȭ �����ش� .
        for(int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefabs, poolPosition, Quaternion.identity);
        }

        Instantiate(coinPrefabs, coinPosition, Quaternion.identity);
        // ������ ��ġ �ð������� �����ֱ� 0���� �ʱ�ȭ.
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.score % 30 == 0 && GameManager.Instance.score != 0)
        {
            meat_Spawn = true;
        }

        // ���ӸŴ����� ���ӿ��� ���°� �ȴٸ�.
        if (GameManager.Instance.isGameover)
        {
            // ���� �÷��� ������ �����.
            return;
        }
        
        // ����ð���   ������ ��ġ ��������  timeBetSpawn ��ŭ�� �ð��� �귶�ٸ�.
        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            
            // ������ ��ġ ������ ���� �ð��� ����.
            lastSpawnTime = Time.time;

            // �����ð��� ���� �ֱ⸦ ���� ( Min ~ Max ).
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // ������ ������ y���� ������ Min ~ Max ��
            float yPos = Random.Range(yMin, yMax);

            // ������ �÷����� ������Ʈ�� ��Ȱ��ȭ ��Ű�鼭 �ٷ� Ȱ��ȭ => Platform ��ũ��Ʈ�� OnEnable() �޼ҵ尡 �����.
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // ������ �÷����� ��ġ�� ��ǥ�� x,y .
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            if(meat_Spawn == true)
            {
                meatPosition = new Vector2(xPos, yPos + 4);
                Instantiate(meatPrefabs, meatPosition, Quaternion.identity);

                meat_Spawn = false;
            }

            for (int i = 0; i < coinCount; i++)
            {
                float coin_yPos = Random.Range(yPos+3, yPos +5);
                float coin_xPos = Random.Range(xPos-5, xPos+5);

                if(coin_yPos >= 4.5f)
                {
                    coin_yPos = 4.5f;
                }

                coinPosition = new Vector2(coin_xPos, coin_yPos);

                Instantiate(coinPrefabs, coinPosition, Quaternion.identity);

            }

            // ���� ������ �÷����� ������Ű�� ���� �ε����� ����.
            currentIndex++;
        }

        // �ε������� ī��Ʈ�� �̻��� �Ǹ� 0���� �ʱ�ȭ���� 0������ �÷������� �����ǰ� �Ѵ�.
        if(currentIndex >= count)
        {
            currentIndex = 0;
        }

    }

}
