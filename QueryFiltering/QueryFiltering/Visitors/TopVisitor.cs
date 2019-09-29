namespace QueryFiltering.Visitors
{
    internal class TopVisitor : QueryFilteringBaseVisitor<int>
    {
        public override int VisitTop(QueryFilteringParser.TopContext context)
        {
            return int.Parse(context.count.Text);
        }
    }
}
