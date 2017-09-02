using System.Runtime.Serialization;

namespace dotnetcorecrud.DomainModel
{
    [DataContract]
    public class JobResult
    {
        public JobResult(string methodType, bool isFinished)
        {
            MethodType = methodType;
            IsFinished = isFinished;
        }

        [DataMember]
        public string MethodType { get; set; }

        [DataMember]
        public bool IsFinished { get; set; }
    }
}