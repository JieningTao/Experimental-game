using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private PlayerScript.Colors Color;

    [SerializeField]
    private float chaseRange;

    [SerializeField]
    private float speed;

    [SerializeField]
    private LayerMask playerLayer;

    private Collider2D myCollider;
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        ConfirmColor();
        startNullColliders();
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

    private void FixedUpdate()
    {
        
    }

    private bool CheckPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, chaseRange, playerLayer) != null;
    }

}
