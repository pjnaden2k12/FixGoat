using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    [Header("WheelDesign")]

    [SerializeField] int numberOfSlices = 0;
    [SerializeField] GameObject sliceObject;
    [SerializeField] Color[] colors;
    [SerializeField] string[] content;



    [Header("WheelSpecifications")]

    [SerializeField] float initialSpinSpeed = 360f;
    [SerializeField] float deceleration = 30f;
    
    float currentSpinSpeed;
    bool isSpinning;

    [Header("Output")]

    [SerializeField] TextMeshProUGUI output;
    float timer = 0;
    void Start()
    {
        GenerateWheel();
    }
    void GenerateWheel()
    {
        for(int i = 0; i<numberOfSlices; i++)
        {
            GameObject slice = Instantiate(sliceObject, transform);
            float sliceSize = 1f / numberOfSlices;
            float sliceRotation = (360f / numberOfSlices) * (i + 1);
            Image sliceImg = slice.GetComponent<Image>();
            slice.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, (180f*sliceSize)-90f);
            sliceImg.fillAmount = sliceSize;
            sliceImg.color = colors[i % colors.Count()] ;
            TextMeshProUGUI contentText = slice.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            contentText.text = content[i % content.Count()];
            slice.transform.localRotation = Quaternion.Euler(0, 0, -sliceRotation);
        }
        
    }
    public void Spin()
    {
        currentSpinSpeed = initialSpinSpeed+Random.Range((initialSpinSpeed/4)*-1, (initialSpinSpeed / 4));
        isSpinning = true;
    }
    void CheckReward()
    {
        for(int i = 0; i<numberOfSlices;i++)
        {
            float startRotation = (360 / numberOfSlices) * i;
            float endRotation = (360 / numberOfSlices) * (i+1);
            if (transform.eulerAngles.z > startRotation && transform.eulerAngles.z <= endRotation)
            {
                output.text = content[i % content.Count()];
            }
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown("s"))
        {
            Spin();
        }
        if (isSpinning)
        {
            timer += Time.deltaTime;
            if (timer > 0.1f)
            {
                CheckReward();
                timer = 0;
            }

            transform.Rotate(new Vector3(0, 0, currentSpinSpeed * Time.deltaTime));

            currentSpinSpeed -= deceleration * Time.deltaTime;

            if (currentSpinSpeed <= 0)
            {
                currentSpinSpeed = 0;
                CheckReward();
                isSpinning = false;
            }
        }
    }
}
