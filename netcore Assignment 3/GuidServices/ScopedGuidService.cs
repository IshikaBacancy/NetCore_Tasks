﻿namespace netcore_Assignment3.GuidServices
{
    public class ScopedGuidService : IGuidService
    {
        private readonly string _guid;

        public ScopedGuidService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return _guid;
        }
    }
}
