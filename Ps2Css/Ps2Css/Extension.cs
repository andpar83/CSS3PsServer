using System;
using System.Collections.Generic;

public static class Extension
{
	public static T[] ToArray<T>(this IEnumerable<T> value)
	{
		return new List<T>(value).ToArray();
	}
	public static IEnumerable<TOutput> ConvertAll<T, TOutput>(this IEnumerable<T> value, Converter<T, TOutput> converter)
	{
		foreach(var item in value)
		{
			yield return converter(item);
		}
	}

	public static TOutput Min<TInput, TOutput>(this List<TInput> @this, Predicate<TInput> match, Converter<TInput, TOutput> converter) where TOutput : IComparable<TOutput>
	{
		var values = @this.FindAll(match).ConvertAll(converter);
		values.Sort();
		return values[0];
	}
	public static TOutput Max<TInput, TOutput>(this List<TInput> @this, Predicate<TInput> match, Converter<TInput, TOutput> converter) where TOutput : IComparable<TOutput>
	{
		var values = @this.FindAll(match).ConvertAll(converter);
		values.Sort();
		return values[values.Count - 1];
	}
}
public class Enum<T>
{
	public static T Parse(string value)
	{
		return (T)Enum.Parse(typeof(T), value);
	}
}
