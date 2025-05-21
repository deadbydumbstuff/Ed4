using UnityEngine;

public class Interactable_Bed : MonoBehaviour,Interactable
{
    MaterialPropertyBlock MatProBlk;
    public void EnterRange()
    {
        MatProBlk.SetFloat("toggle", 1);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void ExitRange()
    {
        MatProBlk.SetFloat("toggle", 0);
        GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void Interact()
    {
        Time_Mananger.instance.DayEnd(); //force day end
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

}
