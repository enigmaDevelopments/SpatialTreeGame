using TreeEditor;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject zombie;
    public Transform player;
    public float spawnRate = .01f;
    void Update()
    {
        if (Random.value < spawnRate)
        {
            Vector3 pos;
            do
            {
               pos = new Vector3(Random.Range(-64, 64), Random.Range(-64, 64), -.4f);
            } while (Vector3.Distance(pos, player.position) < 3);
            GameObject zombieInstance = Instantiate(zombie, pos, Quaternion.identity);
            zombieInstance.GetComponent<FollowPlayer>().target = player;
        }
    }
}
