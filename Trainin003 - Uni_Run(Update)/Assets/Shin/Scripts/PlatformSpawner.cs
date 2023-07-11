using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // 플랫폼 프리팹을 할당할 게임오브젝트 선언.
    public GameObject platformPrefabs;
    public GameObject coinPrefabs;
    public GameObject meatPrefabs;

    // 플랫폼의 갯수 제한.
    public int count = 3;
    public int coinCount = 5;

    // 플랫폼의 생성주기를 만들 각 변수 Spawn = Min ~ Max .
    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn;

    // 플랫폼의 좌표 위치 제한시킬 각 변수 
    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 20f;

    // 미리 생성해두었던 발판들
    private GameObject[] platforms;

    // 현재 사용할 발판의 인덱스 값.
    private int currentIndex = 0;

    // 초반에 사용할 발판을 숨겨둘 위치.
    private Vector2 poolPosition = new Vector2(0, -25);
    private Vector2 coinPosition = new Vector2(6, 3);
    private Vector2 meatPosition;


    // 마지막 배치 시점 시간.
    private float lastSpawnTime;
    public bool meat_Spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
        // 시작시 생성할 발판의 갯수 => 해당 프로젝트에서는 3개로 할당.
        platforms = new GameObject[count];

        // 플랫폼의 생성갯수 만큼 (count) 프리팹을 인스턴스화 시켜준다 .
        for(int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefabs, poolPosition, Quaternion.identity);
        }

        Instantiate(coinPrefabs, coinPosition, Quaternion.identity);
        // 마지막 배치 시간시점과 생성주기 0으로 초기화.
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

        // 게임매니저의 게임오버 상태가 된다면.
        if (GameManager.Instance.isGameover)
        {
            // 랜덤 플랫폼 생성을 멈춘다.
            return;
        }
        
        // 현재시간이   마지막 배치 시점에서  timeBetSpawn 만큼의 시간이 흘렀다면.
        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            
            // 마지막 배치 시점에 현재 시간을 대입.
            lastSpawnTime = Time.time;

            // 생성시간에 생성 주기를 대입 ( Min ~ Max ).
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // 생성될 발판의 y값은 랜덤한 Min ~ Max 값
            float yPos = Random.Range(yMin, yMax);

            // 생성된 플랫폼의 오브젝트를 비활성화 시키면서 바로 활성화 => Platform 스크립트의 OnEnable() 메소드가 실행됨.
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // 생성된 플랫폼이 위치할 좌표값 x,y .
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

            // 다음 순번의 플랫폼을 생성시키기 위한 인덱스값 증가.
            currentIndex++;
        }

        // 인덱스값이 카운트값 이상이 되면 0으로 초기화시켜 0번순의 플랫폼부터 생성되게 한다.
        if(currentIndex >= count)
        {
            currentIndex = 0;
        }

    }

}
