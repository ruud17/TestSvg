using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AgriBook.DB.Models
{
    public class ParcelArea : Amount
    {
        [JsonIgnore] 
        [IgnoreDataMember]
        public Parcel Parcel { get; set; }
    }
}
