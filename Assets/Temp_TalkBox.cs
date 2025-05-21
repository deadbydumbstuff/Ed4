using UnityEngine;

public class Temp_TalkBox : MonoBehaviour, Interactable
{
    MaterialPropertyBlock MatProBlk;

    public Temp_Dialouge.Text text;
    public void EnterRange()
    {
        MatProBlk.SetFloat("toggle", 1);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }  public void ExitRange()
    {
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void Interact()
    {
        Temp_Dialouge.instance.NewDialouge(text,null);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MatProBlk = new MaterialPropertyBlock();
        GetComponent<SpriteRenderer>().GetPropertyBlock(MatProBlk);
        MatProBlk.SetTexture("main", GetComponent<SpriteRenderer>().sprite.texture);
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    // Update is called once per frame
}
