using System.Runtime.Serialization;
using System.ServiceModel;

namespace wcf_chat
{
    [DataContract]
    public class PrivateMessage
    {
        [DataMember]
        public string Adressee { get; set; }

        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public int ID { get; set; }
    }
}
