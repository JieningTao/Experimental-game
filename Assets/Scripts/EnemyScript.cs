using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private PlayerScript.Colors Color;

    [SerializeField]
    private float speed;

    private float chaseRange;
    private Collider2D myCollider;
    private SpriteRenderer mySpriteRenderer;
    private GameObject Target;
    private Rigidbody2D RB2D;

    void Start()
    {
        chaseRange = GetComponent<CircleCollider2D>().radius;
        myCollider = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        ConfirmColor();
        startNullColliders();
        RB2D = GetComponent<Rigidbody2D>();
        Target = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == Color.ToString())
        {
            Physics2D.IgnoreCollision(collision.collider, myCollider, true);
            
        }
    }

    void ConfirmColor()
    {
        if (Color == PlayerScript.Colors.Blue)
        {
            mySpriteRenderer.color = new Color(0.19f, 0.42f, 0.63f, 1f);
        }
        else if (Color == PlayerScript.Colors.Red)
        {
            mySpriteRenderer.color = new Color(0.66f, 0.19f, 0.18f, 1f);
        }
        else if (Color == PlayerScript.Colors.Yellow)
        {
            mySpriteRenderer.color = new Color(0.76f, 0.75f, 0.01f, 1f);
        }
    }

    void startNullColliders()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position,chaseRange);

        foreach (Collider2D C in collidersInRange)
        {
            if (C.CompareTag(Color.ToString()))
                Physics2D.IgnoreCollision(myCollider, C,true);
        }
    }

    private void Update()
    {
        if (Target != null)
        {
            //transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
            TurnToVector(Target.transform.position);
            RB2D.velocity = speed * Time.deltaTime * transform.up;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Target = null;
        }
    }

    public void TurnToVector(Vector2 nextWaypoint)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(nextWaypoint.y - transform.position.y, nextWaypoint.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }
}
