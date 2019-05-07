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
    private Colors InitialColor;
    [SerializeField]
    private bool HaveBlue;
    [SerializeField]
    private bool HaveRed;
    [SerializeField]
    private bool HaveYellow;
    [SerializeField]
    private LayerMask checkmask;

    [SerializeField]
    private HUDScript hud;


    private Colors MyColor;
    private List<Colors> AvaliableColors = new List<Colors>();
    private List<Collider2D> SavedCollider2Ds = new List<Collider2D>();
    private Collider2D checkCollider;
    private Collider2D[] checkHitResults = new Collider2D[20];
    private int selectedColor;


    public enum Colors
    {
        White,
        Blue,
        Red,
        Yellow,
    }

    public void updateHUD()
    {
        if (AvaliableColors.Count > 1)
            hud.PromptText.SetActive(true);
        if(AvaliableColors.Contains(Colors.Blue))
            hud.Blue.SetActive(true);
        else
            hud.Blue.SetActive(false);
        if (AvaliableColors.Contains(Colors.Red))
            hud.Red.SetActive(true);
        else
            hud.Red.SetActive(false);
        if (AvaliableColors.Contains(Colors.Yellow))
            hud.Yellow.SetActive(true);
        else
            hud.Yellow.SetActive(false);
    }

    //consider using seperate colliders for different colors.
    // Start is called before the first frame update
    void Start()
    {
        AvaliableColors.Add(InitialColor);

        if (HaveBlue) 
        AvaliableColors.Add(Colors.Blue);
        if (HaveRed) 
        AvaliableColors.Add(Colors.Red);
        if(HaveYellow)
        AvaliableColors.Add(Colors.Yellow);


        MyColor = AvaliableColors[0];
        checkCollider = GetComponentInChildren<Collider2D>();

        updateHUD();

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();


        body2D.velocity = movement*speed*Time.deltaTime;

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

        if (collision.gameObject.tag == MyColor.ToString())
        {
            SavedCollider2Ds.Add(collision.collider);
            Physics2D.IgnoreCollision(collision.collider, ThisCollision2D,true);
            
        }


        
    }

    private void HandleColorChange()
    {
        

        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (IsInWhite())
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
            else
                Debug.Log("Can't change");



        }
        else if (Input.GetKeyDown(KeyCode.E))
        {

            if (IsInWhite())
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
            else
                Debug.Log("Can't change");
        }
        
        



    }

    private void ConfirmColorChange()
    {
        if (MyColor ==Colors.Blue)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(0f, 0.5239f, 1f, 1f);
        }
        if (MyColor == Colors.White)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 1f, 1f, 1f); 
        }
        if (MyColor == Colors.Red)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(0.96f, 0.27f, 0.25f, 1f); 
        }
        if (MyColor == Colors.Yellow)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 0.99f, 0.19f, 1f); 
        }



        //resets all saved colliders
        foreach (Collider2D collider in SavedCollider2Ds)
        {
            Physics2D.IgnoreCollision(collider, ThisCollision2D, false);
        }
        SavedCollider2Ds.Clear();
    }

    public void GetColor(Colors Color)
    {
        if(!AvaliableColors.Contains(Color))
        AvaliableColors.Add(Color);
        Debug.Log("Player got: " + Color);
        updateHUD();
    }



    private void FixedUpdate()
    {

            HandleColorChange();
        
    }

    private bool IsInWhite()
    {
        return Physics2D.OverlapCircle(transform.position, 0.47f,checkmask) == null;
    }
}
