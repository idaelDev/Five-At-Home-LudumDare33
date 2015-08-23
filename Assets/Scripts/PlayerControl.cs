using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {


    public bool isDead = false;
    public float moveSpeed = 5f;
    public WallCaptor rightCaptor;
    public WallCaptor leftCaptor;
    public WallCaptor rightDownCaptor;
    public WallCaptor leftDownCaptor;
    public HEAD_ORIENTATION currentOrientation = HEAD_ORIENTATION.UP;

    public GameObject SpiderV1;
    public GameObject SpiderV2;
    public GameObject SpiderV3;

    public GameObject currentSpider;
    int spiderPhase = 0;

    bool flipped = false;
    public bool isGrounded = false;

    float gravityScale;
    Rigidbody2D rb;
    public Animator anim;
    private bool canMove = true;
    SpiderHidding sh;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        sh = GetComponent<SpiderHidding>();
        rb = GetComponent<Rigidbody2D>();
        Web.MakingWebEvent += SetCanMove;
        audio = GetComponent<AudioSource>();
        UpSpider();
	}
	
    public void UpSpider()
    {
        spiderPhase++;
        if(flipped)
        {
            Flip();
        }
        if(spiderPhase == 1)
        {
            currentSpider = SpiderV1;
            SpiderV2.SetActive(false);
            SpiderV3.SetActive(false);
        }
        else if (spiderPhase == 2)
        {
            currentSpider = SpiderV2;
            SpiderV1.SetActive(false);
            SpiderV3.SetActive(false);
        }
        else if (spiderPhase == 3)
        {
            currentSpider = SpiderV3;
            SpiderV1.SetActive(false);
            SpiderV2.SetActive(false);
        }
        currentSpider.SetActive(true);
        anim = currentSpider.GetComponent<Animator>();
    }

	// Update is called once per frame
	void FixedUpdate () {

        isGrounded = false;
        rb.gravityScale = 1;


        if (leftDownCaptor.isTrigger || rightDownCaptor.isTrigger)
        {
            isGrounded = true;
            rb.gravityScale = gravityScale;
        }
        else if(!leftDownCaptor.isTrigger && !rightDownCaptor.isTrigger)
        {
            SetOrientation(HEAD_ORIENTATION.UP);
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (canMove)
        {
            Move(h, v);
        }

        if(Input.GetButtonDown("Jump"))
        {
            SetOrientation(HEAD_ORIENTATION.UP);
        }



    }

    void Move(float h, float v)
    {
        switch (currentOrientation)
        {
            case HEAD_ORIENTATION.UP:
                if ((h < 0 && !flipped) || (h > 0 && flipped))
                    Flip();
                if(v > 0)
                {
                    if(rightCaptor.isTrigger && !leftCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else if(leftCaptor.isTrigger && !rightCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                if(v < 0)
                {
                    if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                if(!audio.isPlaying && h!= 0 && isGrounded)
                {
                    audio.Play();
                }
                else if(h==0)
                {
                    audio.Stop();
                }
                anim.SetBool("Walk", h!=0);
                break;
            case HEAD_ORIENTATION.RIGHT:
                if ((v > 0 && !flipped) || (v < 0 && flipped))
                    Flip();
                if(h > 0)
                {
                    if(rightCaptor.isTrigger && ! leftCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else if (leftCaptor.isTrigger && ! rightCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                if( h < 0)
                {
                    if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                if (!audio.isPlaying && v != 0 && isGrounded)
                {
                    audio.Play();
                }
                else if (v == 0)
                {
                    audio.Stop();
                }
                anim.SetBool("Walk", v != 0);
                break;
            case HEAD_ORIENTATION.DOWN:
                if ((h > 0 && !flipped) || (h < 0 && flipped))
                    Flip();
                if(v < 0)
                {
                    if(rightCaptor.isTrigger && !leftCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else if(leftCaptor.isTrigger && !rightCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                if(v > 0)
                {
                    if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                if (!audio.isPlaying && h != 0 && isGrounded)
                {
                    audio.Play();
                }
                else if (h == 0)
                {
                    audio.Stop();
                }
                anim.SetBool("Walk", h != 0);
                break;
            case HEAD_ORIENTATION.LEFT:
                if ((v < 0 && !flipped) || (v > 0 && flipped))
                    Flip();
                if(h < 0)
                {
                    if(rightCaptor.isTrigger && !leftCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else if (leftCaptor.isTrigger && !rightCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                if( h > 0)
                {
                    if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                if (!audio.isPlaying && v != 0 && isGrounded)
                {
                    audio.Play();
                }
                else if (v == 0)
                {
                    audio.Stop();
                }
                anim.SetBool("Walk", v != 0);
                break;
            default:
                break;
        }
        
        if (isGrounded)
        {
            rb.velocity = new Vector2(h * moveSpeed, v * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);
        }

       // rb.MovePosition(transform.position + movement);
    }

    void SetOrientation(HEAD_ORIENTATION orientation)
    {
        currentOrientation = orientation;
        switch (orientation)
        {
            case HEAD_ORIENTATION.UP:
                transform.rotation = Quaternion.Euler(Vector3.zero);
                Physics2D.gravity = Vector2.up * -9.81f;
                gravityScale = 1;
                break;
            case HEAD_ORIENTATION.RIGHT:
                transform.rotation = Quaternion.Euler(Vector3.forward * -90);
                Physics2D.gravity = Vector2.up * -9.81f;
                gravityScale = 0;
                break;
            case HEAD_ORIENTATION.DOWN:
                transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                Physics2D.gravity = Vector2.up * 9.81f;
                gravityScale = 1;
                break;
            case HEAD_ORIENTATION.LEFT:
                transform.rotation = Quaternion.Euler(Vector3.forward * 90);
                Physics2D.gravity = Vector2.up * -9.81f;
                gravityScale = 0;
                break;
            default:
                break;
        }
    }

    public void SetCanMove(bool can)
    {
        canMove = can;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        flipped = !flipped;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = currentSpider.transform.localScale;
        theScale.x *= -1;
        currentSpider.transform.localScale = theScale;
    }

    public void Die()
    {
        sh.isHidding = true;
        canMove = false;
        anim.SetTrigger("Die");
        StartCoroutine(WaitForRestartCoroutine());
    }

    IEnumerator WaitForRestartCoroutine()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(Application.loadedLevelName);
    }
}

public enum HEAD_ORIENTATION
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}
