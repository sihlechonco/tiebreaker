using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiebreaker.Services
{
    public class tiebreaker
    {
        public void ReadInputFileName()
        {
            try
            {
                DeleteFiles();

                string textFile = "";
                string ext = "";

                Console.WriteLine("Please enter file name  :");
                textFile = Console.ReadLine();
                ext = Path.GetExtension(textFile);

                if (textFile == null)
                {
                    Console.WriteLine("Error! Please enter a file name : ");
                    textFile = Console.ReadLine();
                
                    ReadInputFileData(textFile);
                    Console.ReadKey();
                }
                else if(ext != ".txt")
                {
                    Console.WriteLine("Error! Please enter a text file name with .txt extention: ");
                    textFile = Console.ReadLine();

                    ReadInputFileData(textFile);
                    Console.ReadKey();
                }
                else
                {
                    //Console.WriteLine("You entered '{0}'", textFile);
                    
                    ReadInputFileData(textFile);
                    Console.ReadKey();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }

        public void ReadInputFileData(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                if(lines.Length > 6)
                {
                    Console.WriteLine("Error! The game is for only 6 players!");
                }
                else
                {
                    string players = "";
                    string scores = "";
                    List<int> scoresList = new List<int>();
                    List<int> scoresSuitList = new List<int>();
                    int playerfinalscore = 0;
                    int playerfinalsuitscore = 0;
                    foreach (string line in lines)
                    {
                        players = line.Substring(0, line.IndexOf(":"));
                        scores = line.Substring(line.IndexOf(":") + 1);
                        scoresList = CalcScores(scores, players);
                        scoresSuitList = CalcSuitScores(scores, players);

                        AppendPlayers(players);

                        playerfinalscore = getTopThree(scoresList);
                        AppendScores(playerfinalscore);
                        playerfinalsuitscore = getSuitCount(scoresSuitList);
                        AppendSuitScores(playerfinalsuitscore);
                        //Console.WriteLine(players);
                        //Console.WriteLine(scores);
                    }
                    readfiles();
                    Console.ReadKey();
                }
 
            }
            catch (Exception e)
            {

                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }
        public List<int> CalcSuitScores(string score, string player)
        {
            
            List<int> sumofscore = new List<int>();
            try
            {
                string[] cards = score.Split(',').Select(s => s.Trim()).ToArray();
                var cardSuitList = new List<int>();


                int suitChecked = 0;
                //Console.WriteLine("Player : '{0}'", player);

                foreach (string card in cards)
                {

                    suitChecked = checkCardSuit(card);

                    cardSuitList.Add(suitChecked);
                    //Console.WriteLine(cardchecked);
                }
                sumofscore.Add(Convert.ToInt32(cardSuitList.Sum().ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
            return sumofscore;
        }
        public List<int> CalcScores(string score, string player)
        {
            
            List<int> sumofscore = new List<int>();
            try
            {
                string[] cards = score.Split(',').Select(s => s.Trim()).ToArray();

                int cardchecked = 0;
                var cardList = new List<int>();


                foreach (string card in cards)
                {
                    cardchecked = checkCard(card);

                    cardList.Add(cardchecked);

                }

                sumofscore.Add(Convert.ToInt32(cardList.Sum().ToString()));

            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
            return sumofscore;
        }

        public int checkCard(string card)
        {   
            //card = card.Substring(0, 1).ToUpper();
            if (card.Length == 3)
            {
                card = card.Substring(0, 2).ToUpper();
            }
            else if (card.Length == 2)
            {
                card = card.Substring(0, 1).ToUpper();
            }
            else
            {
                Console.WriteLine("Opps! Something is wrong with this card : " + card);
            }
            switch (card)
            {
                case "K":
                card = "13";
                    break;
                case "Q":
                    card = "12";
                    break;
                case "J":
                    card = "11";
                    break;
                case "A":
                    card = "11";
                    break;
                default:
                    card = card;
                    break;
            }
            return Convert.ToInt32(card);
        }

        public int checkCardSuit(string card)
        {
            if(card.Length == 3)
            {
                card = card.Substring(2, 1).ToUpper();
            }
            else if(card.Length == 2)
            {
                card = card.Substring(1, 1).ToUpper();
            }
            else
            {
                Console.WriteLine("Opps! Something is wrong with this card : " + card);
            }
            

            switch (card)
            {
                case "C":
                    card = "1";
                    break;
                case "D":
                    card = "2";
                    break;
                case "H":
                    card = "3";
                    break;
                case "S":
                    card = "4";
                    break;
                default:
                    card = card;
                    break;
            }
            return Convert.ToInt32(card);
        }

        public int getTopThree(List<int> cardsList)
        {
            var cards = cardsList;
            int count = 0;
            var result = cards
            .Select((v, i) => new { v, i })
            .OrderByDescending(x => x.v)
            .ThenByDescending(x => x.i)
            .Take(3)
            .ToArray();

            foreach (var entry in result)
            {
                count = count + entry.v;
            }

            return count;
        }

        public int getSuitCount(List<int> cardsList)
        {
            var cards = cardsList;
            int count = 0;
           

            foreach (var entry in cardsList)
            {
                count = count + entry;
            }

            return count;
        }

        public void AppendSuitScores(int score)
        {
            try
            {
                int thedate = DateTime.Now.GetHashCode();
                string myfile = @"suitscores.txt";

                // Appending the given texts
                using (StreamWriter sw = File.AppendText(myfile))
                {

                    //sw.WriteLine(score);
                }

                // Opening the file for reading
                using (StreamReader sr = File.OpenText(myfile))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }

        public void AppendPlayers(string player)
        {
            try
            {
                int thedate = DateTime.Now.GetHashCode();
                string myfile = @"players.txt";

                // Appending the given texts
                using (StreamWriter sw = File.AppendText(myfile))
                {

                    sw.WriteLine(player);
                }

                // Opening the file for reading
                using (StreamReader sr = File.OpenText(myfile))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }

        public void AppendScores(int score)
        {
            try
            {
                int thedate = DateTime.Now.GetHashCode();
                string myfile = @"cardscores.txt";

                // Appending the given texts
                using (StreamWriter sw = File.AppendText(myfile))
                {
                    sw.WriteLine(score);
                }

                // Opening the file for reading
                using (StreamReader sr = File.OpenText(myfile))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }

        public void readfiles()
        {
            try
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
                string winner = "";
                int score = 0;

                score = lst.Max();
                indexofhighest = lst.IndexOf(lst.Max());
                winner = lstp[indexofhighest];

                checkForATie(score, lst, winner);
                //Console.WriteLine("The winner is : "+winner + ":" + score);
            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }

        public void DeleteFiles()
        {
            string suits = @"suitscores.txt";
            string players = @"players.txt";
            string cards = @"cardscores.txt";
            File.Delete(suits);
            File.Delete(players);
            File.Delete(cards);
        }

        public void checkForATie(int winningscore, List<int> otherscores, string player)
        {

            int countDuplicates = 0;
            List<int> indexoftie = new List<int>();

            for (int i = 0; i < otherscores.Count; i++)
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

        public void checktheSuitScore(int duplicatecount, List<int> indexoftie, int winningscore)
        {
            try
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

                for (int i = 0; i < duplicatecount; i++)
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


                    for (int i = 0; i < countDuplicates; i++)
                    {
                        players.Add(lstp[indexoftie[i]]);
                    }

                    Console.WriteLine("The winners are:");
                    Console.WriteLine(String.Join(", ", players) + " : " + winningscore);
                    //Console.WriteLine(", ", winningscore);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Some reason something went wrong. Please see the below error");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
