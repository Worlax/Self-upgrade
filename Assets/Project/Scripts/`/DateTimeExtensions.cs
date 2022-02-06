using System;
using UnityEngine;

public static class DateTimeExtensions
{
	public static DateTime GetStartOfYear(this DateTime dateTime)
	{
		return new DateTime(dateTime.Year, 1, 1);
	}

	public static DateTime GetStartOfMonth(this DateTime dateTime)
	{
		return new DateTime(dateTime.Year, dateTime.Month, 1);
	}

	public static DateTime GetStartOfWeek(this DateTime dateTime)
	{
		DateTime returnDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

		while (returnDate.DayOfWeek != DayOfWeek.Monday)
		{
			returnDate = returnDate.AddDays(-1);
		}

		return returnDate;
	}

	public static DateTime GetEndOfYear(this DateTime dateTime)
	{
		return new DateTime(dateTime.Year, 12, 31);
	}

	public static DateTime GetEndOfMonth(this DateTime dateTime)
	{
		int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

		return new DateTime(dateTime.Year, dateTime.Month, daysInMonth);
	}

	public static DateTime GetEndOfWeek(this DateTime dateTime)
	{
		DateTime returnDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

		while (returnDate.DayOfWeek != DayOfWeek.Sunday)
		{
			returnDate = returnDate.AddDays(1);
		}

		return returnDate;
	}
}