using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretButtons : MonoBehaviour
{
    public TurretDataContainer turret;

    private EventTrigger eventTrigger;


    [SerializeField] private Image turretDragSprite;
    [SerializeField] private Image spriteBg;
    [SerializeField] private Image turretPlaceArea;
    [SerializeField] private Image turretPlaceInner;
    [SerializeField] private Image turretTargetArea;

    private Camera cam;

    private bool isDragging;

    [SerializeField] private LayerMask layerMask;

    private bool canPlace;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        EventTrigger.Entry dragEnter = new EventTrigger.Entry();
        EventTrigger.Entry drag = new EventTrigger.Entry();
        EventTrigger.Entry dragExit = new EventTrigger.Entry();

        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerExit.eventID = EventTriggerType.PointerExit;
        dragEnter.eventID = EventTriggerType.BeginDrag;
        drag.eventID = EventTriggerType.Drag;
        dragExit.eventID = EventTriggerType.EndDrag;

        pointerEnter.callback.AddListener(delegate { PointerEnter(); });
        pointerExit.callback.AddListener(delegate { PointerExit(); });
        dragEnter.callback.AddListener(delegate { DragEnter(); });
        drag.callback.AddListener(delegate { Drag(); });
        dragExit.callback.AddListener(delegate { DragExit(); });

        eventTrigger.triggers.Add(pointerEnter);
        eventTrigger.triggers.Add(pointerExit);
        eventTrigger.triggers.Add(dragEnter);
        eventTrigger.triggers.Add(drag);
        eventTrigger.triggers.Add(dragExit);
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void PointerEnter()
    {
        spriteBg.color = new Color(0, 1f, 170f / 255f);
    }

    private void PointerExit()
    {
        if (isDragging == false)
        {
            spriteBg.color = new Color(72f / 255f, 72f / 255f, 72f / 255f);
        }
    }

    private void DragEnter()
    {
        isDragging = true;

        spriteBg.color = new Color(72f / 255f, 255f / 255f, 72f / 255f);

        turretDragSprite.sprite = turret.sprite;
        turretDragSprite.gameObject.SetActive(true);

        turretPlaceArea.rectTransform.sizeDelta = new Vector2(120 * turret.placeRadius, 120 * turret.placeRadius);
        turretPlaceArea.gameObject.SetActive(true);

        turretTargetArea.rectTransform.sizeDelta = new Vector2(120 * turret.targetRadius, 120 * turret.targetRadius);
        turretTargetArea.gameObject.SetActive(true);
    }

    private void Drag()
    {
        turretPlaceArea.transform.position = Input.mousePosition;
        turretTargetArea.transform.position = Input.mousePosition;
        turretDragSprite.transform.position = (Vector2)Input.mousePosition + new Vector2(32, 32);

        RaycastHit2D hit = Physics2D.CircleCast(cam.ScreenToWorldPoint(Input.mousePosition), turret.placeRadius, new Vector2(0, 0), 0, layerMask);
        canPlace = hit.transform == null ? true : false;

        if (canPlace)
        {
            turretPlaceArea.color = new Color(0, 1f, 0);
            turretTargetArea.color = new Color(133f / 255f, 1f, 129f / 255f, 150f / 255f);
            turretPlaceInner.color = new Color(0f, 140f / 255f, 0f);
        }

        else
        {
            turretPlaceArea.color = new Color(1f, 0, 0);
            turretTargetArea.color = new Color(1f, 133f/255f, 129f/255f, 150f/255f);
            turretPlaceInner.color = new Color(140f/255f, 0f, 0f);
        }
    }

    private void DragExit()
    {
        isDragging = false;
        spriteBg.color = new Color(72f / 255f, 72f / 255f, 72f / 255f);
        turretDragSprite.gameObject.SetActive(false);
        turretPlaceArea.gameObject.SetActive(false);
        turretTargetArea.gameObject.SetActive(false);

        if (canPlace)
        {
            TurretBehaviour turretBehaviour = Instantiate(turret.turretPrefab, cam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<TurretBehaviour>();
            turretBehaviour.Setup(turret);
        }
    }

    [System.Serializable]
    public struct TurretDataContainer
    {
        [Header("General Data")]
        public Sprite sprite;
        public GameObject turretPrefab;

        [Header("Stats")]
        public float damage;
        public float placeRadius;
        public float targetRadius;
        public float rpm;

    }
}