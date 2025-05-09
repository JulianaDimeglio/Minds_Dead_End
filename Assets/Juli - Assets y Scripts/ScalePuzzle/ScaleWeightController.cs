using TMPro;
using UnityEngine;

public class ScaleWeightController : MonoBehaviour
{
    //reference to the light of the scale 
    public GameObject scaleLight;
    //reference to the plates
    public Transform leftPlate;
    public Transform rightPlate;
    //left plate weight 
    [SerializeField]
    private float _leftWeight = 0f;
    //right plate weight 
    [SerializeField]
    private float _rightWeight = 0f;
    //the correct weight to beat the puzzle  
    [SerializeField]
    private float _goalWeight;


    public TextMeshPro totalWeightText;  
    //stores the calculated sum of both plates weight 
    private float _totalWeight;

    void Update()
    {
        VerifyWeight(leftPlate, ref _leftWeight);
        VerifyWeight(rightPlate, ref _rightWeight);
        //calculate the total weight of the plates 
        _totalWeight = _rightWeight + _leftWeight;
        UpdateFeedback();
    }

    void VerifyWeight(Transform plate, ref float weight)
    {
        //detects objects overlapping the plate 
        Collider[] objects = Physics.OverlapBox(plate.position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity);
        //reset weight 
        weight = 0f;

        //loops through each collider detected on the plate 
        foreach (Collider col in objects)
        {
            if (col.CompareTag("Grabbable"))
            {
                MovableObject grabbable = col.GetComponent<MovableObject>();
                if (grabbable != null)
                {
                    weight += grabbable.objectWeight;
                }
            }
        }

        
            totalWeightText.text = _totalWeight.ToString();

     
    }
    // Function to update the visual feedback on the scale based on the weight
    void UpdateFeedback()
    {
        //gets the material from the light to change its color 
        var mat = scaleLight.GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");

        if (_totalWeight == 0f)
        {
            mat.SetColor("_EmissionColor", Color.white);
        }
        else if (_totalWeight == _goalWeight)
        {
            mat.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            mat.SetColor("_EmissionColor", Color.red);
        }
    }
}
