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
    PlayerControl pc;

    SoundEnemySound audio;
    bool isDead = false;

	// Use this for initialization
	void Start () 
    {
        audio = GetComponent<SoundEnemySound>();
        evf = GetComponentInChildren<EnemyViewField>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walk", true);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        evf.SeeSpiderEvent += AttackingState;
    }

    void AttackingState()
    {
        currentState = EnemyState.ATTACK;
        audio.PlayDetection();
    }
	
	// Update is called once per frame
	void FixedUpdate() 
    {

        switch (currentState)
        {
            case EnemyState.NORMAL:
                rb.velocity = new Vector2(normalSpeed, 0);
                break;
            case EnemyState.ATTACK:
                AttackMovement();
                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }
	}

    void Update()
    {
        //if (evf.seeSpider)
        //{
        //    Debug.Log("Attack !!!");
        //    currentState = EnemyState.ATTACK;
        //}
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
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("Attack");
        }
    }

    void Attack()
    {
        if(evf.seeSpider && !pc.isGIant)
        {
            Debug.Log(Mathf.Abs(transform.position.x - pc.gameObject.transform.position.x));
            if(Mathf.Abs(transform.position.x - pc.gameObject.transform.position.x)<= stopDistance)
            {
                pc.Die();
            }
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

    public void Die()
    {
        currentState = EnemyState.DEAD;
        isDead = true;
        audio.PlayDie();
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}

public enum EnemyState
{
    NORMAL,
    ATTACK,
    DEAD
}
