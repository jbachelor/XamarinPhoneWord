using System;
using System.Text;


namespace Phoneworld_ios
{
	public static class PhoneTranslator
	{
		public static string ToNumber(string raw)
		{
			if (string.IsNullOrWhiteSpace(raw))
			{
				return string.Empty;
			}

			raw = raw.ToUpperInvariant();

			var newNumberSB = new StringBuilder();
			foreach (var c in raw)
			{
				if ("-0123456789".Contains(c))
				{
					newNumberSB.Append(c);
				}
				else
				{
					var result = TranslateToNumber(c);
					if (result != null)
					{
						newNumberSB.Append(result);
					}
				}
			}

			return newNumberSB.ToString();
		}

		public static bool Contains(this string keyString, char c)
		{
			return keyString.IndexOf(c) >= 0;
		}


		public static int? TranslateToNumber(char digit)
		{
			int? result = null;

			if ("ABC".Contains(digit))
			{
				result = 2;
			}
			else if ("DEF".Contains(digit))
			{
				result = 3;
			}
			else if ("GHI".Contains(digit))
			{
				result = 4;
			}
			else if ("JKL".Contains(digit))
			{
				result = 5;
			}
			else if ("MNO".Contains(digit))
			{
				result = 6;
			}
			else if ("PQRS".Contains(digit))
			{
				result = 7;
			}
			else if ("TUV".Contains(digit))
			{
				result = 8;
			}
			else if ("WXYZ".Contains(digit))
			{
				result = 9;
			}

			return result;
		}
	}
}

