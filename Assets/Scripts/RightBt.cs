using UnityEngine;
using UnityEngine.EventSystems;

public class RightBt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject player;
    private Player player_class;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_class = player.GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player_class.OnRightButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player_class.OnButtonUp();
    }
}