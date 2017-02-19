using System;
using Xamarin.Forms;

namespace Marvel.Controls
{
	public class CustomEditor : Editor
	{
		public CustomEditor()
		{
			this.TextChanged += (sender, e) => { this.InvalidateMeasure(); };
		}
	}
}
