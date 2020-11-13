using UnityEngine;
using UnityEngine.UI;

namespace AVDTetris
{
    public class Canvas : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Image[] imgs = GetComponentsInChildren<Image>();
            foreach (var item in imgs)
            {
                item.enabled = true;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
