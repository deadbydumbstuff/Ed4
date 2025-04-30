using UnityEngine;

public class Door_Interaction_Door : MonoBehaviour, Interactable
{
    public Door_Interaction mamager;
    MaterialPropertyBlock MatProBlk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //make the shader actieate
        GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<InteractionManager>().MaterialProBlk(this.gameObject);
        
    }

    // Update is called once per frame

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
        mamager.Door();
        throw new System.NotImplementedException();
    }
}
