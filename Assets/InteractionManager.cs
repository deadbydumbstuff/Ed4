using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //CREATE MATERIAL PROPERTY BLOCK
    public MaterialPropertyBlock MaterialProBlk(GameObject obect)
    {
        MaterialPropertyBlock MatProBlk = new();
        MatProBlk.SetTexture("main",obect.GetComponent<SpriteRenderer>().sprite.texture);
        obect.GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
        return MatProBlk;
    }

    public void UpdateMatpro()
    {

    }
}
