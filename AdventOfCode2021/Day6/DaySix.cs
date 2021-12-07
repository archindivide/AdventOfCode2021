using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DaySix
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day6/Data.txt");
            var newFish = data.Single().Split(",");
            var school = new List<LanternFish>();

            foreach(var fish in newFish)
            {
                school.Add(new LanternFish(int.Parse(fish)));
            }

            for(int i = 0; i < 80; i++)
            {
                var newSpawn = new List<LanternFish>();
                foreach(var fish in school)
                {
                    var spawn = fish.CycleDay();
                    if (spawn != null)
                    {
                        newSpawn.Add(spawn);
                    }
                }
                school.AddRange(newSpawn);
            }
            
            return school.Count().ToString();
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day6/Data.txt");
            var fish = data.Single().Split(",").Select(d => int.Parse(d));

            var spawnCycle = new long[9];

            spawnCycle[0] = fish.Where(f => f == 0).Count();
            spawnCycle[1] = fish.Where(f => f == 1).Count();
            spawnCycle[2] = fish.Where(f => f == 2).Count();
            spawnCycle[3] = fish.Where(f => f == 3).Count();
            spawnCycle[4] = fish.Where(f => f == 4).Count();
            spawnCycle[5] = fish.Where(f => f == 5).Count();
            spawnCycle[6] = fish.Where(f => f == 6).Count();
            spawnCycle[7] = fish.Where(f => f == 7).Count();
            spawnCycle[8] = fish.Where(f => f == 8).Count();

            //1400784684 low
            for(int i = 0; i < 256; i++)
            {
                DaySpawn(spawnCycle);
            }

            return spawnCycle.Sum().ToString();
        }

        public void DaySpawn(long[] spawnCycle)
        {
            var spawned = spawnCycle[0];
            spawnCycle[0] = spawnCycle[1];
            spawnCycle[1] = spawnCycle[2];
            spawnCycle[2] = spawnCycle[3];
            spawnCycle[3] = spawnCycle[4];
            spawnCycle[4] = spawnCycle[5];
            spawnCycle[5] = spawnCycle[6];
            spawnCycle[6] = spawnCycle[7] + spawned;
            spawnCycle[7] = spawnCycle[8];
            spawnCycle[8] = spawned;
        }
    }

    public class LanternFish
    {
        public int SpawnTimer { get; set; }

        public LanternFish(int startTimer)
        {
            SpawnTimer = startTimer;
        }

        public LanternFish? CycleDay()
        {
            LanternFish newLaternFish = null;
            SpawnTimer--;
            if(SpawnTimer < 0)
            {
                newLaternFish = new LanternFish(8);
                SpawnTimer = 6;
            }

            return newLaternFish;
        }
    }
}
