using System.Collections;

namespace WebFeatures.QueryFiltering.Results
{
    public class FilteringResult
    {
        public int Count { get; }

        public ICollection Records { get; }

        public FilteringResult(ICollection records)
        {
            Count = records.Count;
            Records = records;
        }
    }
}
