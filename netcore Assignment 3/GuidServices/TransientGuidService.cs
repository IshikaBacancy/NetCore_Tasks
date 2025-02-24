namespace netcore_Assignment3.GuidServices
{
    public class TransientGuidService : IGuidService
    {
        private readonly string _guid;

        public TransientGuidService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return _guid;
        }
    }
}
