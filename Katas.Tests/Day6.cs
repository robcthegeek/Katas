using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Katas.Tests
{
    public class MemoryBanks
    {
        private readonly int[] _banks;
        private readonly HashSet<string> _memory;

        public MemoryBanks(int[] initialConfiguration)
        {
            _banks = (int[])initialConfiguration.Clone();
            _memory = new HashSet<string>
            {
                Configuration
            };
        }

        internal string Configuration => string.Join("-", _banks);

        internal uint MostUsedBank
        {
            get
            {
                var bankIndex = -1;
                var bankUsage = -1;
                for (int i = 0; i < _banks.Length; i++)
                {
                    if (_banks[i] > bankUsage)
                    {
                        bankIndex = i;
                        bankUsage = _banks[i];
                    }
                }

                return (uint)bankIndex;
            }
        }

        public uint Reallocate()
        {
            uint cycles = 0;

            do
            {
                Redistribute(MostUsedBank);
                cycles++;

            } while (_memory.Add(Configuration));

            return cycles;
        }

        private void Redistribute(uint fromBank)
        {
            var blocks = _banks[fromBank];
            _banks[fromBank] = 0;
            var currentBank = fromBank;
            do
            {
                currentBank++;
                if (currentBank >= _banks.Length)
                    currentBank = 0;

                _banks[currentBank] = _banks[currentBank] + 1;
                blocks--;
            } while (blocks > 0);
        }
    }

    [TestFixture]
    public class Day6
    {

        private uint Solve(IEnumerable<string> memoryBanks)
        {
            return Solve(memoryBanks.Select(int.Parse).ToArray());
        }

        private uint Solve(int[] configuration)
        {
            var banks = new MemoryBanks(configuration);

            var cycles = banks.Reallocate();

            return cycles;
        }

        [TestCase("0,2,7,0", 5)]
        public void Examples_Return_Expected(string memoryBanks, int expected)
        {
            var solution = Solve(memoryBanks.Split(','));

            Assert.That(solution, Is.EqualTo(expected));
        }

        [Test]
        //[Ignore("YOLO")]
        public void ChallengeInput_Produces_WinningResult()
        {
            var solution = Solve("4,1,15,12,0,9,9,5,5,8,7,3,14,5,12,3".Split(','));

            Console.WriteLine($"Solution is... (drum roll): {solution}");
        }
    }
}