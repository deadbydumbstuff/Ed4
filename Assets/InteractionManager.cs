using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //CREATE MATERIAL PROPERTY BLOCK
    public void MaterialProBlk(GameObject obect)
    {
        MaterialPropertyBlock MatProBlk = new();
        MatProBlk.SetTexture("main", GetComponent<SpriteRenderer>().sprite.texture);
        obect.GetComponent<SpriteRenderer>().SetPropertyBlock(MatProBlk);
    }

    public void UpdateMatpro()
    {

    }
}
