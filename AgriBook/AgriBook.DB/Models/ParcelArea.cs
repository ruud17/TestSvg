namespace AgriBook.DB.Models
{
    public class ParcelArea : Amount
    {
        public virtual Parcel Parcel { get; set; }
    }
}
