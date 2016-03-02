using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private Slider sliderGUI;
    private float hp;

    void Start()
    {
        sliderGUI = GetComponent<Slider>();
        hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //スライダの色変え

        if (hp <= 0)
        {
            GameObject.FindWithTag("GameController").SendMessage("GameOver");
        }
    }

    public void AddHP(float deltaHP)
    {
        if (hp > 0)
        {
            hp += deltaHP;
            sliderGUI.value = hp;
        }

    }
}
