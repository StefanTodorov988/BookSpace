using System.Collections.Generic;


namespace BookSpace.Data.Contracts
{ 

    public class PagedResult<T>  where T : class
    {
       
        public int Page { get; set; }
        public int PageSize { get; set; }

        public IEnumerable<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }


    }
}
