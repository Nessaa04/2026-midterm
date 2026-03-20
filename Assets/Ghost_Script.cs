using UnityEngine;

public class Ghost_Script : MonoBehaviour
{ 
    public float Speed = 5;

    public PlayerScript P;

    Rigidbody2D RB;

    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //trying to code it to follow Player
        if (P.transform.position.x > transform.position.x);
         {
            Vector3 pos = transform.position;
            pos.x = transform.position.x;
            transform.position = pos;
         }
        if (P.transform.position.x < transform.position.x);
         {
            Vector3 pos = transform.position;
            pos.x = transform.position.x;
            transform.position = pos;
         }
    }
}
