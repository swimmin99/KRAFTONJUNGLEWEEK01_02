using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
public class waterShaper : MonoBehaviour
{

    [SerializeField]
    private GameObject wavePointPref;
    [SerializeField]
    private GameObject wavePoints;
    [SerializeField]
    private int wavePointsNum;
    [SerializeField]
    [Range(1, 100)]
    private int WavesCount;
    private List<waterSpring> springs = new();
    // How stiff should our spring be constnat
    public float springStiffness = 0.1f;
    // Slowing the movement over time
    public float dampening = 0.03f;
    // How much to spread to the other springs
    public float spread = 0.006f;
    public bool isReady = false;

    public LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = WavesCount;


    }
    void OnValidate()
    {
        StartCoroutine(CreateWaves());
    }
    IEnumerator CreateWaves()
    {
        foreach (Transform child in wavePoints.transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }
        yield return null;
        SetWaves();
        yield return null;
        isReady = true;
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    IEnumerator UpdateWave()
    {
        updateMiddlePoints();
        yield return null;
        UpdateSprings();
        yield return null;
        UpdateLine();
        yield return null;
        Splash(UnityEngine.Random.Range(1, WavesCount), UnityEngine.Random.Range(.001f, .0015f));
    }

    private void SetWaves()
    {
        int waterPointsCount = wavePointsNum;


        springs = new();
        for (int i = 0; i < WavesCount; i++)
        {
            int index = i + 1;

            GameObject wavePoint = Instantiate(wavePointPref, new Vector3(wavePoints.transform.position.x + index * 0.5f, wavePoints.transform.position.y, 0f), wavePoints.transform.rotation, wavePoints.transform) ;

            waterSpring waterSpring = wavePoint.GetComponent<waterSpring>();
            springs.Add(waterSpring);
            springs[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;


        }
        springs[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        springs[WavesCount-1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;


    }

    private void FixedUpdate()
    {
        if(isReady == true)
        {
            StartCoroutine(UpdateWave());
        }
    }


    void updateMiddlePoints()
    {
        lineRenderer.positionCount = WavesCount;
        if (isReady == true)
        {
            foreach (waterSpring waterSpringComponent in springs)
            {
                waterSpringComponent.WaveSpringUpdate(springStiffness, dampening);
                waterSpringComponent.WavePointUpdate();
            }
        }

    }

    void UpdateLine()
    {
        for (int i = 1; i < WavesCount-1; i++)
        {
            lineRenderer.SetPosition(i, springs[i].transform.position);
        }
        lineRenderer.SetPosition(0, springs[0].transform.position + Vector3.left * .75f);
        lineRenderer.SetPosition(WavesCount - 1, springs[WavesCount - 1].transform.position + Vector3.right *.75f);

    }


    private void UpdateSprings()
    {

        int count = springs.Count;
        float[] left_deltas = new float[count];
        float[] right_deltas = new float[count];

        for (int i = 1; i < count-1; i++)
        {
            if (i > 1)
            {
                left_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i - 1].velocity += left_deltas[i];
            }
            if (i < springs.Count - 1)
            {
                right_deltas[i] = spread * (springs[i].height - springs[i + 1].height);
                springs[i + 1].velocity += right_deltas[i];
            }
        }

    }
    private void Splash(int index, float speed)
    {
        if (index >= 0 && index < springs.Count)
        {
            springs[index].velocity += speed;
        }
    }

}
