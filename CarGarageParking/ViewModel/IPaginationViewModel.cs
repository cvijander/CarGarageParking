namespace CarGarageParking.ViewModel
{
    public interface IPaginationViewModel
    {
        
        public int TotalPages { get; }

        public int CurrentPage { get; set; }

        public bool HasPrevious { get; }    
        public bool HasNext { get;}
    }
}
