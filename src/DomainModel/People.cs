using System;
using System.Runtime.Serialization;

namespace dotnetcorecrud.DomainModel
{
    [DataContract]
    public class People
    {
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public Guid PeopleKey { get; set; }
    }
}