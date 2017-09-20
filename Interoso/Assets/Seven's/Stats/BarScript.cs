using UnityEngine;
using UnityEngine.UI;

namespace Seven.Stats
{
	public class BarScript : MonoBehaviour
	{
	    private float fillAmount;

	    [SerializeField]
	    private float lerpSpeed = 5f;

	    [SerializeField]
	    private Image content;

	    [SerializeField]
	    private Text valueText;

	    [SerializeField]private Color fullColor;
	    [SerializeField]private Color lowColor;
	    [SerializeField]private bool lerpColors;

	    [SerializeField]
	    private bool scaleBar;

	    private float currentValue;

	    public float MaxValue { get; set; }

	    public float Value
	    {
	        set
	        {
	            //string[] tmp = valueText.text.Split(':');
	            if(valueText) valueText.text = value + " / " + MaxValue;
	            currentValue = value;
	            fillAmount = Map(value, 0, MaxValue, 0, 1);
	        }
	    }


	    void Start ()
	    {
	        if (lerpColors) content.color = fullColor;
		}

		void Update ()
	    {
	        HandleBar();
	    }

	    public void HandleBar()
	    {
	        if (!scaleBar && fillAmount != content.fillAmount)
	        {
	            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
	        }
	        else if (scaleBar && fillAmount != content.transform.localScale.x)
	        {
	            content.transform.localScale = Vector3.Lerp(content.transform.localScale, new Vector3(fillAmount, fillAmount, 1), Time.deltaTime * lerpSpeed);
	        }

	        if (lerpColors)
	        {
	            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
	        }
	    }

	    /// <summary>
	    /// Translate the Value into a value that the fillAmount can understand.
	    /// Returns a value between 0 and 1
	    /// </summary>
	    /// <param name="value">The actual Value that will be evaluated.</param>
	    /// <param name="inMin">Minimum/Lowest the Value can be</param>
	    /// <param name="inMax">Maximum/Highest the Value can be</param>
	    /// <param name="outMin">Minimum value that it can go -0f-</param>
	    /// <param name="outMax">Maximum value that it can go -1f-</param>
	    /// <returns>Returns a value between 0 and 1</returns>
	    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	    {
	        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	    }

	    public void UpdateBar()
	    {
	        //fillAmount = Map(currentValue, 0, MaxValue, 0, 1);
	        currentValue = Mathf.Ceil(MaxValue * fillAmount);
	        Value = currentValue;

	        //valueText.text = currentValue + " / " + MaxValue;
	    }
	}
}