using UnityEngine;
using System.Collections;

public class AtomController : MonoBehaviour {

	public int maxAtomsCount;
	public GameObject[] atoms;
	public Transform spawn; //原子発生点

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount < maxAtomsCount) {
			int flag = Random.Range(0,atoms.Length);
			var newAtom = Instantiate(atoms[flag],spawn.position,Quaternion.identity) as GameObject;
			newAtom.transform.parent = transform;
		}
	}
}
