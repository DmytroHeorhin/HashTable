using HashTable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 3; i < 10000; i *= 2)
            //{
            //    System.Console.WriteLine(Util.GetPrime(i));
            //}
            //var t = new HashTable(1000);
            //System.Console.WriteLine(t.GetHash(new object()));
            //System.Console.WriteLine(t.GetHash(new object()));
            //System.Console.WriteLine(t.GetHash(-781));
            //System.Console.WriteLine(t.GetHash(10000000000000000));
            //System.Console.WriteLine(t.GetHash("test"));
            //System.Console.WriteLine(t.GetHash(0));
            //System.Console.WriteLine(t.GetHash(-2));

            //var r = new Random();
            //for (int i = 0; i < 1000; i++)
            //{
            //    System.Console.WriteLine(t.GetHash(r.Next()));
            //}

            var myHashTable = new HashTable(100);
            myHashTable.Add(1, "hi");
            myHashTable.Add("service", new Random());
            for (int i = 4; i < 1000; i++)
            {
                myHashTable.Add(i, $"{i}-th element");
            }
            System.Console.WriteLine(myHashTable["service"]);
            myHashTable["service"] = null;
            System.Console.WriteLine(myHashTable["service"]);

        }
    }
}
