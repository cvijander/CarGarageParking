namespace CarGarageParking.Models
{
    public class PaginationViewModel
    {
        public IEnumerable<Garage> Garages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
        public int TotalPages
        {
            get
            {
                int totalP = TotalCount / PageSize;
                if (TotalCount % PageSize != 0)
                {
                    totalP++;
                }
                    return totalP;
                
            }
                
        }

    public int CurrentPage { get; set; }

     public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
                
            }
        }
        

      public bool HasNext 
        {
            get
            {
                return CurrentPage < TotalPages;

            }
        }
          
    }
}
