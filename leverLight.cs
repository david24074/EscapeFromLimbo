using UnityEngine;

public class leverLight : MonoBehaviour
{
    public void activateLight()
    {
        GetComponent<Light>().color = Color.green;
    }
}
