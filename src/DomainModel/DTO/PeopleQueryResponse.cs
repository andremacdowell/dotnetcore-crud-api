using System;
using System.Runtime.Serialization;

namespace dotnetcorecrud.DomainModel.DTO
{
    public class PeopleQueryResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public Guid PeopleKey { get; set; }
    }
}