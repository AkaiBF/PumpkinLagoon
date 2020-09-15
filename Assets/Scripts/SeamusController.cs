using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum LookingAt
{
    right,
    left,
    bottom,
    up
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SeamusController : MonoBehaviour
{

    public GameController gc;
    /*Movement*/
    [Range(0.01f, 0.2f)]
    public float speed;
    public Grid grid;
    public bool teleporting = false;
    public bool posteleporting = false;

    public Weapon weapon;
    public int maxHealth = 500;
    private int health;
    public int armor = 0;
    public GameObject slider;

    public int calabazas = 0;
    public GameObject calaTxt;

    private LookingAt lookDir = LookingAt.bottom;
    public Vector2 destination;
    private bool lastRight = false;
    Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        calaTxt.GetComponent<Text>().text = calabazas.ToString();
        destination = GetComponent<Transform>().position;
    }

    bool CanWalk(Vector2 direction)
    {
        Vector2 seamusPos = GetComponent<Transform>().position;
        RaycastHit2D hit = Physics2D.Linecast(seamusPos , seamusPos + direction);

        return hit.collider == GetComponent<Collider2D>() || !hit.collider;
    }

    // Update is called once per frame
    void Update()
    {
        if(teleporting)
        {
            destination = transform.position;
            teleporting = false;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Heal(30, true); 
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            GetCalabaza();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            //Attack();
        }
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            input.y = 0;
        } else
        {
            input.x = 0;
        }
        UpdateLook();
    }

    private void FixedUpdate()
    {
        if (!gc.IsPaused())
        {
            Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed);
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            if ((Vector2)this.transform.position == destination)
            {
                if (Input.GetKey(KeyCode.UpArrow) && CanWalk(Vector2.up))
                {
                    destination = (Vector2)this.transform.position + (Vector2.up);
                    posteleporting = false;
                }
                if (Input.GetKey(KeyCode.RightArrow) && CanWalk(Vector2.right))
                {
                    destination = (Vector2)this.transform.position + (Vector2.right);
                    posteleporting = false;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && CanWalk(Vector2.left))
                {
                    destination = (Vector2)this.transform.position + (Vector2.left);
                    posteleporting = false;
                }
                if (Input.GetKey(KeyCode.DownArrow) && CanWalk(Vector2.down))
                {
                    destination = (Vector2)this.transform.position + (Vector2.down);
                    posteleporting = false;
                }
            }
        }
    }

    private void UpdateLook()
    {
        Animator anim = GetComponent<Animator>();
        if (input.x > 0)
        {
            if (!lastRight)
            {
                lastRight = true;
                GetComponent<SpriteRenderer>().flipX = lastRight;
            }
            anim.SetTrigger("WalkRight");
            lookDir = LookingAt.right;
        }
        else if (input.x < 0)
        {
            if (lastRight)
            {
                lastRight = false;
                GetComponent<SpriteRenderer>().flipX = lastRight;
            }
            anim.SetTrigger("WalkLeft");
            lookDir = LookingAt.left;
        }
        else if (input.y < 0)
        {
            if (lastRight)
            {
                lastRight = false;
                GetComponent<SpriteRenderer>().flipX = lastRight;
            }
            anim.SetTrigger("WalkDown");
            lookDir = LookingAt.bottom;
        }
        else if (input.y > 0)
        {
            if (lastRight)
            {
                lastRight = false;
                GetComponent<SpriteRenderer>().flipX = lastRight;
            }
            anim.SetTrigger("WalkUp");
            lookDir = LookingAt.up;
        }
        else
            anim.SetTrigger("Idle");
    }

    private Vector2 forward()
    {
        Vector2 targetRelative = new Vector2();
        switch (lookDir)
        {
            case LookingAt.bottom:
                targetRelative = Vector2.down;
                break;
            case LookingAt.left:
                targetRelative = Vector2.left;
                break;
            case LookingAt.up:
                targetRelative = Vector2.up;
                break;
            case LookingAt.right:
                targetRelative = Vector2.right;
                break;
        }
        return targetRelative;
    }

    public void Receives(int dmg)
    {

        health -= (dmg * (100-armor) / 100) + 1;
        float slihealth = (float)health / (float)maxHealth;
        slider.GetComponent<Image>().fillAmount = slihealth;
    }
    private void GetCalabaza()
    {
        if (calabazas < 99)
        {
            Vector2 seamusPos = GetComponent<Transform>().position;
            RaycastHit2D hit = Physics2D.Linecast(seamusPos, seamusPos + forward());
            if (hit.collider.gameObject.CompareTag("calabaza"))
            {
                
                hit.collider.gameObject.SetActive(false);
                calabazas++;
            }
            if(hit.collider.gameObject.CompareTag("NPC"))
            {

                hit.collider.gameObject.GetComponent<NPCController>().Talk();
            }
        }
        calaTxt.GetComponent<Text>().text = calabazas.ToString();
    }
    private void UseCalabaza()
    {
        if(calabazas > 0)
        {
            calabazas--;
        }
        calaTxt.GetComponent<Text>().text = calabazas.ToString();
    }
    private void Heal(int amount, bool calabaza)
    {
        if(calabaza)
        {
            UseCalabaza();
            if(health + amount <= maxHealth)
            {
                health += amount;
            } else
            {
                health = maxHealth;
            }
        }
        float slihealth = (float)health / (float)maxHealth;
        slider.GetComponent<Image>().fillAmount = slihealth;
    }
}
