using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Jeu : MiniGame
{
    [SerializeField]
    private List<RawImage> _imagesSelected = new List<RawImage>();

    [SerializeField]
    private List<RawImage> _imagesList = new List<RawImage>();

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    Vector3 firstImagePos;
    Vector3 secondImagePos;

    private Canvas _canvas;

    bool _move = false;
    bool _grow = false;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    public override void GameStatue()
    {
        _isStart = !_isStart;
        _canvas.enabled = _isStart;
    }

    [SerializeField] private float _speed = 0.02f;
    public override void ToUpdate()
    {
        if (!_isStart)
        {
            return;
        }
        
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_move)
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned
            foreach (RaycastResult result in results)
            {
                RawImage imageClicked = result.gameObject.GetComponent<RawImage>();
                if (!_imagesSelected.Contains(imageClicked))
                {
                    _imagesSelected.Add(imageClicked);
                    if (_imagesSelected.Count == 2)
                    {
                        firstImagePos = new Vector3(_imagesSelected[0].transform.position.x, _imagesSelected[0].transform.position.y, 0);
                        secondImagePos = new Vector3(_imagesSelected[1].transform.position.x, _imagesSelected[1].transform.position.y, 0);
                        _move = true;
                    }
                }
                else
                {
                    _imagesSelected.Remove(imageClicked);
                }
            }
        }

        if (_move)
        {
            if (_grow)
            {
                _imagesSelected[0].transform.localScale += new Vector3(_speed, _speed, _speed);
                _imagesSelected[1].transform.localScale += new Vector3(_speed, _speed, _speed);
                if (_imagesSelected[0].transform.localScale == Vector3.one || _imagesSelected[1].transform.localScale == Vector3.one)
                {
                    _move = false;
                    _grow = false;
                    _imagesSelected.Clear();
                }
            }
            else
            {
                _imagesSelected[0].transform.localScale -= new Vector3(_speed, _speed, _speed);
                _imagesSelected[1].transform.localScale -= new Vector3(_speed, _speed, _speed);
                if (_imagesSelected[0].transform.localScale == Vector3.zero || _imagesSelected[1].transform.localScale == Vector3.zero)
                {
                    _imagesSelected[0].transform.position = secondImagePos;
                    _imagesSelected[1].transform.position = firstImagePos;

                    int firstImageIndex = _imagesList.IndexOf(_imagesSelected[0]);
                    int secondImageIndex = _imagesList.IndexOf(_imagesSelected[1]);

                    _imagesList[firstImageIndex] = _imagesSelected[1];
                    _imagesList[secondImageIndex] = _imagesSelected[0];

                    _grow = true;
                }
            }
        }
    }

    public override bool Verif()
    {
        if (_move)
        {
            return false;
        }
        
        for (int i = 0; i < _imagesList.Count; i++)
        {
            if (i != _imagesList[i].GetComponent<GameImage>().GetIndex())
            {
                return false;
            }
        }
        return true;
    }
}
