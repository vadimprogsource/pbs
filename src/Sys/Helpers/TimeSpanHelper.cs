using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Helpers
{
    //Преобразование стоки в time span: "1min20sec"=>TimeSpan.FromMinutes(1) + TimeSpan.FromSeconds(20) 
    public class TimeSpanHelper
    {

        private static bool findLetterCaseInvariant(string s, int startPos, char token)
        {
            token = char.ToLowerInvariant(token);

            for (int i = startPos; i < s.Length; i++)
            {
                if (char.ToLowerInvariant(s[i]) == token)
                {
                    return true;
                }
            }

            return false;
        }

        public static TimeSpan Parse(string s)
        {
            TimeSpan totalTimeSpan = TimeSpan.Zero;
            StringBuilder digitBuilder = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                char t = s[i];

                if (char.IsWhiteSpace(t))
                {
                    continue;
                }

                if (char.IsDigit(t))
                {
                    digitBuilder.Append(t);
                    continue;
                }

                long time;

                if (!long.TryParse(digitBuilder.ToString(), out time) || time == 0)
                {

                    digitBuilder.Clear();
                    continue;
                }

                digitBuilder.Clear();

                if (char.IsLetter(t))
                {
                    switch (t)
                    {
                        case 'D':
                        case 'd': totalTimeSpan = totalTimeSpan.Add(TimeSpan.FromDays(time)); continue;
                        case 'h':
                        case 'H': totalTimeSpan = totalTimeSpan.Add(TimeSpan.FromHours(time)); continue;
                        case 'M':
                        case 'm':
                            if (findLetterCaseInvariant(s, i + 1, 's'))
                            {
                                totalTimeSpan = totalTimeSpan.Add(TimeSpan.FromMilliseconds(time));
                            }
                            else
                            {
                                totalTimeSpan = totalTimeSpan.Add(TimeSpan.FromMinutes(time));
                            }
                            continue;
                        case 'S':
                        case 's': totalTimeSpan = totalTimeSpan.Add(TimeSpan.FromSeconds(time)); continue;

                    }
                }

                totalTimeSpan = totalTimeSpan.Add(TimeSpan.FromTicks(time));



            }

            return totalTimeSpan;
        }


        public static string ToString(TimeSpan value)
        {
            StringBuilder timeSpanBuilder = new StringBuilder();

            if (value.Days > 0)
            {
                timeSpanBuilder.Append(value.Days).Append('.');
            }


            if (value.Hours < 10)
            {
                timeSpanBuilder.Append('0');
            }

            timeSpanBuilder.Append(value.Hours).Append(':');

            if (value.Minutes < 10)
            {
                timeSpanBuilder.Append('0');
            }

            timeSpanBuilder.Append(value.Minutes).Append(':');


            if (value.Seconds < 10)
            {
                timeSpanBuilder.Append('0');
            }

            timeSpanBuilder.Append(value.Seconds);


            return timeSpanBuilder.ToString();

        }
    }
}
