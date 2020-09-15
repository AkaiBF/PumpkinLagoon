using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public List<string> conversation;
    private int _xPos;
    private int _yPos;
    private Vector2 destination;
    private float speed = 0.008f;
    public GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        this._xPos = 0;
        this._yPos = 0;
        this.destination = this.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gc.IsPaused())
        {
            Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed);
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            if ((Vector2)this.transform.position == destination)
            {
                if (NumericTools.InsideProbability(50))
                {
                    // Horizontal
                    int prob = 50 + 25 * (int)Mathf.Abs(this._xPos);
                    if (NumericTools.InsideProbability(prob))
                    {
                        Move(-1 * NumericTools.Sign(this._xPos), 0);
                    }
                    else
                    {
                        Move(1 * NumericTools.Sign(this._xPos), 0);
                    }
                }
                else
                {
                    // Vertical
                    int prob = 50 + 25 * (int)Mathf.Abs(this._yPos);
                    if (NumericTools.InsideProbability(prob))
                    {
                        Move(0, -1 * NumericTools.Sign(this._yPos));
                    }
                    else
                    {
                        Move(0, 1 * NumericTools.Sign(this._yPos));
                    }
                }
            }
        }
    }

    private bool CanMove(Vector2 direction)
    {

        Vector2 seamusPos = GetComponent<Transform>().position;
        RaycastHit2D hit = Physics2D.Linecast(seamusPos, seamusPos + direction);

        return hit.collider == GetComponent<Collider2D>() || !hit.collider;
    }

    private void Move(int x, int y)
    {
        Vector2 dir = new Vector2(x, y);
        if(CanMove(dir))
        {
            this.destination = this.destination + dir;
            this._xPos += (int)dir.x;
            this._yPos += (int)dir.y;
        }
    }
}
