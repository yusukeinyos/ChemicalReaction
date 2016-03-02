using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour
{
    public GameObject playerParticle;
    public List<string> childName = new List<string>();
    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null)
        {
            if (col.CompareTag("Molecule"))
            {
                MoleculeCheck(col);
                if (col.gameObject.transform.childCount > 0)
                {
                    DeleteAllChildObjects(col.gameObject);
                }
                if (col.gameObject.transform.parent.tag == "Molecule")
                {
                    DeleteAllChildObjects(col.gameObject.transform.parent.gameObject);
                }
            }
        }

        //爆発効果

    }

    void MoleculeCheck(Collider2D col)
    {
        childName.Clear();
        int cCount = 0;
        int hCount = 0;
        int oCount = 0;
        //List<string> childName = new List<string>();
        if (col.gameObject.transform.parent != null)
        {
            if (col.gameObject.transform.parent.tag == "Molecule") //自分が子のとき
            {
                GameObject parentGameObject = col.gameObject.transform.parent.gameObject;
                int childCount = parentGameObject.transform.childCount;

                foreach (Transform child in parentGameObject.transform)
                {
                    childName.Add(child.name.Substring(0, 1)); //子の名前を全取得
                }
                childName.Add(parentGameObject.name.Substring(0, 1));
            }
            if (col.gameObject.transform.childCount > 0) //自分が親の時
            {
                GameObject parentGameObject = col.gameObject;
                foreach (Transform child in parentGameObject.transform)
                {
                    childName.Add(child.name.Substring(0, 1)); //子の名前を全取得
                }
                childName.Add(parentGameObject.name.Substring(0, 1));
            }
        }

        for (int i = 0; i < childName.Count; i++)
        {
            switch (childName[i])
            {
                case "C":
                    cCount++;
                    break;
                case "H":
                    hCount++;
                    break;
                case "O":
                    oCount++;
                    break;
            }
        }
        string gotMoleculeName = GetMoleculeName(cCount, hCount, oCount);
        Debug.Log(GetMoleculeName(cCount, hCount, oCount));
        GameObject.FindWithTag("GameController").SendMessage("CheckMoleculeName", gotMoleculeName);
    }

    string GetMoleculeName(int cCount, int hCount, int oCount)
    {
        string name = "";
        if (cCount == 0 && hCount == 2 && oCount == 0)
            name = "水素";
        else if (cCount == 0 && hCount == 0 && oCount == 2)
            name = "酸素";
        else if (cCount == 0 && hCount == 2 && oCount == 1)
            name = "水";
        else if (cCount == 1 && hCount == 0 && oCount == 2)
            name = "二酸化炭素";
        else if (cCount == 1 && hCount == 4 && oCount == 0)
            name = "メタン";
        return name;
    }

    public void DeleteAllChildObjects(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(parent);
    }



}
