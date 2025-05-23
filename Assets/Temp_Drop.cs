using UnityEngine;

public class Temp_Drop : MonoBehaviour
{
    public bool Drop;
    float i = 0;
    public RectTransform lastpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        if (Drop)
        {
            i += Time.deltaTime;
            if (i > 10)
            {
                transform.position = Vector3.Lerp(transform.position, lastpos.transform.position, (i - 10)/6);
            }
        }
    }
}
