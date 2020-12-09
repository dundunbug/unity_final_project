using System.Collections;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public Animator animator_flying;
    public int AttackMode;
	private float windForce = 1.0f;
    private float Distance_from_player;
    private player player_script;
    private GameObject player;
    private Rigidbody2D rb;
    public float slow;
    public float fast;
    private float timer = 0;
    public GameObject Bomb;
    private GameObject curBomb;
    public GameObject Drop;
    private GameObject curDrop;
    private Vector2 Origin_Position;
	// Use this for initialization
	void Start () 
	{
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
        rb = GetComponent<Rigidbody2D>();
        Origin_Position = transform.position;
        slow = (slow >= 0.02f)? slow : 0.02f;
        fast = (slow >= 0.04f)? fast : 0.04f;
	}

	void MovedByWind(float Distance_from_player)
    {
        if (Mathf.Abs(Origin_Position.y - transform.position.y) > 1)
        {
            rb.AddForce(new Vector2 (0, (Origin_Position.y - transform.position.y)) * 0.5f);
        }
        
        float temp = Random.Range (0.0f, 1.0f);
        if ((Distance_from_player > 7.0f) && Mathf.Abs(Origin_Position.x - transform.position.x) > 2)
        {
            rb.AddForce(new Vector2 ((Origin_Position.x - transform.position.x),0) * 0.25f);
        }
        
        // normal flying
        if (Distance_from_player > 7.0f)
        {
            animator_flying.SetBool("Near", false);
            if (temp > 0.5f)
            {
                rb.velocity += Vector2.left * slow;    
            }
            else  
            {
                rb.velocity += Vector2.right * slow;
            }
        }
        else // Player is in range
        {
            // Drop bomb when fly's x ~= player's x
            if(timer >= 3.0f && Mathf.Abs(transform.position.x - player.transform.position.x) < 7.0f)
            {
                animator_flying.SetBool("Near", true);
                GenerateBomb();
                timer = 0f;
            }

            // When fly's y ~= player's y, fly higher
            if (transform.position.y - player.transform.position.y < 2.0f)
            {
                rb.AddForce(new Vector2 (0, (transform.position.y - player.transform.position.y)) * 0.5f);
            }

            // 80% chance fly to player, 20% fly away
            if (temp > 0.8f)
            {
                rb.velocity += new Vector2 (transform.position.x - player.transform.position.x, 0).normalized * fast;    
            }
            else  
            {
                rb.velocity -= new Vector2 (transform.position.x - player.transform.position.x, 0).normalized * fast;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

    }

	//attack
    void GenerateBomb()
    {
        curBomb = Instantiate(Bomb, transform.position + new Vector3 (0.0f, - 0.5f, 0.0f), Quaternion.identity);
        Rigidbody2D curBomb_rb = curBomb.GetComponent<Rigidbody2D>();
        curBomb_rb.angularVelocity = 0f;
        curBomb_rb.velocity += (AttackMode == 0)? new Vector2 ((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y)).normalized * new Vector2 ((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y)).magnitude : new Vector2(0,0) ;   
    }
	// Update is called once per frame
	void FixedUpdate() 
	{
        if (rb.velocity.magnitude > 4.0f)
        {
            rb.velocity = rb.velocity - rb.velocity.normalized; 
        }

        timer += Time.deltaTime;
        Distance_from_player = (transform.position - player.transform.position).magnitude;
        MovedByWind(Distance_from_player);
	}

    /*void FlyingDies(){
        curDrop = Instantiate(Drop, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.4f);
    }*/
}
