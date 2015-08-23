using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float normalSpeed = 5f;
    public float attackSpeed = 5f;
    public float panicSpeed = 10f;
    public bool lookRight = true;
    public float stopDistance = 1;

    Rigidbody2D rb;
    Animator anim;
    Vector3 lastNewPosition;
    EnemyState currentState = EnemyState.NORMAL;

    EnemyViewField evf;

	// Use this for initialization
	void Start () 
    {
        evf = GetComponentInChildren<EnemyViewField>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walk", true);
    }
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        if(evf.seeSpider)
        {
            Debug.Log("Attack !!!");
            currentState = EnemyState.ATTACK;
        }
        switch (currentState)
        {
            case EnemyState.NORMAL:
                rb.velocity = new Vector2(normalSpeed, 0);
                break;
            case EnemyState.ATTACK:
                AttackMovement();
                break;
            case EnemyState.PANIC:
                break;
            default:
                break;
        }
	}

    void AttackMovement()
    {
        if(evf.lastPosition.x - transform.position.x > stopDistance)
        {
            if (!lookRight)
                Flip();
            rb.velocity = new Vector2(attackSpeed, 0);
        }
        else if (evf.lastPosition.x - transform.position.x < -stopDistance)
        {
            if (lookRight)
                Flip();
            rb.velocity = new Vector2(attackSpeed, 0);
        }
        else
        {
            Attack();
        }
    }

    void Attack()
    {
        if(evf.seeSpider)
        {
            Debug.Log("attacking");
        }
        currentState = EnemyState.NORMAL;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Limit" && currentState == EnemyState.NORMAL)
        {
            Flip();
        }
    }

    void Flip()
    {
        lookRight = !lookRight;
        normalSpeed *= -1;
        attackSpeed *= -1;
        panicSpeed *= -1;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}

public enum EnemyState
{
    NORMAL,
    ATTACK,
    PANIC
}
