using UnityEngine;
using UnityEngine.UI;

public class Camera_Effects : MonoBehaviour
{
    [Header("Fadeout")]
    [SerializeField] Image Image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FadeOut(float duration,int Mode)
    {
        //cover the screen in a 
        switch (Mode)
        {
            case 0://sine fade
                Image.material.SetFloat("Time",0);
                break;
            case 1://swipe fade

                break;
            case 2://spiral fade?

                break;
        }

    }
}
