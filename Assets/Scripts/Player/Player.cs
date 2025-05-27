using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRB;
    public Vector2 velocity;
    public float speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // myRB.MovePosition(myRB.position - velocity * Time.deltaTime);
            myRB.velocity = new Vector2(-speed, myRB.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRB.MovePosition(myRB.position + velocity * Time.deltaTime);
            myRB.velocity = new Vector2(speed, myRB.velocity.y);
        }
    }
}
