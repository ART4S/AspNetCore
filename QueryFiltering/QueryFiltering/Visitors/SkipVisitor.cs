namespace QueryFiltering.Visitors
{
    internal class SkipVisitor : QueryFilteringBaseVisitor<int>
    {
        public override int VisitSkip(QueryFilteringParser.SkipContext context)
        {
            return int.Parse(context.count.Text);
        }
    }
}
