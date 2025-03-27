using UnityEngine;

public class playerControler : MonoBehaviour
{
    public Transform blaster;
    public GameObject bullet;
    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float move = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, move, 0);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, -move, 0);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-move, 0, 0);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(move, 0, 0);
        if (Input.GetMouseButtonDown(0))
            Instantiate(bullet, blaster.position, transform.rotation);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = dir;
    }
}
