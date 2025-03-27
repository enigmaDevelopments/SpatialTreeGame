using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject zombie;
    public Transform player;
    public QuadTree tree;
    public float spawnRate = .01f;
    void FixedUpdate()
    {
        if (Random.value < spawnRate)
        {
            Vector3 pos;
            do
            {
               pos = new Vector3(Random.Range(-tree.size, tree.size), Random.Range(-tree.size, tree.size), -.4f);
            } while (Vector3.Distance(pos, player.position) < 3);
            GameObject zombieInstance = Instantiate(zombie, pos, Quaternion.identity);
            zombieInstance.GetComponent<FollowPlayer>().target = player;
            zombieInstance.GetComponent<TreeInterface>().tree = tree;
            tree.Insert(zombieInstance.transform);
        }
    }
}
