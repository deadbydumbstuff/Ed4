using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Player_Core player;
    [SerializeField] Rigidbody2D rb;

    [Header("CameraFollow")]
    public Transform cameras;
    public Vector2 cameraBoundingBox;
    [SerializeField] bool ReCenter;
    float x;
    float y;


    [Header("Directions")]
    public Sprite[] DirSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        Move();

        //check if camera is in bounding box
        if (Mathf.Abs(cameras.position.x - transform.position.x) > cameraBoundingBox.x ||Mathf.Abs(cameras.position.y - transform.position.y) > cameraBoundingBox.y)
        {
            ReCenter = true;
        }

        if (ReCenter)
        {
            Vector2 e = transform.position - cameras.position;
            cameras.position += new Vector3(e.x * 2,e.y * 2,0) * Time.deltaTime;
            
        }
        if (Vector2.Distance(cameras.position, transform.position) <= 0.3)
        {
            ReCenter = false;
        }
    }
    void Move()
    {
        Vector2 dir = new(x, y);
        dir.Normalize();
        rb.linearVelocity += player.Speed * Time.deltaTime * dir;
        //+= player.Speed * Time.deltaTime * dir;
        SpriteRenderer Sr = transform.GetComponent<SpriteRenderer>();
        //change sprite based on currnt direction
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            Sr.sprite = (x >= 0) ? DirSprite[0] : DirSprite[1];
        }
        else if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            Sr.sprite = (y > 0) ? DirSprite[2] : DirSprite[3];
        }
     
    }

}
