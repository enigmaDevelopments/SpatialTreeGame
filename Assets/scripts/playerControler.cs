using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour
{
    public Transform blaster;
    public GameObject bullet;
    public QuadTree tree;
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
            Instantiate(bullet, blaster.position, transform.rotation).GetComponent<Bullet>().tree = tree;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = dir;
        if (tree.PointInRadius(transform,.8f) || tree.size < Mathf.Abs(transform.position.x) || tree.size < Mathf.Abs(transform.position.y))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
