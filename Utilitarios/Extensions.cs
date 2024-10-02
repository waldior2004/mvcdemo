using System;
using System.Collections.Generic;

namespace com.msc.infraestructure.utils
{
    public static class Extensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public static string ToSize(this Int64 value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00");
        }

        public static string ToClearString(this string value)
        {
            return value.Trim().Replace("'", "").Replace("´", "");
        }

        public static string ToClearDateTimeString(this string value)
        {
            var fecEnter = value.Trim().Replace("'", "").Replace("´", "").Replace("a. m.", "am").Replace("p. m.", "pm").Replace("a.m.","am").Replace("p.m.","pm");

            var segmentsIn = fecEnter.Split(' ');

            if (segmentsIn.Length >= 3)
            {
                if (segmentsIn[0].Length == 9)
                {
                    segmentsIn[0] = "0" + segmentsIn[0];
                }
                if (segmentsIn[1].Length == 4)
                {
                    segmentsIn[1] = "0" + segmentsIn[1];
                }

                return string.Format("{0} {1} {2}", segmentsIn[0], segmentsIn[1], segmentsIn[2]);
            }
            else return fecEnter;
        }

    }
}
