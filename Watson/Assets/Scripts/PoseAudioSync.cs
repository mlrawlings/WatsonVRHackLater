using UnityEngine;
using System.Collections;

public class PoseAudioSync : MonoBehaviour {
       
    AudioSource audioSource;
    GameObject playerOne;
    Vector3 initialPosition = new Vector3(0f, 1.5f, 5.51f);
    Vector3 distanceOffset = new Vector3(0f, 0f, .5f);
   // float[] groupTimes = new float[]  {45.66f, 49.66f, 60.66f};
    bool isReady = false;
    //float targetTime;
    private float _timeTarget;
    public float timeTarget
    {
        get
        {
            return _timeTarget;
        }set
        {
            this._timeTarget = value;
            isReady = true;
        }
    }

    public SpriteRenderer childSprite;
            

    // Use this for initialization
    void Start () {
             
	}
	
	// Update is called once per frame
	void Update () {
        if (isReady)
        {
            float songTime = audioSource.time;            
            float timeTilCollide = timeTarget - songTime;
            if(timeTilCollide > 0)
            {
                float stepPercentage = Time.deltaTime / timeTilCollide;
                Vector3 distance = playerOne.transform.position - gameObject.transform.position + distanceOffset;
                Vector3 stepDistance = distance * stepPercentage;
                stepDistance.y = 0;
                gameObject.transform.position = gameObject.transform.position + stepDistance;
                Color spriteOpa = childSprite.color;
                spriteOpa.a = 2.0f * stepPercentage;
                childSprite.color = spriteOpa;
            }
            else
            {
                reset();
            }
            
        }
    }
    
    public void reset()
    {
        isReady = false;
        gameObject.transform.position = initialPosition;
        Debug.Log("Reset "+this.name);
    }

    public void setResources(Sprite cueSprite)
    {
        audioSource = GameObject.Find("AudioMainSource").GetComponent<AudioSource>();
        playerOne = GameObject.Find("[EnfluxVRHumanoid]");
        childSprite.sprite = cueSprite;
        reset();       
    }

    //public void setTartgetTime(float timeTarget)
    //{
    //    this.timeTarget = timeTarget;
    //    isReady = true;
    //}
}
