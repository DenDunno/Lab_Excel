﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    class _26Converter
    {
        private static int _26system = 26;
        private static int start = 64;     // 64 в ASCII це '@' , 65 = 'A' ,  66 = 'B' і так далі


        public static string ConvertTo26 (int num)
        {
            string answer = "";

            List<int> _26numbers = new List<int>();

            while (num > _26system)
            {
                _26numbers.Add(num % _26system);
                num /= _26system;
            }

            _26numbers.Add(num);
            _26numbers.Reverse();

            foreach (int _26num in _26numbers)
            {
                answer += Convert.ToChar(_26num + start);
            }
            
            return answer;
        }


        public static int ConvertTo10(string num26)
        {
            int answer = 0;

            num26.Reverse();
            
            for (int i = 0; i < num26.Count(); ++i)
            {
                answer += (int)Math.Pow(_26system, i) * num26[i];
            }

            return answer - start;
        }
    }
}