using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject sphere;
    private GameObject[,] spheres;
    private float time=1;
    private bool flag=false;
    // Use this for initialization
    private int w = 8, h = 4;
	void Start () {
        int x = -7, y = 8;
        spheres = new GameObject[h,w];
        //GameObject gameObject = Instantiate(block, new Vector3(x + j * x * -1, y - 2 * i, 0), Quaternion.identity);
        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                spheres[i, j] = Instantiate(sphere, new Vector3(x+j*2, y-i*2, 0.8f), Quaternion.identity);
                spheres[i, j].name = i + ":" + j;
                //spheres[i, j].SetActive(false);
                if ((i + j + 2) % 2 == 0)
                {
                    spheres[i,j].GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }
        DoGenerateSphere();
    }
	
	// Update is called once per frame
	void Update () {
        time= Time.deltaTime+time;
        //Debug.Log("time="+(int)time);
        if ((int)time % 2 == 0)
        {
            if (!flag)
            {
                DoGenerateSphere();
            }
            flag = true;
        }
        else
        {
            flag = false;
        }

        if (Input.GetKeyDown("w")){
            DoGenerateSphere();
        }
    }
    private void DoGenerateSphere()
    {
        int randNum = Random.Range(0, w * h);
        int division = (randNum + w) / w - 1;
        int mod = (randNum + w) % w;
        //Debug.Log(division + ":" + mod);
        int count=0;
        while (true)
        {
            if (count >= w * h)
            {
                break;
            }
            if (spheres[division, mod].activeSelf)
            {
                randNum = Random.Range(0, w * h);
                division = (randNum + w) / w - 1;
                mod = (randNum + w) % w;
            }
            else
            {
                break;
            }
            count++;
        }
        spheres[division, mod].SetActive(true);
    }
}
