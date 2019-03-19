using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body2D;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Collider2D ThisCollision2D;
    [SerializeField]
    private string InitialColor;
    [SerializeField]
    private bool HaveBlue;
    [SerializeField]
    private bool HaveRed;
    [SerializeField]
    private bool HaveYellow;


    private string MyColor;
    private List<string> AvaliableColors = new List<string>();
    private List<Collider2D> SavedCollider2Ds = new List<Collider2D>();
    private int selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        AvaliableColors.Add(InitialColor);

        if (HaveBlue) 
        AvaliableColors.Add("Blue");
        if (HaveRed) 
        AvaliableColors.Add("Red");
        if(HaveYellow)
        AvaliableColors.Add("Yellow");


        MyColor = AvaliableColors[0];

        

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);



        body2D.velocity = movement*speed;

        // Boost from UFO script
        /*
        if (Input.GetKeyDown("space"))
            body2D.AddForce(movement * speed * 50);
        else
            body2D.AddForce(movement * speed);
            */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == MyColor)
        {
            SavedCollider2Ds.Add(collision.collider);
            Physics2D.IgnoreCollision(collision.collider, ThisCollision2D,true);
            
        }


        
    }

    private void HandleColorChange()
    {

        if (Input.GetKey(KeyCode.Q) )
        {
            
                selectedColor--;
            Debug.Log("--");
            if (selectedColor < 0)
            {
                selectedColor++;
            }
            else
            {
                Debug.Log(selectedColor);
                Debug.Log(AvaliableColors[selectedColor]);
                MyColor = AvaliableColors[selectedColor];
                ConfirmColorChange();
            }

          
            
        }
        else if (Input.GetKey(KeyCode.E))
        {
            
                selectedColor++;
            Debug.Log("++");
            if (selectedColor > AvaliableColors.Count - 1)
            {
                selectedColor--;
            }
            else
            {
                Debug.Log(selectedColor);
                Debug.Log(AvaliableColors[selectedColor]);
                MyColor = AvaliableColors[selectedColor];
                ConfirmColorChange();
            }
        }



    }

    private void ConfirmColorChange()
    {
        if (MyColor =="Blue")
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(0f, 0.5239f, 1f, 1f); // Set to opaque gray
        }
        if (MyColor == "White")
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 1f, 1f, 1f); // Set to opaque gray
        }
        if (MyColor == "Red")
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(0.96f, 0.27f, 0.25f, 1f); // Set to opaque gray
        }
        if (MyColor == "Yellow")
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 0.99f, 0.19f, 1f); // Set to opaque gray
        }



        //resets all saved colliders
        foreach (Collider2D collider in SavedCollider2Ds)
        {
            Physics2D.IgnoreCollision(collider, ThisCollision2D, false);
        }
        SavedCollider2Ds.Clear();
    }

    public void GetColor(string Color)
    {
        AvaliableColors.Add(Color);
        Debug.Log("Player got: " + Color);
    }



    private void FixedUpdate()
    {
        HandleColorChange();
        
    }
}
