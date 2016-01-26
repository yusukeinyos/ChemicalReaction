using UnityEngine;
using System.Collections;

public enum Direction{Top,Down,Right,Left};

public class AtomMover : MonoBehaviour {
	public Direction firstDirection;
	public float speed;
	//public float maxChangeTime; //方向を変えるmax時間
	public Transform gravityPointA;
	//public Transform gravityPointB;
	public GameObject burstParticle;

	private float changeTime;
	private Vector2 startPosition;
	private Vector2 direction;
	private Vector2[] connectionPoints;
	public bool[] isConnected;
	private int numConnectable;
	public int connectionCount; //結合数
	private float beforeTime;
	private string atomName;
	private GameObject rayHitGameObject; //rayが当たったobject

	private bool isDrag;
	Rigidbody2D rig;

	//[SerializeField]
	//AnimationCurve curve;

	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody2D> ();

		startPosition = transform.position;

		switch (firstDirection) {
		case Direction.Down:
			direction = Vector2.down; break;
		case Direction.Left:
			direction = Vector2.left; break;
		case Direction.Right:
			direction = Vector2.right; break;
		}

		atomName=gameObject.name;

		isConnected = new bool[] {false,false,false,false};
		connectionCount = 0;

		connectionPoints=new Vector2[4];

			
		numConnectable=-1;
		isDrag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.tag!="Atom"&&transform.parent.tag!="Molecule") { //親がいなければ
			if (!isDrag) {
				Vector2 vecA = gravityPointA.position - transform.position;
				float distanceA = Vector2.SqrMagnitude (vecA);
				if (rig.velocity.sqrMagnitude > 3)
					rig.AddForce (speed*vecA / 30);
				else
					rig.AddForce (speed*vecA / 20);
			}
		}
		CheckConnection ();
		CheckMolecule ();
	}

	void OnMouseDrag()
	{
		isDrag = true;
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));

		Vector2 centerPoint = new Vector2 (transform.position.x, transform.position.y);
		connectionPoints[0]=new Vector2(0,1)+centerPoint;
		connectionPoints[1]=new Vector2(0,-1)+centerPoint;
		connectionPoints[2]=new Vector2(1,0)+centerPoint;
		connectionPoints[3]=new Vector2(-1,0)+centerPoint;

		MakeConnection ();
	}

	void OnMouseUp()
	{
		isDrag = false;
		if (rayHitGameObject != null && numConnectable!=-1) {
			AtomMover am = rayHitGameObject.GetComponent<AtomMover>();
			if(rayHitGameObject.transform.parent.gameObject.tag!="Atom") {
				if(am.numConnectable<4) {
					rayHitGameObject.transform.parent=transform;
					Destroy(rayHitGameObject.GetComponent<Rigidbody2D>());

					Vector2 connectDirection=setDirection(numConnectable);
					rayHitGameObject.transform.position=transform.position+1.5f*new Vector3(connectDirection.x,connectDirection.y,0);
					rayHitGameObject.transform.rotation=transform.rotation;
					Instantiate(burstParticle,transform.position+0.75f*new Vector3(connectDirection.x,connectDirection.y,-10.2f),Quaternion.identity);
					connectionCount++;
					rayHitGameObject=null;
					isConnected[numConnectable]=true;

					am.setConnection(numConnectable);
					numConnectable=-1;
				}
			}


		}
		//Destroy (gameObject);
	}

	private void MakeConnection()
	{
		RaycastHit2D hit;
		rayHitGameObject = null;
		numConnectable = -1;
		for (int i=0; i<4; i++) {
			if(isConnected[i]!=true) {//まだ結合がなければ
				Vector3 shotDirection=Vector3.zero;
				switch(i)
				{
				case 0: shotDirection=transform.up; break;
				case 1: shotDirection=-transform.up; break;
				case 2: shotDirection=transform.right; break;
				case 3: shotDirection=-transform.right; break;
				}
				hit = Physics2D.Raycast(connectionPoints[i],shotDirection,0.2f,1<<LayerMask.NameToLayer("Atom"));
				if(hit.collider!=null) {
					if(hit.collider.CompareTag("Atom"))
					{
						rayHitGameObject=hit.collider.gameObject;
						numConnectable=i;
						Debug.Log("Connectable:"+numConnectable+rayHitGameObject.name);
					}


				}
			}
		}
		Debug.Log (transform.up);

	}

	private void CheckMolecule()
	{
		if (transform.childCount > 0) {
			rig.gravityScale = 0.05f;
			gameObject.tag="Molecule";
			gameObject.layer=11; //Molecule
			gameObject.GetComponent<CircleCollider2D>().isTrigger=true;
		}
		if (transform.parent.tag == "Molecule" || transform.parent.tag == "Atom") {
			gameObject.tag = "Molecule";
			gameObject.layer = 11; //Molecule
		}
	}

	private void setConnection(int num)
	{
		switch (num) {
		case 0: isConnected[1]=true; break;
		case 1: isConnected[0]=true; break;
		case 2: isConnected[3]=true; break;
		case 3: isConnected[2]=true; break;
		}
	}

	private void CheckConnection()
	{
		switch (atomName) {
		case "C":
			break;
		case "H":
			if(connectionCount==1)
			{
				for(int i=0;i<4;i++)
					isConnected[i]=true;
				connectionCount=4;
			}
			break;
		case "O":
			if(connectionCount==2)
			{
				for(int i=0;i<4;i++)
					isConnected[i]=true;
				connectionCount=4;
			}
			break;

		}
	}

	public Vector2 setDirection(int num)
	{
		Vector2 direction = Vector2.zero;
		Quaternion rotation = transform.rotation;
		Debug.Log (rotation.z);
		switch(num)
		{
		case 0: direction=transform.up; break;
		case 1: direction=-transform.up; break;
		case 2: direction=transform.right; break;
		case 3: direction=-transform.right; break;
		} 
		return direction;
	}

	public bool isDragging()
	{
		return isDrag;
	}
}
