using UnityEngine;

public class ForwardRayDrawer : MonoBehaviour
{
    [SerializeField] private float _length = 1;
    [SerializeField] private Color _color = Color.yellow;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * _length, _color);
        
    }
}
