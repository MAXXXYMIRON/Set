using System;


namespace prog
{
    class Set
    {
        /// <summary>
        /// Массив 8 битных чисел для составления мно-ва. 
        /// </summary>
        byte[] memory;
        /// <summary>
        /// Размер переменной массива.
        /// </summary>
        const int sizeMemo = 8;


        /// <summary>
        /// Размер мн-ва.
        /// </summary>
        public readonly int Size;


        /// <summary>
        /// Конструктор с заданием размера.
        /// </summary>
        public Set(int size = 64)
        {
            Size = size;

            if (Size % sizeMemo == 0)
                memory = new byte[Size / sizeMemo];
            else
                memory = new byte[(Size / sizeMemo) + 1];
        }


        /// <summary>
        /// Преобразовать в массив или массив в мн-во.
        /// </summary>
        public int[] ToArray
        {
            get
            {
                int[] array = new int[Size];
                byte temp1 = 0, temp2 = 0;
                int index = 0;

                for (int i = 0; i < memory.Length; i++)
                {
                    temp1 = memory[i];
                    temp2 = memory[i];

                    for (int k = 0; k < sizeMemo; k++)
                    {
                        temp2 |= 0b_0000_0001;

                        if (temp1 == temp2)
                        {
                            array[index] = (i * sizeMemo) + k;
                            index++;
                        }

                        temp2 >>= (byte)1;
                        temp1 >>= (byte)1;
                    }
                }
                return array;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    Insert(value[i]);
                }
            }
        }


        /// <summary>
        /// Вставить эл. в мн-во.
        /// </summary>
        public void Insert(int item)
        {
            if (item > Size || item < 0) throw new Exception("Число вне границ множества!");

            int index = item / sizeMemo;
            while (item / sizeMemo > 0)
            {
                item -= sizeMemo;
            }

            byte mask = 0b_0000_0001;
            while(item > 0)
            {
                mask <<= (byte)1;
                item--;
            }

            memory[index] |= mask;
        }

        /// <summary>
        /// Удалить эл. мн-ва.
        /// </summary>
        public void Delete(int item)
        {
            if (item > Size || item < 0) throw new Exception("Число вне границ множества!");

            int index = item / sizeMemo;
            while (item / sizeMemo > 0)
            {
                item -= sizeMemo;
            }

            byte mask = 0b_0000_0001;
            while (item > 0)
            {
                mask <<= (byte)1;
                item--;
            }
            mask ^= 0b_1111_1111;

            memory[index] &= mask;
        }

        /// <summary>
        /// Проверить эл. на вхождение в мн-во.
        /// </summary>
        public bool Contains(int item)
        {
            if (item > Size || item < 0) throw new Exception("Число вне границ множества!");

            int index = item / sizeMemo;
            while (item / sizeMemo > 0)
            {
                item -= sizeMemo;
            }

            byte temp1 = memory[index],
                temp2 = memory[index];
            while (item > 0)
            {
                temp1 >>= (byte)1;
                temp2 >>= (byte)1;
                item--;
            }
            temp1 |= 0b_0000_0001;

            return (temp1 == temp2);
        }


        /// <summary>
        /// Оператор объединения мн-в.
        /// </summary>
        public static Set operator + (Set set1, Set set2)
        {
            if (set1.Size != set2.Size) throw new Exception("Размеры операндов должны совпадать!");

            Set set3 = new Set(set1.Size);

            for (int i = 0; i < set1.memory.Length; i++)
            {
                set3.memory[i] = (byte)(set1.memory[i] | set2.memory[i]);
            }

            return set3;
        }

        /// <summary>
        /// Оператор пересечения мн-в.
        /// </summary>
        public static Set operator * (Set set1, Set set2)
        {
            if (set1.Size != set2.Size) throw new Exception("Размеры операндов должны совпадать!");

            Set set3 = new Set(set1.Size);

            for (int i = 0; i < set1.memory.Length; i++)
            {
                set3.memory[i] = (byte)(set1.memory[i] & set2.memory[i]);
            }

            return set3;
        }

        /// <summary>
        /// Оператор сравнения мн-в.
        /// </summary>
        public static bool operator == (Set set1, Set set2)
        {
            if (set1.Size != set2.Size) throw new Exception("Размеры операндов должны совпадать!");
            for (int i = 0; i < set1.memory.Length; i++)
            {
                if (set1.memory[i] != set2.memory[i]) return false;
            }
            return true;
        }

        /// <summary>
        /// Оператор сравнения мн-в.
        /// </summary>
        public static bool operator != (Set set1, Set set2)
        {
            if (set1.Size != set2.Size) throw new Exception("Размеры операндов должны совпадать!");

            return !(set1 == set2);
        }
    }
}
