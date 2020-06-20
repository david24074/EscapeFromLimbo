using UnityEngine;

public class LeverLight : MonoBehaviour
{
    public void ActivateLight()
    {
        GetComponent<Light>().color = Color.green;
    }
}
