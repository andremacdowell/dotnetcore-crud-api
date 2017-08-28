using System;
using System.Runtime.Serialization;

namespace dotnetcorecrud.Models
{
    [DataContract]
    public class People
    {
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public Guid PeopleKey { get; set; }
    }
}