using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog
{
    class Program
    {
        static void Main(string[] args)
        {
            Eratosfen(50);
            Console.ReadKey();
        }

        //Вывод простых чисел от 2 до n
        static void Eratosfen(int n)
        {
            Set set = new Set(n);

            for (int i = 2; i <= n; i++)
            {
                set.Insert(i);
            }

            for (int i = 2; i * i <= n; i++)
            {
                if(set.Contains(i))
                {
                    for (int k = i + i; k <= n; k += i)
                    {
                        if (set.Contains(k))
                            set.Delete(k);
                    }
                }
            }

            int[] array = set.ToArray;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0) break;
                Console.Write($"{array[i]}  ");
            }
            Console.WriteLine();
        }
    }
}
