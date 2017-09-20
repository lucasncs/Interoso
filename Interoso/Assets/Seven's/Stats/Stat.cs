using UnityEngine;

namespace Seven.Stats
{
	[System.Serializable]
	public class Stat
	{
	    [SerializeField]
	    private BarScript _bar;

	    [SerializeField]
	    private float _maxVal;

	    [SerializeField]
	    private float _value;

	    public float Value
	    {
	        get
	        {
	            return _value;
	        }

	        set
	        {
	            _value = Mathf.Clamp(value, 0, MaxVal);
	            if (_bar)
	            {
	                _bar.Value = _value;
	            }
	        }
	    }

	    public float MaxVal
	    {
	        get
	        {
	            return _maxVal;
	        }

	        set
	        {
	            _maxVal = value;
	            if (_bar)
	            {
	                _bar.MaxValue = _maxVal;
	            }
	        }
	    }

	    public BarScript VisualBar
	    {
	        get
	        {
	            return _bar;
	        }
	        set
	        {
	            _bar = value;
	        }
	    }

	    Stat()
	    {
	        MaxVal = _maxVal;
	        Value = _value;
	    }
	}
}