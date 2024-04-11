using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamController : Controller
    {
        [HttpPost, Route("Q1")]
        public List<string> Q1(string input)
        {
            var res = new List<string>();
            List<string> permutations = GeneratePermutations(input);
            foreach (string permutation in permutations) { res.Add(permutation); }
            return res;
        }

        [HttpPost, Route("Q2")]
        public int Q2(string input)
        {
            var filterData = input.Split(',');

            var afterFindOdd = new List<int>();

            foreach (var x in filterData) { if (Convert.ToInt32(x) % 2 != 0) afterFindOdd.Add(Convert.ToInt32(x)); }

            var groupby = afterFindOdd.GroupBy(x => x).Select(y => new OddResult
            {
                Number = y.Key,
                Count = y.Count()
            }).ToList();

            return groupby.MaxBy(x => x.Count).Number;
        }

        [HttpPost, Route("Q3")]
        public int Q3(string input)
        {
            var count = 0;
            var filterData = input.Split(',');
            foreach (var x in filterData) { if (x == ":)" || x == ":D" || x == ";-D") count++; }
            return count;
        }

        private List<string> GeneratePermutations(string input)
        {
            List<string> permutations = new List<string>();
            GeneratePermutationsHelper(input.ToCharArray(), 0, permutations);
            return permutations;
        }
        private void GeneratePermutationsHelper(char[] arr, int index, List<string> permutations)
        {
            if (index == arr.Length - 1)
            {
                permutations.Add(new string(arr));
                return;
            }

            for (int i = index; i < arr.Length; i++)
            {
                Swap(arr, index, i);
                GeneratePermutationsHelper(arr, index + 1, permutations);
                Swap(arr, index, i); // backtrack
            }
        }
        private void Swap(char[] arr, int i, int j)
        {
            char temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private class OddResult
        {
            public int Number { get; set; }
            public int Count { get; set; }
        }
    }
}
