using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 temp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        temp = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            temp.Set(
                player.transform.position.x + 2,
                player.transform.position.y + 3,
                player.transform.position.z
                );
        }
        transform.position = temp;
    }
}
