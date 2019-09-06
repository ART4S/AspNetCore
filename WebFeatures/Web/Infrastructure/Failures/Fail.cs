namespace Web.Infrastructure.Failures
{
    public class Fail
    {
        public string UserMessage { get; set; }

        public Fail()
        {
            
        }

        public static implicit operator Fail(string str)
        {
            return new Fail()
            {
                UserMessage = str
            };
        }
    }
}
