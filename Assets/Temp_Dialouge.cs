using UnityEngine;
using TMPro;
using UnityEngine.UI;


public interface OnDiaougeEnd
{
    void end();
}
public class Temp_Dialouge : MonoBehaviour
{
    public static Temp_Dialouge instance;
    OnDiaougeEnd ODE;

    public GameObject DiaologeObj;
    public TMP_Text textBox;
    public Image Icon;

    private int i;//iteraction of the loop of dialouge we are in
    public string[] currentDialougeSection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class Text
    {
        public Sprite Image;
        public string[] messages;
    }


    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (Input.GetKeyDown(Player_Core.instance.AdvanceDialouge))
        {
            if (currentDialougeSection != null && i < currentDialougeSection.Length)
            {
                textBox.text = currentDialougeSection[i];
                i++;
            }
            else
            {
                currentDialougeSection = null;
                DiaologeObj.SetActive(false);
                if (ODE != null)
                {
                    ODE.end();
                }
            }
        }
    }

    public void NewDialouge(Text text, OnDiaougeEnd EndEvent)
    {
        i = 1;
        Icon.sprite = text.Image;
        ODE = EndEvent;
        currentDialougeSection = text.messages;
        textBox.text = currentDialougeSection[0];
        DiaologeObj.SetActive(true);
    }
    
}
