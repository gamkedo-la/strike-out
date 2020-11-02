using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public GameObject Batter;

    public bool m1, m2, m3, m4, m5, m6, m7, m8;
    public bool M1, M2, M3, M4, M5, M6, M7, M8;
    public bool e1, e2, e3, e4, e5, e6, e7, e8;

    public int price;
    public Text buttonText;

    public void Start()
    {
        if (m1 && GameManager.m1)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m2 && GameManager.m2)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m3 && GameManager.m3)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m4 && GameManager.m4)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m5 && GameManager.m5)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m6 && GameManager.m6)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m7 && GameManager.m7)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (m8 && GameManager.m8)
        {
            this.GetComponent<Button>().interactable = false;
        }

        if (M1 && GameManager.M1)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M2 && GameManager.M2)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M3 && GameManager.M3)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M4 && GameManager.M4)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M5 && GameManager.M5)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M6 && GameManager.M6)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M7 && GameManager.M7)
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (M8 && GameManager.M8)
        {
            this.GetComponent<Button>().interactable = false;
        }

    }
    public void Enter()
    {
        Batter.SetActive(true);
    }

    public void Exit()
    {
        Batter.SetActive(false);
    }

    public void Purchase()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;

            if (m1)
            {
                GameManager.m1 = true;
                GameManager.m1v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m2)
            {
                GameManager.m2 = true;
                GameManager.m2v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m3)
            {
                GameManager.m3 = true;
                GameManager.m3v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m4)
            {
                GameManager.m4 = true;
                GameManager.m4v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m5)
            {
                GameManager.m5 = true;
                GameManager.m5v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m6)
            {
                GameManager.m6 = true;
                GameManager.m6v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m7)
            {
                GameManager.m7 = true;
                GameManager.m7v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (m8)
            {
                GameManager.m8 = true;
                GameManager.m8v = 1;
                this.GetComponent<Button>().interactable = false;
            }

            if (M1)
            {
                GameManager.M1 = true;
                GameManager.M1v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M2)
            {
                GameManager.M2 = true;
                GameManager.M2v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M3)
            {
                GameManager.M3 = true;
                GameManager.M3v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M4)
            {
                GameManager.M4 = true;
                GameManager.M4v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M5)
            {
                GameManager.M5 = true;
                GameManager.M5v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M6)
            {
                GameManager.M6 = true;
                GameManager.M6v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M7)
            {
                GameManager.M7 = true;
                GameManager.M7v = 1;
                this.GetComponent<Button>().interactable = false;
            }
            if (M8)
            {
                GameManager.M8 = true;
                GameManager.M8v = 1;
                this.GetComponent<Button>().interactable = false;
            }

            if (e1)
            { GameManager.e1 = true; }
            if (e2)
            { GameManager.e2 = true; }
            if (e3)
            { GameManager.e3 = true; }
            if (e4)
            { GameManager.e4 = true; }
            if (e5)
            { GameManager.e5 = true; }
            if (e6)
            { GameManager.e6 = true; }
            if (e7)
            { GameManager.e7 = true; }
            if (e8)
            { GameManager.e8 = true; }

            if (buttonText)
                buttonText.text = "Purchased!";

            this.GetComponent<Button>().interactable = false;
        }
    }
}
