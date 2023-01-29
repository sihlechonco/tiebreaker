using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiebreaker.Services
{
    public class TestService
    {
        public void getTopThree()
        {
            var cards = new List<int>() { 1, 2,3, 4, 4};
            int count = 0;
            var result = cards
            .Select((v, i) => new { v, i })
            .OrderByDescending(x => x.v)
            .ThenByDescending(x => x.i)
            .Take(3)
            .ToArray();

            foreach (var entry in result)
            {
                count = count + entry.i;
            }

            Console.WriteLine(count);
        }

        public void AppendSuitScores(int score, string player)
        {
            int thedate = DateTime.Now.GetHashCode();
            string myfile = @"file.txt";

            // Appending the given texts
            using (StreamWriter sw = File.AppendText(myfile))
            {
                sw.WriteLine(player);
                sw.WriteLine(score);
            }

            // Opening the file for reading
            using (StreamReader sr = File.OpenText(myfile))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public void readfiles()
        { 
            List<int> lst = new List<int>();
            int[] integerArray = new int[6];
            int contor = 0;
            string[] stringArray = System.IO.File.ReadAllLines(@"cardscores.txt");
            foreach (string example in stringArray)
            {
                lst.Add(Convert.ToInt32(example.Trim()));
                integerArray[contor] = Convert.ToInt32(example.Trim());
                ++contor;
            }


            List<string> lstp = new List<string>();
            int[] integerArrayp = new int[6];
            int contorp = 0;
            string[] stringArrayp = System.IO.File.ReadAllLines(@"players.txt");
            foreach (string example in stringArrayp)
            {
                lstp.Add((example.Trim()));
                //integerArrayp[contor] = Convert.ToInt32(example.Trim());
                ++contorp;
            }

            int indexofhighest = 0;
            int indexofDuplicates = 0;
            string winner = "";
            int score = 0;

            score = lst.Max();
            //indexofhighest = lst.IndexOf(lst.Max());
            //winner = lstp[indexofhighest];

            var duplicates = lst.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();
            Console.WriteLine(String.Join(", ", duplicates));

            if(duplicates.Count > 0)
            {
                indexofDuplicates = lst.IndexOf(duplicates[0]);
            }
            //Console.WriteLine(winner + ":" + score);
        }

        public void checkforduplicates()
        {
            int[] arr = { 1, 2, 3, 2, 4, 5, 2, 4 };

            var duplicates = arr.GroupBy(x => x)
                  .Where(g => g.Count() > 1)
                  .Select(y => y.Key)
                  .ToList();

            Console.WriteLine(String.Join(", ", duplicates));
        }

       public void checkForATie(int winningscore, List<int> otherscores, string player)
        {

            int countDuplicates = 0;
            List<int> indexoftie = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                if (otherscores[i].Equals(winningscore))
                {
                    countDuplicates++;
                    indexoftie.Add(i);
                }
            }

            if (countDuplicates > 1)
            {
                checktheSuitScore(countDuplicates, indexoftie, winningscore);
            }
            else
            {
                Console.WriteLine("The winner is : " + player + ":" + winningscore);
            }
        }

        public void checktheSuitScore(int duplicatecount, List<int> indexoftie,int winningscore)
        {
            List<int> lst = new List<int>();
            int[] integerArray = new int[6];
            int contor = 0;
            int highestsuitscore = 0; 
            List<int> listtiedsuits = new List<int>();
            int countDuplicates = 0;

            string[] stringArray = System.IO.File.ReadAllLines(@"cardscores.txt");
            foreach (string example in stringArray)
            {
                lst.Add(Convert.ToInt32(example.Trim()));
                integerArray[contor] = Convert.ToInt32(example.Trim());
                ++contor;
            }

            for(int i =0; i< duplicatecount; i++)
            {
                listtiedsuits.Add(integerArray[indexoftie[i]]);
            }

            highestsuitscore = listtiedsuits.Max();

            for (int i = 0; i < listtiedsuits.Count; i++)
            {
                if (listtiedsuits[i].Equals(highestsuitscore))
                {
                    countDuplicates++;
                    //indexoftie.Add(i);
                }
            }

            if (countDuplicates > 1)
            {
                List<string> lstp = new List<string>();
                //int[] integerArrayp = new int[6];
                List<string> players = new List<string>();
                int contorp = 0;
                string[] stringArrayp = System.IO.File.ReadAllLines(@"players.txt");
                foreach (string example in stringArrayp)
                {
                    lstp.Add((example.Trim()));
                    //integerArrayp[contor] = Convert.ToInt32(example.Trim());
                    ++contorp;
                }


                for(int i=0; i < countDuplicates; i++) 
                {
                    players.Add(lstp[indexoftie[i]]);
                }

                Console.WriteLine("The winners are:");
                Console.WriteLine(String.Join(", ", players) + " : " + winningscore);
                //Console.WriteLine(", ", winningscore);
            }
        }

    }
}
