using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���ӸŴ����󿡼� ���ӿ������°� �ƴ϶��.
        // ���ӿ������°� �ȴٸ� ���� ������ �̵����� �׸��ΰ� �ȴ�.
        if(!GameManager.Instance.isGameover)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
