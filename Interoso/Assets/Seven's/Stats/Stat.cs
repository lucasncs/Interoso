using UnityEngine;

namespace Seven.Stats
{
	[System.Serializable]
	public class StatWithBar : Stat
	{
		[SerializeField]
		private BarScript _bar;

		public override float Value
		{
			get
			{
				return base.Value;
			}

			set
			{
				base.Value = value;
				if (_bar)
				{
					_bar.Value = _value;
				}
			}
		}

		public override float MaxVal
		{
			get
			{
				return base.MaxVal;
			}

			set
			{
				base.MaxVal = value;
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
			private set
			{
				_bar = value;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
	        MaxVal = _maxVal;

			if (_bar != null)
			{
				_bar.MaxValue = _maxVal;
				_bar.Value = _value;
			}
		}
	}

	[System.Serializable]
	public class Stat
	{
	    [SerializeField]
		protected float _maxVal;

	    [SerializeField]
	    protected float _value;

	    public virtual float Value
	    {
	        get
	        {
	            return _value;
	        }

	        set
	        {
	            _value = Mathf.Clamp(value, 0, MaxVal);
	        }
	    }

	    public virtual float MaxVal
	    {
	        get
	        {
	            return _maxVal;
	        }

	        set
	        {
	            _maxVal = value;
	        }
	    }


		public virtual void Initialize()
	    {
	        Value = _value;
	    }
	}
}