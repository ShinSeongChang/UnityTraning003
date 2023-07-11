using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    // �ڽ��� ����� ���� ���� ����
    private float width = default;
    // Start is called before the first frame update

    private void Awake()    // Awake �Լ��� start ���� �������� ���� ����ȴ� ( ���Ŀ� Unity Life cycle ���� )
    {
        //  BoxCollider2D ������Ʈ�� �����Ѵ�
        BoxCollider2D backgroindCollider = GetComponent<BoxCollider2D>();
        width = backgroindCollider.size.x;  //  �ڽ��ݶ��̴��� ����� ��´�. 
    }
    void Start()
    {

    }

    public void Reposition()    //  ����� �ٽ� ���ڸ��� ������ ��ų �Լ�
    {        
        Vector2 offset = new Vector2(width * 2f, 0);    // �̵� ��ų ��ġ�� Vector2 �� ( x�� == �ڽ��� ������ * 2 ĭ���� �Ѵ� )
        transform.position = (Vector2)transform.position + offset;      //  transform.position �� offset�� �����Ѵ�.
    }

    // Update is called once per frame
    void Update()
    {
        // update�� ���� ������ ����Ǵ� ���� �ڽ��� ��ġ�� ��� Ž���Ѵ�
        if (transform.position.x <= -width)     // ���� �ڽ��� ������ ġ����ŭ -�� ���Ѵٸ� (�ڽ��� ũ�⸸ŭ �������� ���ٸ� )
        {
            Reposition();   //  �������� ���ش�.
        }
    }
}
