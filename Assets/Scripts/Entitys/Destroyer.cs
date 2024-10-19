using Interface;
using UnityEngine;

namespace Entitys
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<IDestroyable>()?.DestroyObject();
        }
    }
}
