using UnityEngine;
using System.Collections;

public class BlocksMover : MonoBehaviour
{
    public GameObject newSprite;
    private float deltaY;
    private Vector3 currentPosition;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -Time.deltaTime, 0);
        if (deltaY < 17.74)
        {
            deltaY += Time.deltaTime;
        }
        else
        {
            currentPosition = transform.position;
            deltaY = 0;
            Instantiate(newSprite, new Vector3(currentPosition.x, currentPosition.y + 2 * deltaY, currentPosition.z), Quaternion.identity);
        }
    }


}
