using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PatientRecordController : MonoBehaviour
{
    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private LevelDefs levelToLoad;
    public GameEnums.PatientEnum patient;
    private Animator _animator;
    private PatientRecordUIController recordController;

    //Start
    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        recordController = GetComponentInParent<PatientRecordUIController>();
    }

    //Request Load Scene
    public void RequestLoadScene()
    {
        //PlayLoadingVideo();
        recordController.LoadScene(levelToLoad);
    }

    /*private void PlayLoadingVideo()
    {
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        // This will cause our Scene to be visible through the video being played.
        videoPlayer.targetCameraAlpha = 0.5F;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.clip = _loadingVideo;

        // Skip the first 100 frames.
        videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Play();
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }*/

    //OnEnable
    private void OnEnable()
    {
        _animator.SetBool("Opened", true);
    }

    //Close Patient Record
    public void RequestClosePatientRecord()
    {
        _animator.SetBool("Opened", false);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(false);
    }
}
