using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public static class ExtensionMethods
{
	#region MonoBehaviour

	/// <summary>
	/// Invokes the method <paramref name="method"/> in <paramref name="time"/> seconds.
	/// </summary>
	public static Coroutine Invoke(this MonoBehaviour mb, Action method, float time)
	{
		return mb.StartCoroutine(Invoker(method, time));
	}

	/// <summary>
	/// Invokes the method <paramref name="method"/> in <paramref name="time"/> seconds, then repeatedly every <paramref name="repeatRate"/> seconds.
	/// </summary>
	public static Coroutine InvokeRepeating(this MonoBehaviour mb, Action method, float time, float repeatRate)
	{
		return mb.StartCoroutine(Invoker(method, time, repeatRate, true));
	}

	private static IEnumerator Invoker(Action method, float time, float repeatRate = 0, bool repeat = false)
	{
		yield return new WaitForSeconds(time);
		do
		{
			method();
			yield return new WaitForSeconds(repeatRate);
		} while (repeat);
	}

	#endregion

	#region String

	/// <summary>
	/// Converts the first letter to Upper and separete with spaces words in Camel.
	/// </summary>
	public static string ToTitleCase(this string str)
	{
		return (str.Length > 0 ? str.FirstUpperCase().SepareteCamel() : str);
	}

	/// <summary>
	/// Separete  with spaces words in Camel.
	/// </summary>
	public static string SepareteCamel(this string str)
	{
		return (str.Length > 0 ? Regex.Replace(str, @"(\B[A-Z])", @" $1") : str);
	}

	/// <summary>
	/// Converts the first letter to Upper.
	/// </summary>
	public static string FirstUpperCase(this string str)
	{
		string firstChar = str[0].ToString();
		return (str.Length > 0 ? firstChar.ToUpper() + str.Substring(1) : str);
	}

	/// <summary>
	/// Verify if the string is empty.
	/// </summary>
	public static bool IsNullOrEmpty(this string str)
	{
		return str == "" || str == null;
	}

	#endregion

	#region Color

	/// <summary>
	/// Returns the Hexadecimal code of the <paramref name="color"/>.
	/// </summary>
	public static string ToHex(this Color color)
	{
		Color32 temp = color;
		return string.Format("{0}{1}{2}{3}", temp.r.ToString("X2"), temp.g.ToString("X2"), temp.b.ToString("X2"), temp.a.ToString("X2"));
	}

	/// <summary>
	/// Returns the Color of the Hexadecimal code.
	/// </summary>
	public static Color ToColor(this string hex)
	{
		hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
		hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
		byte a = 255;//assume fully visible unless specified in hex
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		//Only use alpha if the string has enough characters
		if (hex.Length == 8)
		{
			a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		}
		return new Color32(r, g, b, a);
	}

	#endregion

	#region Object

	/// <summary>
	/// Verify if the <paramref name="obj"/> is null or empty.
	/// </summary>
	public static bool IsNullOrEmpty(this UnityEngine.Object obj)
	{
		return obj == null;
	}

	#endregion
}