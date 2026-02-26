using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance { get; private set; }

    public PlayableDirector director;
    public TimelineAsset introTimeline;
    public TimelineAsset startGameTimeline;

    public Transform player;
    public Vector3 teleportPosition;

    //public PlayerController playerController; // Oyuncunun hareketini kontrol eden script

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // Karakter kontrolü kapalý baþlar
        //playerController.enabled = false;

        // Ýlk Timeline'ý baþlat
        PlayTimeline(introTimeline);
    }

    public void PlayTimeline(TimelineAsset timeline)
    {
        if (timeline == null) return;

        director.playableAsset = timeline;
        director.Play();
    }

    public void OnIntroFinished()
    {
        // Notepad alma zamaný (bir event üzerinden çaðrýlabilir)
        PlayTimeline(startGameTimeline);
    }


    public void OnNotepadFinished()
    {
        //playerController.enabled = true; // Artýk karakter kontrolü açýlýr
    }

    public void TeleportPlayer()
    {
        var controller = player.GetComponent<CharacterController>();
        var playerScript = player.GetComponent<playerController>();
        if (controller != null)
        {
            controller.enabled = false;
            playerScript.enabled = false;
            player.position = teleportPosition;
            controller.enabled = true;
            playerScript.enabled = true;

        }
        else
        {
            player.position = teleportPosition;
        }
    }
}
