    using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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


    public bool justMove = false;
    public GameObject currentSpider;
    int spiderPhase = 0;

    bool flipped = false;
    public bool isGrounded = false;

    float gravityScale;
    Rigidbody2D rb;
    public Animator anim;
    private bool canMove = true;
    SpiderHidding sh;
    SoundSpiderManager audio;
    WebCreation webCreation;

    bool isAttacking = false;
    public bool isGIant = false;

    public Text tutoText;

    const string up = "W";
    const string right = "D";
    const string down = "S";
    const string left = "A";
    const string action = "Q";

	// Use this for initialization
	void Start () {
        webCreation = GetComponent<WebCreation>();
        audio = GetComponent<SoundSpiderManager>();
        UpSpider();
        sh = GetComponent<SpiderHidding>();
        rb = GetComponent<Rigidbody2D>();
        Web.MakingWebEvent += SetCanMove;
        tutoText.text = "";
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
            tutoText.transform.Translate(0, 1, 0);
        }
        currentSpider.SetActive(true);
        anim = currentSpider.GetComponent<Animator>();
        audio.PlayLevelUp();
    }

	// Update is called once per frame
	void FixedUpdate () {


        if (leftDownCaptor.isTrigger || rightDownCaptor.isTrigger)
        {
            isGrounded = true;
            rb.gravityScale = gravityScale;
        }
        else if (!leftDownCaptor.isTrigger && !rightDownCaptor.isTrigger)
        {
            if (justMove)
            {
                justMove = false;
            }
            else
            {
                isGrounded = false;
                SetOrientation(HEAD_ORIENTATION.UP);
                rb.gravityScale = 1;
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            if(isGIant && Input.GetButtonDown("Action"))
            {
                Attack();
            }
            Move(h, v);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if(Input.GetButtonDown("Jump"))
        {
            SetOrientation(HEAD_ORIENTATION.UP);
        }
    }

    void Update()
    {
        if(webCreation.onWeb)
        {
            tutoText.text = action;
        }
        //else
        //{
        //    tutoText.text = "";
        //}
    }

    void Attack()
    {
        anim.SetTrigger("Eat");
        canMove = false;
        StartCoroutine(WaitAttackCoroutine());
    }

    IEnumerator WaitAttackCoroutine()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    void Move(float h, float v)
    {
        tutoText.text = "";
        switch (currentOrientation)
        {
            case HEAD_ORIENTATION.UP:
                if ((h < 0 && !flipped) || (h > 0 && flipped))
                    Flip();

                if(rightCaptor.isTrigger && !leftCaptor.isTrigger)
                {
                    tutoText.text = up;
                    if (v > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else if(leftCaptor.isTrigger && !rightCaptor.isTrigger)
                {
                    tutoText.text = up;
                    if (v > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }


                else if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                {
                    tutoText.text = down;
                    if(v < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                {
                    tutoText.text = down;
                    if (v < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else
                {
                    v = 0;
                }

                if( h!= 0 && isGrounded)
                {
                    audio.PlayWalk();
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

                if(rightCaptor.isTrigger && ! leftCaptor.isTrigger)
                {
                    tutoText.text = right;
                    if (h > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }

                else if (leftCaptor.isTrigger && ! rightCaptor.isTrigger)
                {
                    tutoText.text = right;
                    if (h > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }

                else if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                {
                    tutoText.text = left;
                    if (h < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                {
                    tutoText.text = left;
                    if (h < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                else
                {
                    h = 0;
                }


                if (v != 0 && isGrounded)
                {
                    audio.PlayWalk();
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

                if(rightCaptor.isTrigger && !leftCaptor.isTrigger)
                {
                    tutoText.text = down;
                    if (v < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else if(leftCaptor.isTrigger && !rightCaptor.isTrigger)
                {
                    tutoText.text = down;
                    if (v < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                {
                    tutoText.text = up;
                    if (v > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.LEFT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                {
                    tutoText.text = up;
                    if (v > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.RIGHT);
                        h = 0;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else
                {
                    v = 0;
                }

                if (h != 0 && isGrounded)
                {
                    audio.PlayWalk();
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

                if(rightCaptor.isTrigger && !leftCaptor.isTrigger)
                {
                    tutoText.text = left;
                    if (h < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                else if (leftCaptor.isTrigger && !rightCaptor.isTrigger)
                {
                    tutoText.text = left;
                    if (h < 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                else if (!rightDownCaptor.isTrigger && leftDownCaptor.isTrigger)
                {
                    tutoText.text = right;
                    if (h > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.UP);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                else if (!leftDownCaptor.isTrigger && rightDownCaptor.isTrigger)
                {
                    tutoText.text = right;
                    if (h > 0)
                    {
                        SetOrientation(HEAD_ORIENTATION.DOWN);
                        v = 0;
                    }
                    else
                    {
                        h = 0;
                    }
                }
                else
                {
                    h = 0;
                }

                if (v != 0 && isGrounded)
                {
                    audio.PlayWalk();
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
            if (currentOrientation == HEAD_ORIENTATION.UP || currentOrientation == HEAD_ORIENTATION.DOWN)
            {
                rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(h * moveSpeed, v * moveSpeed);
            }
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
                tutoText.rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
                Physics2D.gravity = Vector2.up * -9.81f;
                gravityScale = 1;
                break;
            case HEAD_ORIENTATION.RIGHT:
                transform.rotation = Quaternion.Euler(Vector3.forward * -90);
                tutoText.rectTransform.localRotation = Quaternion.Euler(Vector3.forward * 90);
                //Physics2D.gravity = Vector2.right * -9.81f;
                gravityScale = 0;
                break;
            case HEAD_ORIENTATION.DOWN:
                Physics2D.gravity = Vector2.up * 9.81f;
                gravityScale = 1;
                transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                tutoText.rectTransform.localRotation = Quaternion.Euler(Vector3.forward * 180);
                break;
            case HEAD_ORIENTATION.LEFT:
                transform.rotation = Quaternion.Euler(Vector3.forward * 90);
                tutoText.transform.localRotation = Quaternion.Euler(Vector3.forward * -90);
                //Physics2D.gravity = Vector2.right * -9.81f;
                gravityScale = 0;
                break;
            default:
                break;
        }
        justMove = true;
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
        audio.PlayDie();
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
