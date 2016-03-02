using UnityEngine;
using System.Collections;

public class MedicineMover : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rot_z = transform.rotation.z;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot_z + 10 * Time.deltaTime));
    }
}
