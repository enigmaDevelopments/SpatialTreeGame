using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject zombie;
    public Transform player;
    public Transform Camera;
    public float spawnRate = .01f;
    public float playerDistance = 3;
    void FixedUpdate()
    {
        Spwan();
        Spwan();
        Spwan();
        Spwan();
    }
    private void Spwan()
    {
        if (Random.value < spawnRate)
        {
            Vector3 pos;
            do
            {
                pos = new Vector3(Random.Range(-QuadTree.size, QuadTree.size), Random.Range(-QuadTree.size, QuadTree.size), -.4f);
            } while (Vector3.Distance(pos, Camera.position) < playerDistance);
            GameObject zombieInstance = Instantiate(zombie, pos, Quaternion.identity);
            zombieInstance.GetComponent<FollowPlayer>().target = player;
            QuadTree.Insert(zombieInstance.transform);
        }
    }
}
