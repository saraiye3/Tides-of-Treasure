using TMPro;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Grid grid;
    public GameObject screenParent;
    public TMP_Text hammer_text;

    private void Start()
    {
        screenParent.SetActive(false);
    }

    public void ShowHammer()
    {
        screenParent.SetActive(true);
    }

    public void OnHammerClicked()
    {
        screenParent.SetActive(false);
        grid.EnableHammerMode();
    }

    public void OnPassClicked()
    {
        screenParent.SetActive(false);   
    }
}
