namespace DapperContext.Queries.Abstractions
{
    public interface IInsertQuery
    {
        string Body();

        string ReturnId();
    }
}
