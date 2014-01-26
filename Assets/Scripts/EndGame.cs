using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    public Vector3 startPoint;
    public Vector3 endPoint;

    public float scale;

    public float timeToTake;
    float time;
	GameObject scrollContainer;

    bool shouldScroll;
    public float speed;

    public void PopulateEndView()
    {
        scrollContainer = new GameObject();

        EventShower[] eventViews = FindObjectsOfType<EventShower>();

        for (int i = 0; i < eventViews.Length; ++i)
        {
            EventShower controller = eventViews[i];
            controller.transform.parent = scrollContainer.transform;
            controller.transform.localPosition = startPoint + (-i * Vector3.up * scale);
        }

        //scrollContainer.transform.Translate(Vector3.left * 100.0f);
        Camera.main.transform.Translate(Vector3.left * 200.0f);
        time = 0.0f;

        shouldScroll = true;
    }

	// Use this for initialization
	void Start () {

        shouldScroll = false;
            
         
	}
	
	// Update is called once per frame
	void Update () {
        if (shouldScroll)
        {
			if(scrollContainer != null)
			{
                if (time <= timeToTake)
                {
                    scrollContainer.transform.position = Vector3.Lerp(startPoint, endPoint, time / timeToTake);
                    time += Time.deltaTime;
                }
                else
                {
					if (Input.GetKey (KeyCode.Escape))
					{
						Application.LoadLevel("FrontEnd");
					}
					else
					{
	                    scrollContainer.transform.Translate(0.0f, Input.GetAxis("Vertical") * speed * -1.0f, 0.0f);
	                    if (scrollContainer.transform.position.y > endPoint.y)
	                    {
	                        scrollContainer.transform.position = new Vector3(scrollContainer.transform.position.x, endPoint.y, scrollContainer.transform.position.z);
	                    }
	                    else if (scrollContainer.transform.position.y < startPoint.y)
	                    {
	                        scrollContainer.transform.position = new Vector3(scrollContainer.transform.position.x, startPoint.y, scrollContainer.transform.position.z);
	                    }
					}
                    /*
                    float axisValue = Input.GetAxis("Vertical");
                    print("Axis: " + axisValue);
                    if (axisValue > 0.0f)
                    {
                        scrollContainer.transform.position = new Vector3(scrollContainer.transform.position.x, 
                            Mathf.Clamp(scrollContainer.transform.position.y + Input.GetAxis("Vertical") * speed, startPoint.y, endPoint.y),
                            scrollContainer.transform.position.z);
                    }
                    else if(axisValue < 0.0f)
                    {
                        Vector3 newVector = new Vector3(scrollContainer.transform.position.x,
                            Mathf.Clamp(scrollContainer.transform.position.y - Input.GetAxis("Vertical") * speed, startPoint.y, endPoint.y),
                            scrollContainer.transform.position.z);

                        print(newVector);
                        scrollContainer.transform.position = newVector;
                    }*/
                }
                    

			}
        }
	}
}
