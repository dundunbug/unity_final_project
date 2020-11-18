using System.Collections;
using UnityEngine;

public class Flying : MonoBehaviour
{
	public float windForce = 1.0f;
    private float Distance_from_player;
    private player player_script;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 Origin_Position;
	// Use this for initialization
	void Start () 
	{
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
        rb = GetComponent<Rigidbody2D>();
        Origin_Position = transform.position;
	}

	void MovedByWind(float Distance_from_player)
    {
        float temp = Random.Range (0.0f, 1.0f);
        if (Mathf.Abs(Origin_Position.x - transform.position.x) > 2)
        {
            rb.AddForce(new Vector2 ((Origin_Position.x - transform.position.x),0) * windForce);
        }
        if (Distance_from_player > 7.0f)
        {
            
            if (temp > 0.5f)
            {
                if (temp > 0.75f && (transform.position.y - Origin_Position.y) < -0.25f)
                {
                    rb.AddForce((Vector2.right + new Vector2(0.0f, 1f)) * windForce);    
                }
                else
                {
                    rb.AddForce(Vector2.right * windForce);
                }
            }
            else  
            {
                rb.AddForce(-1 * Vector2.right * windForce);
            }
        }
        else
        {
            if (temp > 0.7f)
            {
                rb.AddForce(new Vector2((transform.position.x - player.transform.position.x),0f) * windForce);
            }
            else  
            {
                rb.AddForce(new Vector2(-0.5f * (transform.position.x - player.transform.position.x),0f) * windForce);
            }
        }
        
	}

	//void OnCollisionEnter2D(Collision2D hit)
	//{

	//}
	
	// Update is called once per frame
	void Update () 
	{
        Distance_from_player = (transform.position - player.transform.position).magnitude;
        MovedByWind(Distance_from_player);
	}
}
