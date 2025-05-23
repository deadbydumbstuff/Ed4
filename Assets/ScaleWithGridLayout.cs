using UnityEngine;
using UnityEngine.UI;

public class ScaleWithGridLayout : MonoBehaviour
{
    public GridLayoutGroup GridLayout;
    [SerializeField] float minscale = 950;
    public Inventory_Page_Manager IPM;
    [SerializeField] bool NotScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateScale()
    {
        //if (NotScale) { return; }
        float childCount = Mathf.Floor(transform.childCount / 3);
        //GridLayout.cellSize.x
        if (GridLayout != null)
        {


            float scale = GridLayout.cellSize.y * childCount;
            if (scale >= minscale)
            {
                RectTransform rt = this.gameObject.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, scale);
            }
        }
        //reset scale to childcount * gridlay scale if grater then 950
        
    }
}
