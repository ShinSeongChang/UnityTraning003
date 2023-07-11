using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //      ����ƽ���� ����
    public static GameManager Instance = default;   // �̱����� �Ҵ��� ���� ����

    public bool isGameover = false;     //  ���ӿ��� ���¸� �Ǵ��� ����
    public Text scoreText = default;    //  ������ ����� �ؽ�Ʈ
    public GameObject gameoverUi = default; //  ���� ������ ȭ�鿡 ǥ���� ���ӿ��� Ui

    public int score = 0;      //  ������ ������ ���ھ�

    public void Awake()
    {
        if (Instance == null)   //  �ν��Ͻ��� ����ִٸ� 
        {
            Instance = this;    //  �ڱ� �ڽ��� �Ҵ�
        }
        else    //  �ν��Ͻ��� �̹� �Ҵ�Ǿ� �ִ� ���¶�� 
        {
            Debug.LogWarning("���� �� �� �̻��� ���ӸŴ����� �����մϴ�!");
            Destroy(gameObject);    //  �ڽ��� ������Ʈ�� �ı��� �Ѵ�.
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //  ���ӿ��� �����̸鼭 ���콺 ���ʹ�ư�� �����ٸ�
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            //  ���� �� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
     
    // ������ ������ų �޼���
    public void AddScore(int newScore)
    {
        //  ���ӿ��� ���°� Ʈ�簡 �ƴ϶�� ( �ʵ� ����� false�� �ʱ�ȭ ������ )
        if(!isGameover)
        {
            //  ���ھ�� �� ������ �߰����ش�.
            score += newScore;

            //  ���ھ� �ؽ�Ʈ���� ���ھ� ������ ����Ѵ�
            scoreText.text = "Score : " + score;    
        }
    }

    // �÷��̾ ������ ȣ���� �޼���
    public void OnPlayerDead()
    {
        // ���ӿ������¸� Ʈ��� �ٲ��ش�.
        isGameover = true;

        // ���߾� �ξ��� ( false ���¿��� ) ���ӿ��� Ui�� �����ش�. ( true )
        gameoverUi.SetActive(true);
    }

}
