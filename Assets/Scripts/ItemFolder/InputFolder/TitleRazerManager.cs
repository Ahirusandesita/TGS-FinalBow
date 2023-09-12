// --------------------------------------------------------- 
// TitleRazerManager.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using System;

interface IFTakeRayCastTitle
{
    void TakeRay();

    void SelectingRay();

    void SelectedRay();
}
public class TitleRazerManager : MonoBehaviour
{
    [SerializeField] Transform leftRazerStartPosition = default;
    [SerializeField] Transform rightRazerStartPosition = default;

    [SerializeField] Transform leftPointer = default;
    [SerializeField] Transform rightPointer = default;

    [SerializeField] LineRenderer leftRenderer = default;
    [SerializeField] LineRenderer rightRenderer = default;

    [SerializeField] float maxLineLength = 100f;

    InputManagement input = default;

    DirectionParts left;
    DirectionParts right;

    const int START_INDEX = 0;

    struct DirectionParts
    {
        public Transform start;
        public Transform pointer;
        public LineRenderer renderer;
        public RaycastHit hit;
        public Func<bool>buttonPushing;
        public IFTakeRayCastTitle cache;
        public bool selecting;
        public DirectionParts(Transform startPosition,Transform pointer,LineRenderer renderer,Func<bool>input)
        {
            this.start = startPosition;
            this.pointer = pointer;
            this.renderer = renderer;
            hit = default;
            buttonPushing = input;
            cache = default;
            selecting = false;
        }

        public Vector3 GetStartPosition()
        {
            return start.position;
        }

        public Vector3 GetPointPosition()
        {
            return pointer.position;
        }
    }

    private void Awake()
    {
        input = GetComponent<InputManagement>();
        leftRenderer.SetPosition(START_INDEX, left.GetStartPosition());
        rightRenderer.SetPosition(START_INDEX, right.GetStartPosition());

        left = new DirectionParts(leftRazerStartPosition, leftPointer, leftRenderer,input.ButtonDownLeftUpTrigger);
        right = new DirectionParts(rightRazerStartPosition, rightPointer, rightRenderer,input.ButtonRightUpTrigger);
    }
    private void Update()
    {
        RayFindObject(left);
        RayFindObject(right);

        SetRayPoint(left);
        SetRayPoint(right);

        

    }

    private void RayFindObject(DirectionParts parts)
    {
        if (Physics.Raycast(parts.GetStartPosition(), parts.start.forward, out parts.hit, maxLineLength))
        {
            parts.cache = parts.hit.collider.GetComponent<IFTakeRayCastTitle>();
            parts.cache.TakeRay();

            // ‚±‚±‚ÉƒŒƒtƒg

        }
        else
        {
            parts.cache = default;
        }
    }

    private void SetRayPoint(DirectionParts parts)
    {
        const int END_INDEX = 1;
        parts.renderer.SetPosition(END_INDEX, parts.hit.point);
        parts.pointer.position = parts.hit.point;
    }
    
       
    
}