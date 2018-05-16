using System;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreeElementsHash();
            SameKeySave();
            SearchOneOf10000();
            SearchNullElementsOf11000();

            Console.ReadKey();
        }

        private static void ThreeElementsHash()
        {
            int checksum = 0;
            HashTable Table = new HashTable(3);
            Table.PutPair(0, "sth0");
            Table.PutPair(1, "sth1");
            Table.PutPair(2, "sth2");
            for (int i=0; i<3; i++)
                if(Table.GetValueByKey(i).ToString() == ("sth"+i))
                    checksum++;
                else
                    Console.WriteLine("! " + i + "-й ключ имеет неправильное значение");

            if (checksum == 3)
                Console.WriteLine("Все ключи имеют правильные значения");
        }

        private static void SameKeySave()
        {
            HashTable Table = new HashTable(1);
            Table.PutPair(0, "wrong");
            Table.PutPair(0, "right");
            if ((Table.GetValueByKey(0)).ToString() == ("right"))
                Console.WriteLine("Значение ключа было корректно перезаписано");
            else
                Console.WriteLine("! Ключ имеет неверное значение");
        }

        private static void SearchOneOf10000()
        {
            var rnd = new Random();
            HashTable Table = new HashTable(10000);
            for (int i = 0; i < 10000; i++)
                Table.PutPair(i, i.ToString());
            int number = rnd.Next(0, 9999);
            if ((Table.GetValueByKey(number)).ToString() == (number.ToString()))
                Console.WriteLine("Значение ключа совпадает с ожидаемым");
            else
                Console.WriteLine("! Значение ключа не совпадает с нужным");
        }

        private static void SearchNullElementsOf11000()
        {
            int counter = 0;
            HashTable Table = new HashTable(11000);
            for (int i = 0; i < 10000; i++)
                Table.PutPair(i, i.ToString());
            for (int i = 10000; i < 11000; i++)
                if (Table.GetValueByKey(i) == null)
                    counter++;
                else
                    Console.WriteLine("! " + i + "-й элемент возвратил не null значение");
            if (counter == 1000)
                Console.WriteLine("Значения возвращены верно");
        }
    }

    class HashTable
    {
        public class Item
        {
            int key;
            object value;
            public Item(int key, object value)
            {
                this.key = key;
                this.value = value;
            }

            public int GetKey()
            {
                return key;
            }

            public object GetValue()
            {
                return value;
            }
        }

        Item[] Table;
 
        public HashTable(int size)
        {
            Table = new Item[size];
        }

        public void PutPair(int key, object value)
        {
            int position = (key % Table.Length);
            while(Table[position] != null && Table[position].GetKey() != key)
            {
                position = (position + 1) % Table.Length;
            }
            Table[position] = new Item(key, value);
        }

        public object GetValueByKey(int key)
        {
            int position = key % Table.Length;
            while (Table[position] != null && Table[position].GetKey() != key)
            {
                position = (position + 1) % Table.Length;
            }
            if (Table[position] != null)
            {
                return Table[position].GetValue();
            }
            else
            {
                return null;
            }
        }
    }
}
