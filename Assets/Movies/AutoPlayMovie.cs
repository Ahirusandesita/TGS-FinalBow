// --------------------------------------------------------- 
// AutoPlayMovie.cs 
// 
// CreateDay: 2023/09/18
// Creator  : TakayanagiSora
// --------------------------------------------------------- 

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class AutoPlayMovie : MonoBehaviour
{
    #region variable 
    [SerializeField]
    private VideoPlayer _videoPlayer = default;

    [SerializeField]
    private RawImage _videoDisplay = default;

    [SerializeField, Tooltip("“®‰æ‚ğÄ¶‚·‚é‚Ü‚Å‚Ì•ú’uŠÔ")]
    private float _timeUntilPlayVideo_s = 60f;

    [Tooltip("•ú’u‚µ‚Ä‚¢‚éŠÔ")]
    private float _nonOperationTime = default;

    private MovieDisplayManager _displayManager = default;

    private bool _isPlaying = false;

    private bool _isClosed = false;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {
        _displayManager = _videoDisplay.GetComponent<MovieDisplayManager>();
    }

    private void Start()
    {
        _videoDisplay.enabled = false;
        _videoPlayer.time = 0f;
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Touch.Any) || OVRInput.Get(OVRInput.NearTouch.Any) || OVRInput.Get(OVRInput.RawNearTouch.Any) || OVRInput.Get(OVRInput.RawTouch.Any))
        {
            _nonOperationTime = 0f;

            if (_videoDisplay.enabled && !_isClosed)
            {
                _isClosed = true;
                _isPlaying = false;
                _videoPlayer.Stop();
                _displayManager.CloseFrame();
            }
        }
        else
        {
            if (_isPlaying)
                return;

            _nonOperationTime += Time.deltaTime;

            if (_nonOperationTime >= _timeUntilPlayVideo_s)
            {
                _isPlaying = true;

                _isClosed = false;
                _videoDisplay.enabled = true;

                _videoPlayer.Play();
            }
        }
    }
    #endregion
}