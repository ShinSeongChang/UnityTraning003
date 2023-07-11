using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigid;
    private Animator animator;
    private AudioSource playerAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        // isDead ���¶�� ���� ����
        if(isDead)
        {
            return;
        }

        // ���콺 ���ʹ�ư Ű�� ������ ���� ����ī��Ʈ�� 2 �̸��϶� ( ����Ƚ�� 2ȸ�� ���� )
        else if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // ����ī��Ʈ ����
            jumpCount++;

            // �����ϴ� ���� �÷��̾��� �ӵ��� 0
            playerRigid.velocity = Vector2.zero;

            // 2D �ϱ� Vector2 ���(x,y��), y�࿡ ����������ŭ ���� �༭ ��������
            playerRigid.AddForce(new Vector2 (0, jumpForce));

            // ����� �ҽ� ���
            playerAudio.Play();     

        }

        // ���콺 ���ʹ�ư Ű�� ������ ���� ���� �÷��̾ ������ ���¶�� == y ���� 0 �ʰ����
        else if(Input.GetMouseButtonUp(0) && playerRigid.velocity.y > 0)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }

        // �ִϸ�����.������Ÿ��(���⿡���� bool����) ("Ű��" == "Grounded" �Ķ���� , Ű���� �� ���� == isgronded �� �Լ��� ���� ���°� ���� ����)
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        // �ִϸ�����.������ Ÿ�� (���⿡���� Trigger ����) ( "Ű��" == "Die" �Ķ���� )
        animator.SetTrigger("Die");

        //  �÷��̾� ����� �ҽ� Ŭ���� ��ü ( deathŬ������ )
        playerAudio.clip = deathClip;

        //  ��ü�� ������ҽ��� �ٽ� �÷��� ( �׾����� ������� ��ȯ )
        playerAudio.Play();

        // �÷��̾�� ������ ���ڸ����� �ӵ��� �ҰԵȴ�.
        playerRigid.velocity = Vector2.zero;

        // isDead�� true�� ����� �� �Լ����� �ۿ��ϰ� �Ѵ�.
        isDead = true;                  

        // ���ӸŴ����� �÷��̾� ���� ���� �޼��� ȣ��
        GameManager.Instance.OnPlayerDead();

    }


    //  ������ Ʈ���ſ� ��� ����
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //  �ش� Ʈ���� ��ü�� �±װ� "Dead" �̸鼭 ���� ���°� !isDead��� ( true ��� ==> �ʱ갪�� false )
        if (other.tag == "Dead" && !isDead)     
        {
            //  Die �Լ��� �����Ѵ�. ( �÷��̾�� Die �ִϸ��̼��� �����ָ� ���ڸ����� �ӵ��� �Ұ� �ȴ� )
            Die();      
        }
        else if(other.tag == "Meat")
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f);
        }


    }

    // ������ �ݶ��̴��� ��� ���� ( ���� ��� �ִ� ���� )
    public void OnCollisionEnter2D(Collision2D collision)   
    {
        //  �浹 ������ ���� Ȥ�� �������̶�� ( ������ �ε��� ��Ȳ ���� )
        //  y���� 1�� �������� �ϸ��� ���, 1�̶�� ���� ����, -1 �̶�� �Ʒ�����
        if (collision.contacts[0].normal.y > 0.7f)      
        {
            //  ���̶�� ���� ( �� �ִϸ��̼� ���� )
            isGrounded = true;

            //  ���� ī��Ʈ�� �ʱ�ȭ �����ش�.
            jumpCount = 0;          
        }
    }

    // ������ �ݶ��̴����� ����� ���� ( ������ ����� ���� )
    public void OnCollisionExit2D(Collision2D collision)    
    {
        // ���� �ƴ϶�� ���� ( ���� �ִϸ��̼� ����, ���� ī��Ʈ �� )
        isGrounded = false;     
    }
}
