using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace infojegyzethu_kemiaiElemekFelfedezese
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("C:/Users/gluck/source/repos/infojegyzethu_kemiaiElemekFelfedezese/felfedezesek.csv", Encoding.UTF8);

            List<discovery> discoveryList = new List<discovery>();

            for (int i = 1; i < lines.Length; i++)
            {
                discovery discovery = new discovery(lines[i]);
                discoveryList.Add(discovery);
            }

            Console.Write("3. feladat:");
            numberOfChemicalElements(discoveryList);

            Console.Write("4. feladat:");
            discoveriesInAncientTimes(discoveryList);

            Console.Write("5. feladat:");
            string vegyjel = queryAChemicalSymbolFromUser();

            Console.Write("6. feladat: Keresés");
            search(discoveryList, vegyjel);

            Console.Write("7. feladat:");
            timeSpanBetweenDiscoveries(discoveryList);

            Console.WriteLine("8. feladat: Statisztika");
            statistics(discoveryList);

            Console.ReadKey();
        }

        private static void statistics(List<discovery> discoveryList)
        {
            int examinedDiscoverysYear = 0;
            
            List<int> DiscoveryYears = new List<int>();
            List<int> DiscoveryYearsDistinct = new List<int>();

            for (int i = 0; i < discoveryList.Count; i++)
            {           
                if (int.TryParse(discoveryList[i].ev, out examinedDiscoverysYear))
                {
                    DiscoveryYears.Add(examinedDiscoverysYear);
                }
            }

            DiscoveryYearsDistinct = DiscoveryYears.Distinct().ToList();

            foreach (var DiscoveryYear in DiscoveryYearsDistinct)
            {
                int discoveryCounter = 0;

                for (int i = 1; i < discoveryList.Count; i++)
                {
                    if (discoveryList[i].ev == DiscoveryYear.ToString())
                    {
                        discoveryCounter++;
                    }                    
                }

                if (discoveryCounter >= 4)
                {
                    Console.WriteLine(DiscoveryYear + ": " + discoveryCounter + " db");
                }
            }
        }

        private static void timeSpanBetweenDiscoveries(List<discovery> discoveryList)
        {
            List<int> timeSpanList = new List<int>();
            int examinedDiscoverysYear = 0;
            int previousDiscoverysYear = 0;

            for (int i = 1; i < discoveryList.Count; i++)
            {
                if (int.TryParse(discoveryList[i].ev, out examinedDiscoverysYear) && int.TryParse(discoveryList[i-1].ev, out previousDiscoverysYear))
                {                  
                        timeSpanList.Add(examinedDiscoverysYear - previousDiscoverysYear);
                }                
            }

            int maxTimeSpan = 0;

            for (int i = 0; i < timeSpanList.Count; i++)
            {
                if (timeSpanList[i] > maxTimeSpan)
                {
                    maxTimeSpan = timeSpanList[i];
                }
            }
            Console.WriteLine(" " + maxTimeSpan + " év volt a leghosszabb időszak két elem felfedezése között."); 
        }

        private static void search(List<discovery> discoveryList, string vegyjel)
        {
            bool chemicalFound = false;

            for (int i = 0; i < discoveryList.Count; i++)
            {
                if (discoveryList[i].vegyjel.ToUpper() == vegyjel.ToUpper())
                {
                    Console.WriteLine("Az elem vegyjele: " + discoveryList[i].vegyjel);
                    Console.WriteLine("Az elem neve: " + discoveryList[i].elem);
                    Console.WriteLine("Rendszáma: " + discoveryList[i].rendszam);
                    Console.WriteLine("Felfedezés éve: " + discoveryList[i].ev);
                    Console.WriteLine("Felfedező: " + discoveryList[i].felfedezo);
                }
                else
                {
                    chemicalFound = false;
                }
            }

            if (!chemicalFound)
            {
                Console.WriteLine("Nincs ilyen elem az adatbázisban!");
            }            
        }

        private static string queryAChemicalSymbolFromUser()
        {
            string pattern = @"^[a-zA-Z]+$";
            Regex rx = new Regex(pattern);
            Match match;

            string vegyjel;

            do
            {
                Console.Write("Kérek egy vegyjelet: ");
                vegyjel = Console.ReadLine();

                match = rx.Match(vegyjel);

            } while (!(vegyjel.Length == 1 || vegyjel.Length == 2) && match.Success);

            return vegyjel;
        }

        private static void numberOfChemicalElements(List<discovery> discoveryList)
        {
            Console.WriteLine(discoveryList.Count);
        }

        private static void discoveriesInAncientTimes(List<discovery> discoveryList)
        {
            int discoveriesInAncientTimesCounter = 0;

            foreach (var discovery in discoveryList)
            {
                if (discovery.ev == "Ókor")
                {
                    discoveriesInAncientTimesCounter++;
                }
            }
            Console.WriteLine("Felfedezések száma az ókorban: " + discoveriesInAncientTimesCounter);
        }
    }
}
