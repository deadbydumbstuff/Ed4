using UnityEngine;
public class Door_Interaction_Door : MonoBehaviour, Interactable
{
    MaterialPropertyBlock MatProBlk;
    public Transform doorExit;
    [SerializeField] Door_Interaction_Door Exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //make the shader actieate
        MatProBlk = GameObject.FindGameObjectWithTag("GlobalManager").GetComponent<InteractionManager>().MaterialProBlk(this.gameObject);
        
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
        //mamager.Door(this.gameObject,GameObject.FindGameObjectWithTag("Player").transform); // add playuer
        Playerdoor(GameObject.FindGameObjectWithTag("Player").transform);
        //throw new System.NotImplementedException();
    }


    void Playerdoor(UnityEngine.Transform transformer)
    {
        Vector3 pos = Exit.GetComponent<Door_Interaction_Door>().doorExit.position;
        transformer.position = pos;
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.transform.position = new(pos.x, pos.y, camera.position.z);
   }

    void NPC_Door()
    {
        //for when a player goes through door
    }
}
