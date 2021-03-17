using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    public GameObject bloodDrop;

    public int amount;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            splash();
        }
    }

    private Vector2 get_direction()
    {
        float randomx = Random.Range(10, 50);
        float randomy = Random.Range(10, 50);
        return new Vector2(randomx, randomy);
    }

    private void splash()
    {
        
        for (int i = 0; i < amount; i++)
        {
            bloodDrop.transform.position = transform.position;
            Vector2 thing = get_direction();
            Debug.Log(thing);
            bloodDrop.GetComponent<Rigidbody2D>().velocity = get_direction();
            Instantiate(bloodDrop);
        }
    }
}
